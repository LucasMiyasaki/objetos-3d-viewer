using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using System.Numerics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

namespace Objetos3D.Classes
{
    public class Objeto3D
    {
        private List<(float x, float y, float z)> listaVerticesOriginais;
        private List<(int a, int b, int c)> listaFaces;
        private Matriz4x4 matrizAcumulada = new Matriz4x4();

        private Bitmap _frameBuffer;
        private readonly object _frameLock = new();

        public Light IluminacaoLuz { get; set; }
        public Material MaterialObjeto { get; set; }
        public string ShadingModel { get; set; }

        public Objeto3D()
        {
            listaVerticesOriginais = new List<(float x, float y, float z)>();
            listaFaces = new List<(int a, int b, int c)>();
            IluminacaoLuz = new Light();
            MaterialObjeto = new Material();
            ShadingModel = "";
        }

        public Objeto3D(Objeto3D objeto)
        {
            this.listaVerticesOriginais = objeto.listaVerticesOriginais.ToList();
            this.listaFaces = objeto.listaFaces.ToList();

            this.matrizAcumulada = new Matriz4x4(objeto.matrizAcumulada);
        }
        public class Poligono
        {
            public List<int> X { get; set; } = new List<int>();
            public List<int> Y { get; set; } = new List<int>();
            public List<int> Z { get; set; } = new List<int>();
        }

        public void AddVertice(float x, float y, float z)
        {
            listaVerticesOriginais.Add((x, y, z));
        }

        public List<(float x, float y, float z)> GetListaVerticeOriginais()
        {
            return listaVerticesOriginais;
        }

        public void AddFaces(int x, int y, int z)
        {
            listaFaces.Add((x, y, z));
        }

        public List<(int a, int b, int c)> GetListaFaces()
        {
            return listaFaces;
        }

        public void lerArquivo(string caminhoArquivo)
        {
            try
            {
                string[] linhas = File.ReadAllLines(caminhoArquivo);

                foreach (string linha in linhas)
                {
                    if (linha.StartsWith("v ")) // apenas vértices
                    {
                        string[] partes = linha.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (partes.Length >= 4)
                        {
                            float x, y, z;
                            bool temVirgula = partes.Skip(1).Take(3).Any(p => p.Contains(","));

                            if (temVirgula)
                            {
                                x = float.Parse(partes[1], new CultureInfo("pt-BR"));
                                y = float.Parse(partes[2], new CultureInfo("pt-BR"));
                                z = float.Parse(partes[3], new CultureInfo("pt-BR"));
                            }
                            else
                            {
                                x = float.Parse(partes[1], CultureInfo.InvariantCulture);
                                y = float.Parse(partes[2], CultureInfo.InvariantCulture);
                                z = float.Parse(partes[3], CultureInfo.InvariantCulture);
                            }

                            this.AddVertice(x, y, z);
                        }
                    }
                    else if (linha.StartsWith("f ")) // apenas faces
                    {
                        string[] partes = linha.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (partes.Length >= 4)
                        {
                            int x, y, z;

                            x = int.Parse(partes[1].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            y = int.Parse(partes[2].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            z = int.Parse(partes[3].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0]);

                            this.AddFaces(x, y, z);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private Bitmap GetFrameBuffer(int width, int height)
        {
            if (_frameBuffer == null ||
                _frameBuffer.Width != width ||
                _frameBuffer.Height != height)
            {
                _frameBuffer?.Dispose();                                 // libera o antigo
                _frameBuffer = new Bitmap(width, height,                 // cria 24 bpp
                                          PixelFormat.Format24bppRgb);
            }
            return _frameBuffer;
        }

        public Bitmap desenhaObjeto(int pictureBoxWidth, int pictureBoxHeight, bool removerFaces, bool scanLine, string shadder, Matriz4x4 m = null)
        {
            Bitmap bmp = GetFrameBuffer(pictureBoxWidth, pictureBoxHeight);

            using (Graphics g = Graphics.FromImage(bmp))
                g.Clear(Color.White);

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            ShadingModel = shadder;

            int[,] z_buffer = new int[pictureBoxWidth, pictureBoxHeight];
            if (scanLine)
            {
                for (int x = 0; x < pictureBoxWidth; x++)
                {
                    for (int y = 0; y < pictureBoxHeight; y++)
                    {
                        z_buffer[x, y] = -999;
                    }
                }
            }

            try
            {
                int centroX = pictureBoxWidth / 2;
                int centroY = pictureBoxHeight / 2;

                var vertices = GetListaVerticeOriginais();
                var faces = GetListaFaces();

                Vector3 viewVector = Vector3.Normalize(new Vector3(0, 0, 1)); // Observador fixo olhando de Z+ para a origem

                foreach (var face in faces)
                {
                    bool visivel = true;

                    var v1_obj = vertices[face.a - 1];
                    var v2_obj = vertices[face.b - 1];
                    var v3_obj = vertices[face.c - 1];

                    var matrizFinal = matrizAcumulada;
                    if (m != null) matrizFinal *= m;

                    var t1_tuple = Matriz4x4.Transform(v1_obj, matrizFinal);
                    var t2_tuple = Matriz4x4.Transform(v2_obj, matrizFinal);
                    var t3_tuple = Matriz4x4.Transform(v3_obj, matrizFinal);

                    Vector3 t1 = new Vector3(t1_tuple.x, t1_tuple.y, t1_tuple.z);
                    Vector3 t2 = new Vector3(t2_tuple.x, t2_tuple.y, t2_tuple.z);
                    Vector3 t3 = new Vector3(t3_tuple.x, t3_tuple.y, t3_tuple.z);

                    Vector3 normalFace = CalcularNormalDaFace(t1, t2, t3);

                    if (removerFaces)
                    {
                        if (Vector3.Dot(normalFace, viewVector) <= 0)
                            visivel = false;
                    }

                    Point p1_screen = new Point((int)t1.X + centroX, (int)-t1.Y + centroY);
                    Point p2_screen = new Point((int)t2.X + centroX, (int)-t2.Y + centroY);
                    Point p3_screen = new Point((int)t3.X + centroX, (int)-t3.Y + centroY);

                    Color corDaFace = Color.Gray;

                    if (visivel)
                    {
                        if (scanLine) // Preenchimento para Flat Shading
                        {
                            if(ShadingModel == "Flat")
                            {
                                Vector3 pontoCentralFace = (t1 + t2 + t3) / 3.0f;
                                corDaFace = CalcularCorPhong(pontoCentralFace, normalFace, IluminacaoLuz, MaterialObjeto, viewVector);
                            }

                            Poligono poligono = new Poligono();
                            poligono.X.AddRange(new[] { p1_screen.X, p2_screen.X, p3_screen.X });
                            poligono.Y.AddRange(new[] { p1_screen.Y, p2_screen.Y, p3_screen.Y });
                            poligono.Z.AddRange(new[] { (int)t1.Z, (int)t2.Z, (int)t3.Z });
                            ScanLineFill(poligono, corDaFace, bmpData, z_buffer);

                            if(ShadingModel == "")
                            {
                                DesenharLinhaBresenham(bmpData, p1_screen, p2_screen, (int)t1.Z, (int)t2.Z, Color.Black, scanLine, z_buffer);
                                DesenharLinhaBresenham(bmpData, p2_screen, p3_screen, (int)t2.Z, (int)t3.Z, Color.Black, scanLine, z_buffer);
                                DesenharLinhaBresenham(bmpData, p3_screen, p1_screen, (int)t3.Z, (int)t1.Z, Color.Black, scanLine, z_buffer);
                            }
                        }
                        else
                        {
                            Color corAresta = (ShadingModel == "Flat") ? corDaFace : Color.Black;
                            DesenharLinhaBresenham(bmpData, p1_screen, p2_screen, (int)t1.Z, (int)t2.Z, corAresta, scanLine, z_buffer);
                            DesenharLinhaBresenham(bmpData, p2_screen, p3_screen, (int)t2.Z, (int)t3.Z, corAresta, scanLine, z_buffer);
                            DesenharLinhaBresenham(bmpData, p3_screen, p1_screen, (int)t3.Z, (int)t1.Z, corAresta, scanLine, z_buffer);
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bmpData);
            }
            return bmp;
        }

        private unsafe void DesenharLinhaBresenham(BitmapData bmpData, Point p1, Point p2, int z0, int z1, Color cor, bool flagDoScanLine, int[,] z_buffer)
        {
            int x0 = p1.X;
            int y0 = p1.Y;
            int x1 = p2.X;
            int y1 = p2.Y;

            int dx = Math.Abs(x1 - x0);
            int sx = x0 < x1 ? 1 : -1;
            int dy = -Math.Abs(y1 - y0);
            int sy = y0 < y1 ? 1 : -1;
            int err = dx + dy;

            int steps = Math.Max(dx, Math.Abs(dy));
            float currentZ = z0;
            float zStep = steps > 0 ? (float)(z1 - z0) / steps : 0f;

            while (true)
            {
                if (flagDoScanLine)
                {
                    if (x0 >= 0 && x0 < z_buffer.GetLength(0) && y0 >= 0 && y0 < z_buffer.GetLength(1))
                    {
                        if (currentZ > z_buffer[x0, y0])
                        {
                            SetPixel(x0, y0, bmpData, cor);
                            z_buffer[x0, y0] = (int)currentZ;
                        }
                    }
                    else
                    {
                        SetPixel(x0, y0, bmpData, cor);
                    }
                }
                else
                {
                    SetPixel(x0, y0, bmpData, cor);
                }

                if (x0 == x1 && y0 == y1) break;
                int e2 = 2 * err;
                if (e2 >= dy)
                {
                    err += dy;
                    x0 += sx;
                }
                if (e2 <= dx)
                {
                    err += dx;
                    y0 += sy;
                }
                currentZ += zStep;
            }
        }

        private void ScanLineFill(Poligono pol, Color cor,
                                BitmapData bmpData, int[,] z_buffer)
        {
            if (pol.X.Count < 3) return;

            int minY = pol.Y.Min();
            int maxY = pol.Y.Max();

            minY = Math.Max(minY, 0);
            maxY = Math.Min(maxY, z_buffer.GetLength(1) - 1);


            for (int y = minY; y <= maxY; y++)
            {
                var inters = new List<(int x, float z)>();

                for (int i = 0; i < pol.X.Count; i++)
                {
                    int j = (i + 1) % pol.X.Count;

                    int x_i = pol.X[i]; int y_i = pol.Y[i]; float z_i = pol.Z[i];
                    int x_j = pol.X[j]; int y_j = pol.Y[j]; float z_j = pol.Z[j];

                    if ((y_i <= y && y_j > y) || (y_j <= y && y_i > y)) { 
                        if (y_j - y_i == 0) continue;

                        float t = (float)(y - y_i) / (y_j - y_i);
                        int x = x_i + (int)((x_j - x_i) * t);
                        float z_at_intersection = z_i + (z_j - z_i) * t;
                        inters.Add((x, z_at_intersection));
                    }
                }

                inters.Sort((a, b) => a.x.CompareTo(b.x));

                for (int k = 0; k < inters.Count - 1; k += 2)
                {
                    int xStart = inters[k].x;
                    int xEnd = inters[k + 1].x;
                    float zStart = inters[k].z;
                    float zEnd = inters[k + 1].z;

                    int currentXStart = Math.Max(xStart, 0);
                    int currentXEnd = Math.Min(xEnd, z_buffer.GetLength(0) - 1);

                    if (currentXStart > currentXEnd) continue;

                    int spanLength = xEnd - xStart;
                    float z = zStart;
                    float zStep = (spanLength) > 0 ? (zEnd - zStart) / (spanLength) : 0f;

                    if (xStart < currentXStart)
                    {
                        z += zStep * (currentXStart - xStart);
                    }


                    for (int x = currentXStart; x <= currentXEnd; x++)
                    {
                        if (z > z_buffer[x, y])
                        {
                            SetPixel(x, y, bmpData, cor);
                            z_buffer[x, y] = (int)z;
                        }
                        z += zStep;
                    }
                }
            }
        }

        private unsafe void SetPixel(int x, int y, BitmapData bmpData, Color cor)
        {
            int bytesPorPixel = 3; // Format24bppRgb
            int stride = bmpData.Stride;

            // Certifique-se de que (x,y) esteja dentro do bitmap
            if (x < 0 || x >= bmpData.Width || y < 0 || y >= bmpData.Height)
                return;

            // Ponteiro para o início da linha
            byte* linha = (byte*)bmpData.Scan0 + (y * stride);
            // Posição do pixel em X
            int pos = x * bytesPorPixel;

            // 24bpp => B, G, R
            linha[pos + 0] = cor.B;  // Blue
            linha[pos + 1] = cor.G;  // Green
            linha[pos + 2] = cor.R;  // Red
        }

        public void AcumularRotacao(float deltaX, float deltaY)
        {
            float angX = deltaY * 0.01f;
            float angY = deltaX * 0.01f;

            var rotX = Matriz4x4.RotationX(angX);
            var rotY = Matriz4x4.RotationY(angY);

            matrizAcumulada = rotY * rotX * matrizAcumulada;
        }

        public void AcumularRotacaoZ(float deltaX)
        {
            float angZ = deltaX * 0.01f;

            var rotZ = Matriz4x4.RotationZ(angZ);

            matrizAcumulada = rotZ * matrizAcumulada;
        }


        public void AcumularEscala(float escX, float escY, float escZ)
        {
            var escalaNova = Matriz4x4.Escala(escX, escY, escZ);
            matrizAcumulada = escalaNova * matrizAcumulada;
        }

        public void AcumularTranslacao(float x, float y, float z)
        {
            var translacao = Matriz4x4.Translacao(x, y, z);
            matrizAcumulada = translacao * matrizAcumulada;
        }

        private (float x, float y, float z) CalcularNormal((float x, float y, float z)v1, (float x, float y, float z)v2, (float x, float y, float z)v3)
        {
            var vet1 = (x: v1.x - v2.x, y: v1.y - v2.y, z: v1.z - v2.z);
            var vet2 = (x: v1.x - v3.x, y: v1.y - v3.y, z: v1.z - v3.z);

            var n = (
                x: vet1.y * vet2.z - vet1.z * vet2.y,
                y: vet1.z * vet2.x - vet1.x * vet2.z,
                z: vet1.x * vet2.y - vet1.y * vet2.x);

            float comprimento = MathF.Sqrt(n.x * n.x + n.y * n.y + n.z * n.z);

            return (n.x / comprimento, n.y / comprimento, n.z / comprimento);
        }

        private Vector3 CalcularNormalDaFace(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            Vector3 ab = v2 - v1;
            Vector3 ac = v3 - v1;
            Vector3 normal = Vector3.Cross(ab, ac);

            if (normal.LengthSquared() > 0)
                return Vector3.Normalize(normal);
            return Vector3.Zero;
        }

        private Color CalcularCorPhong(Vector3 ponto, Vector3 normal, Light luz, Material material, Vector3 viewVector)
        {
            // Componente Ambiente
            var iaF = luz.GetIaFloats();
            var kaF = material.GetKaFloats();
            float rAmb = iaF.r * kaF.r;
            float gAmb = iaF.g * kaF.g;
            float bAmb = iaF.b * kaF.b;

            // Vetor L (para a luz)
            Vector3 lVec = Vector3.Normalize(luz.Position - ponto);

            // Componente Difusa
            var idF = luz.GetIdFloats();
            var kdF = material.GetKdFloats();
            float dotLN = Math.Max(0, Vector3.Dot(lVec, normal));
            float rDiff = idF.r * kdF.r * dotLN;
            float gDiff = idF.g * kdF.g * dotLN;
            float bDiff = idF.b * kdF.b * dotLN;

            // Componente Especular
            var isF = luz.GetIsFloats();
            var ksF = material.GetKsFloats();
            Vector3 hVec = Vector3.Normalize(lVec + viewVector); // Vetor Halfway [cite: 10, 11, 136]
            float dotHN = Math.Max(0, Vector3.Dot(hVec, normal));
            float specFactor = (float)Math.Pow(dotHN, material.Shininess);

            float rSpec = isF.r * ksF.r * specFactor;
            float gSpec = isF.g * ksF.g * specFactor;
            float bSpec = isF.b * ksF.b * specFactor;

            // Cor final
            int r = (int)(Math.Clamp(rAmb + rDiff + rSpec, 0, 1) * 255);
            int g = (int)(Math.Clamp(gAmb + gDiff + gSpec, 0, 1) * 255);
            int b = (int)(Math.Clamp(bAmb + bDiff + bSpec, 0, 1) * 255);

            return Color.FromArgb(r, g, b);
        }
    }
}
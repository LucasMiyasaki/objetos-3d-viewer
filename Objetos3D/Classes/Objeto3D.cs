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

namespace Objetos3D.Classes
{
    public class Objeto3D
    {
        private List<(float x, float y, float z)> listaVerticesOriginais;
        private List<(int a, int b, int c)> listaFaces;
        private Matriz4x4 matrizAcumulada = new Matriz4x4();

        private Bitmap _frameBuffer;
        private readonly object _frameLock = new();

        public Objeto3D()
        {
            listaVerticesOriginais = new List<(float x, float y, float z)>();
            listaFaces = new List<(int a, int b, int c)>();
        }

        public Objeto3D(Objeto3D objeto)
        {
            this.listaVerticesOriginais = objeto.listaVerticesOriginais.ToList();
            this.listaFaces = objeto.listaFaces.ToList();

            this.matrizAcumulada = new Matriz4x4(objeto.matrizAcumulada);
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

        public Bitmap desenhaObjeto(int pictureBoxWidth,int pictureBoxHeight, bool removerFaces, Matriz4x4 m = null)
        {
            Bitmap bmp = GetFrameBuffer(pictureBoxWidth, pictureBoxHeight);

            using (Graphics g = Graphics.FromImage(bmp))
                g.Clear(Color.White);

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect,
                                              ImageLockMode.ReadWrite,
                                              bmp.PixelFormat);

            try
            {
                int centroX = pictureBoxWidth / 2;
                int centroY = pictureBoxHeight / 2;

                var vertices = GetListaVerticeOriginais();
                var faces = GetListaFaces();

                foreach (var face in faces)
                {
                    bool visivel = true;

                    var v1 = vertices[face.a - 1];
                    var v2 = vertices[face.b - 1];
                    var v3 = vertices[face.c - 1];

                    var matrizFinal = matrizAcumulada;
                    if (m != null) matrizFinal *= m;

                    var t1 = Matriz4x4.Transform(v1, matrizFinal);
                    var t2 = Matriz4x4.Transform(v2, matrizFinal);
                    var t3 = Matriz4x4.Transform(v3, matrizFinal);

                    if(removerFaces) {
                        var normal = CalcularNormal(t1, t2, t3);

                        var remocao = 0 * normal.x + 0 * normal.y + (-1) * normal.z;
                        if (remocao >= 0)
                            visivel = false;
                    }

                    Point p1 = new((int)t1.x + centroX,
                                   (int)-t1.y + centroY);
                    Point p2 = new((int)t2.x + centroX,
                                   (int)-t2.y + centroY);
                    Point p3 = new((int)t3.x + centroX,
                                   (int)-t3.y + centroY);

                    if (visivel)
                    {
                        DesenharLinhaBresenham(bmpData, p1.X, p1.Y, p2.X, p2.Y, Color.Black);
                        DesenharLinhaBresenham(bmpData, p2.X, p2.Y, p3.X, p3.Y, Color.Black);
                        DesenharLinhaBresenham(bmpData, p3.X, p3.Y, p1.X, p1.Y, Color.Black);
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bmpData);
            }
            return bmp;
        }

        private unsafe void DesenharLinhaBresenham(BitmapData bmpData, int x0, int y0, int x1, int y1, Color cor)
        {
            int bytesPorPixel = 3; // Format24bppRgb
            int stride = bmpData.Stride;

            // Função local que pinta 1 pixel (x,y) em cor "cor"
            void SetPixel(int x, int y)
            {
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

            // Algoritmo de Bresenham
            int dx = Math.Abs(x1 - x0);
            int sx = x0 < x1 ? 1 : -1;
            int dy = -Math.Abs(y1 - y0);
            int sy = y0 < y1 ? 1 : -1;
            int err = dx + dy;

            while (true)
            {
                SetPixel(x0, y0);
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
            }
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
    }
}
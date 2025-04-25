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
    internal class Objeto3D
    {
        private List<(float x, float y, float z)> listaVerticesOriginais;
        private List<(int a, int b, int c)> listaFaces;
        private Matriz4x4 matrizRotacao = new Matriz4x4();
        private Matriz4x4 matrizEscala = new Matriz4x4();

        public Objeto3D()
        {
            listaVerticesOriginais = new List<(float x, float y, float z)>();
            listaFaces = new List<(int a, int b, int c)>();
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

        public Bitmap desenhaObjeto(int pictureBoxWidth, int pictureBoxHeight, int deslX, int deslY)
        {
            // Cria o bitmap com formato 24bpp para acesso direto (3 bytes por pixel)
            Bitmap bmp = new Bitmap(pictureBoxWidth, pictureBoxHeight, PixelFormat.Format24bppRgb);

            // Bloqueia o bitmap para acesso direto à memória
            Rectangle retangulo = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(retangulo, ImageLockMode.ReadWrite, bmp.PixelFormat);

            // Ponteiro para o início dos dados
            IntPtr ptrInicio = bmpData.Scan0;
            int stride = bmpData.Stride;
            int bytesPorPixel = 3; // pois usamos Format24bppRgb

            // Preenche a tela de branco (podemos fazer de forma simples com um for)
            // n total de bytes = stride * altura
            int totalBytes = Math.Abs(stride) * bmp.Height;
            unsafe
            {
                byte* p = (byte*)ptrInicio;
                for (int i = 0; i < totalBytes; i++)
                {
                    // Branco = 255 em cada componente
                    p[i] = 255;
                }
            }

            // Coordenadas do centro
            int centroX = pictureBoxWidth / 2;
            int centroY = pictureBoxHeight / 2;

            // A lista de vértices e faces originais
            var vertices = this.GetListaVerticeOriginais();
            var faces = this.GetListaFaces();

            // Para cada face, desenha as três arestas
            foreach (var face in faces)
            {
                var v1 = vertices[face.a - 1];
                var v2 = vertices[face.b - 1];
                var v3 = vertices[face.c - 1];

                // Aplica a rotação e escala
                var matrizFinal = matrizRotacao * matrizEscala;
                var t1 = Matriz4x4.Transform(v1, matrizFinal);
                var t2 = Matriz4x4.Transform(v2, matrizFinal);
                var t3 = Matriz4x4.Transform(v3, matrizFinal);

                // Converte para coordenadas de tela
                Point p1 = new Point(
                    (int)(t1.x) + centroX + deslX,
                    (int)(-t1.y) + centroY + deslY);

                Point p2 = new Point(
                    (int)(t2.x) + centroX + deslX,
                    (int)(-t2.y) + centroY + deslY);

                Point p3 = new Point(
                    (int)(t3.x) + centroX + deslX,
                    (int)(-t3.y) + centroY + deslY);

                // Desenha as linhas em memória (preto)
                DesenharLinhaBresenham(bmpData, p1.X, p1.Y, p2.X, p2.Y, Color.Black);
                DesenharLinhaBresenham(bmpData, p2.X, p2.Y, p3.X, p3.Y, Color.Black);
                DesenharLinhaBresenham(bmpData, p3.X, p3.Y, p1.X, p1.Y, Color.Black);
            }

            // Libera o acesso ao bitmap
            bmp.UnlockBits(bmpData);

            // Retorna o bitmap resultante
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

            matrizRotacao = rotY * rotX * matrizRotacao;
        }

        public void AcumularRotacaoZ(float deltaX)
        {
            float angZ = deltaX * 0.01f;

            var rotZ = Matriz4x4.RotationZ(angZ);

            matrizRotacao = rotZ * matrizRotacao;
        }


        public void AcumularEscala(float escX, float escY, float escZ)
        {
            var escalaNova = Matriz4x4.Escala(escX, escY, escZ);
            matrizEscala = escalaNova * matrizEscala;
        }
    }
}
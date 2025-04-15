using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;

namespace Objetos3D.Classes
{
    internal class Objeto3D
    {
        private List<(float x, float y, float z)> listaVerticesOriginais;
        private List<(int a, int b, int c)> listaFaces;

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

        public Bitmap desenhaObjeto(int pictureBoxWidth, int pictureBoxHeight, float escala, int deslX, int deslY)
        {
            Bitmap bmp = new Bitmap(pictureBoxWidth, pictureBoxHeight);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                int centroX = pictureBoxWidth / 2;
                int centroY = pictureBoxHeight / 2;

                var vertices = this.GetListaVerticeOriginais();
                var faces = this.GetListaFaces();

                foreach (var face in faces)
                {
                    var v1 = vertices[face.a - 1]; // .obj começa em 1
                    var v2 = vertices[face.b - 1];
                    var v3 = vertices[face.c - 1];

                    Point p1 = new Point((int)(v1.x * escala) + centroX + deslX, (int)(-v1.y * escala) + centroY + deslY);
                    Point p2 = new Point((int)(v2.x * escala) + centroX + deslX, (int)(-v2.y * escala) + centroY + deslY);
                    Point p3 = new Point((int)(v3.x * escala) + centroX + deslX, (int)(-v3.y * escala) + centroY + deslY);

                    g.DrawLine(Pens.Black, p1, p2);
                    g.DrawLine(Pens.Black, p2, p3);
                    g.DrawLine(Pens.Black, p3, p1);
                }
            }

            return bmp;
        }
    }
}

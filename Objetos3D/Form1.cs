using Objetos3D.Classes;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace Objetos3D
{
    public partial class Form1 : Form
    {
        private Objeto3D objeto;

        // variáveis da movimentação do objeto
        private bool arrastando = false;
        private Point posicaoMouseAnterior;
        private int deslocamentoX = 0;
        private int deslocamentoY = 0;

        // variável para zoom do objeto
        private float escalaAtual = 1;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;

            // Permite que o PictureBox capture foco para o MouseWheel funcionar
            this.MouseWheel += Form1_MouseWheel;
            pictureBox1.MouseEnter += (s, e) => this.Focus();
        }

        private void btnAbrirObjeto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos OBJ (*.obj)|*.obj";
            openFileDialog.Title = "Selecione um arquivo .obj";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                reiniciar();
                string caminhoArquivo = openFileDialog.FileName;
                lerArquivo(caminhoArquivo);
                desenhaObjeto();
            }
        }

        private void reiniciar()
        {
            objeto = new Objeto3D();
            pictureBox1.Image = null;
            escalaAtual = 1;
        }

        private void lerArquivo(string caminhoArquivo)
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

                            objeto.AddVertice(x, y, z);
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

                            objeto.AddFaces(x, y, z);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void desenhaObjeto()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                int centroX = pictureBox1.Width / 2;
                int centroY = pictureBox1.Height / 2;

                var vertices = objeto.GetListaVerticeOriginais();
                var faces = objeto.GetListaFaces();

                foreach (var face in faces)
                {
                    float escala = escalaAtual;
                    var v1 = vertices[face.a - 1]; // .obj começa em 1
                    var v2 = vertices[face.b - 1];
                    var v3 = vertices[face.c - 1];

                    Point p1 = new Point((int)(v1.x * escala) + centroX + deslocamentoX, (int)(-v1.y * escala) + centroY + deslocamentoY);
                    Point p2 = new Point((int)(v2.x * escala) + centroX + deslocamentoX, (int)(-v2.y * escala) + centroY + deslocamentoY);
                    Point p3 = new Point((int)(v3.x * escala) + centroX + deslocamentoX, (int)(-v3.y * escala) + centroY + deslocamentoY);

                    g.DrawLine(Pens.Black, p1, p2);
                    g.DrawLine(Pens.Black, p2, p3);
                    g.DrawLine(Pens.Black, p3, p1);
                }
            }

            pictureBox1.Image = bmp;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                arrastando = true;
                posicaoMouseAnterior = e.Location;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastando)
            {
                int dx = e.X - posicaoMouseAnterior.X;
                int dy = e.Y - posicaoMouseAnterior.Y;

                deslocamentoX += dx;
                deslocamentoY += dy;

                posicaoMouseAnterior = e.Location;

                desenhaObjeto();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            arrastando = false;
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                escalaAtual += 0.1f;
            }
            else
            {
                escalaAtual -= 0.1f;
            }

            Debug.WriteLine("MouseWheel no Form: nova escala = " + escalaAtual);
            desenhaObjeto();
        }


    }
}

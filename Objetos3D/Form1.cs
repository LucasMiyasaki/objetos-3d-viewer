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

        // variável de rotacao
        private bool rotacionando = false;

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
                objeto.lerArquivo(caminhoArquivo);
                desenhaObjeto();
            }
        }

        private void reiniciar()
        {
            objeto = new Objeto3D();
            pictureBox1.Image = null;
            escalaAtual = 1;
        }

        private void desenhaObjeto()
        {
            pictureBox1.Image = objeto.desenhaObjeto(pictureBox1.Width, pictureBox1.Height, escalaAtual, deslocamentoX, deslocamentoY);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                arrastando = true;
                posicaoMouseAnterior = e.Location;
            }
            if (e.Button == MouseButtons.Right)
            {
                rotacionando = true;
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
            else if (rotacionando)
            {
                int dx = e.X - posicaoMouseAnterior.X;
                int dy = e.Y - posicaoMouseAnterior.Y;

                objeto.AcumularRotacao(dx, dy);
                posicaoMouseAnterior = e.Location;
                desenhaObjeto();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            arrastando = false;
            rotacionando = false;

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
            desenhaObjeto();
        }
    }
}

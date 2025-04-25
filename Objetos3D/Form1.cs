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
        private string eixoEscalaAtivo = "";
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

            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            this.KeyPreview = true; // Importante: permite que o formulário capture as teclas mesmo com o foco no PictureBox

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
            deslocamentoX = 0;
            deslocamentoY = 0;
    }

        private void desenhaObjeto()
        {
            pictureBox1.Image = objeto.desenhaObjeto(pictureBox1.Width, pictureBox1.Height, deslocamentoX, deslocamentoY);
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
            float fator = (e.Delta > 0) ? 1.1f : 0.9f;

            if (eixoEscalaAtivo == "X")
            {
                objeto.AcumularEscala(fator, 1f, 1f);
            }
            else if (eixoEscalaAtivo == "Y")
            {
                objeto.AcumularEscala(1f, fator, 1f);
            }
            else if (eixoEscalaAtivo == "Z")
            {
                objeto.AcumularEscala(1f, 1f, fator);
            }
            else
            {
                // Escala uniforme se nenhuma tecla estiver pressionada
                objeto.AcumularEscala(fator, fator, fator);
            }

            desenhaObjeto();
        }


        private void btn10x_Click(object sender, EventArgs e)
        {
            escalaAtual *= 10;
            desenhaObjeto();
        }

        private void btn100x_Click(object sender, EventArgs e)
        {
            escalaAtual *= 100;
            desenhaObjeto();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X) eixoEscalaAtivo = "X";
            if (e.KeyCode == Keys.Y) eixoEscalaAtivo = "Y";
            if (e.KeyCode == Keys.Z) eixoEscalaAtivo = "Z";
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            eixoEscalaAtivo = "";
        }

    }
}

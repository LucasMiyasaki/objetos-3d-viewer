using Objetos3D.Classes;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace Objetos3D
{
    public partial class Form1 : Form
    {
        private Objeto3D objeto;

        // variável da abertura do arquivo
        string caminhoArquivo;

        // variáveis da movimentação do objeto
        private bool arrastando = false;
        private Point posicaoMouseAnterior;
        private int deslocamentoX = 0;
        private int deslocamentoY = 0;
        private int deslocamentoZ = 0;

        // variável para escala do objeto
        private string eixoEscalaAtivo = "";

        // variável de rotacao
        private bool rotacionando = false;
        private int rotacaoX = 0;
        private int rotacaoY = 0;
        private int rotacaoZ = 0;

        // ­­­­­­­­­­­­­­­­­­­Escala (trackbars)
        private int escGAc = 0;
        private int escXAc = 0;
        private int escYAc = 0;
        private int escZAc = 0;
        private const float ESC_STEP = 1.10f;
        private static readonly int STEPS_10X = (int)Math.Round(Math.Log(10) / Math.Log(ESC_STEP));
        private static readonly int STEPS_100X = (int)Math.Round(Math.Log(100) / Math.Log(ESC_STEP));

        //faces
        private bool removerFaces = false;

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

        private void abrirNovo()
        {
            objeto = new Objeto3D();
            pictureBox1.Image = null;
            reiniciar();
        }

        private void reiniciar()
        {
            deslocamentoX = 0;
            deslocamentoY = 0;
            rotacaoX = 0;
            rotacaoY = 0;
            rotacaoZ = 0;
            tbTransX.Value = 0;
            tbTransY.Value = 0;
            tbTransZ.Value = 0;
            tbRotacaoX.Value = 0;
            tbRotacaoY.Value = 0;
            tbRotacaoZ.Value = 0;
            escGAc = 0;
            escXAc = 0;
            escYAc = 0;
            escZAc = 0;
            tbEscalaG.Value = 0;
            tbEscalaX.Value = 0;
            tbEscalaY.Value = 0;
            tbEscalaZ.Value = 0;
        }

        private void desenhaObjeto()
        {
            pictureBox1.Image = objeto.desenhaObjeto(pictureBox1.Width, pictureBox1.Height, removerFaces);
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
            if (objeto != null)
            {
                if (arrastando)
                {
                    int dx = e.X - posicaoMouseAnterior.X;
                    int dy = e.Y - posicaoMouseAnterior.Y;

                    deslocamentoX += dx;
                    deslocamentoY += -dy;

                    objeto.AcumularTranslacao(dx, -dy, 0);

                    tbTransX.Value = deslocamentoX;
                    tbTransY.Value = deslocamentoY;

                    posicaoMouseAnterior = e.Location;
                    desenhaObjeto();
                }
                else if (rotacionando)
                {
                    if ((ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        int dx = e.X - posicaoMouseAnterior.X;

                        objeto.AcumularRotacaoZ(dx);

                        rotacaoZ = Clamp(rotacaoZ + dx, tbRotacaoZ.Minimum, tbRotacaoZ.Maximum);
                        tbRotacaoZ.Value = rotacaoZ;
                    }
                    else
                    {
                        int dx = e.X - posicaoMouseAnterior.X;
                        int dy = e.Y - posicaoMouseAnterior.Y;

                        objeto.AcumularRotacao(dx, dy);

                        rotacaoX = Clamp(rotacaoX + dx, tbRotacaoX.Minimum, tbRotacaoX.Maximum);
                        rotacaoY = Clamp(rotacaoY + dy, tbRotacaoY.Minimum, tbRotacaoY.Maximum);

                        tbRotacaoX.Value = rotacaoX;
                        tbRotacaoY.Value = rotacaoY;
                    }

                    posicaoMouseAnterior = e.Location;
                    desenhaObjeto();
                }
            }

        }

        private int Clamp(int v, int min, int max) => Math.Min(Math.Max(v, min), max);

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            arrastando = false;
            rotacionando = false;

        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (objeto != null)
            {
                float fator = (e.Delta > 0) ? 1.1f : 0.9f;
                int step = (e.Delta > 0) ? 1 : -1;

                if (eixoEscalaAtivo == "X")
                {
                    objeto.AcumularEscala(fator, 1f, 1f);

                    escXAc = Clamp(escXAc + step, tbEscalaX.Minimum, tbEscalaX.Maximum);
                    tbEscalaX.Value = escXAc;
                }
                else if (eixoEscalaAtivo == "Y")
                {
                    objeto.AcumularEscala(1f, fator, 1f);

                    escYAc = Clamp(escYAc + step, tbEscalaY.Minimum, tbEscalaY.Maximum);
                    tbEscalaY.Value = escYAc;
                }
                else if (eixoEscalaAtivo == "Z")
                {
                    objeto.AcumularEscala(1f, 1f, fator);

                    escZAc = Clamp(escZAc + step, tbEscalaZ.Minimum, tbEscalaZ.Maximum);
                    tbEscalaZ.Value = escZAc;
                }
                else
                {
                    // Escala uniforme se nenhuma tecla estiver pressionada
                    objeto.AcumularEscala(fator, fator, fator);

                    escGAc = Clamp(escGAc + step, tbEscalaG.Minimum, tbEscalaG.Maximum);
                    tbEscalaG.Value = escGAc;
                }

                desenhaObjeto();
            }
        }

        private void btn10x_Click_1(object sender, EventArgs e)
        {
            // 1. Avança o TrackBar global a quantidade de passos que equivale a ×10
            escGAc = Clamp(escGAc + STEPS_10X, tbEscalaG.Minimum, tbEscalaG.Maximum);
            tbEscalaG.Value = escGAc;

            // 2. Calcula o fator e aplica
            float fator = (float)Math.Pow(ESC_STEP, STEPS_10X);
            objeto.AcumularEscala(fator, fator, fator);
            desenhaObjeto();
        }

        private void btn100x_Click(object sender, EventArgs e)
        {
            escGAc = Clamp(escGAc + STEPS_100X, tbEscalaG.Minimum, tbEscalaG.Maximum);
            tbEscalaG.Value = escGAc;

            float fator = (float)Math.Pow(ESC_STEP, STEPS_100X);
            objeto.AcumularEscala(fator, fator, fator);
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

        private void abrirArquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos OBJ (*.obj)|*.obj";
            openFileDialog.Title = "Selecione um arquivo .obj";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                abrirNovo();
                caminhoArquivo = openFileDialog.FileName;
                objeto.lerArquivo(caminhoArquivo);
                desenhaObjeto();
            }
        }

        private void tbTransX_Scroll(object sender, EventArgs e)
        {
            int delta = tbTransX.Value - deslocamentoX;
            deslocamentoX = tbTransX.Value;
            objeto.AcumularTranslacao(delta, 0, 0);
            desenhaObjeto();
        }

        private void tbTransY_Scroll(object sender, EventArgs e)
        {
            int delta = tbTransY.Value + deslocamentoY;
            deslocamentoY = -tbTransY.Value;
            objeto.AcumularTranslacao(0, delta, 0);
            desenhaObjeto();
        }

        private void tbTransZ_Scroll(object sender, EventArgs e)
        {
            int delta = tbTransZ.Value - deslocamentoZ;
            deslocamentoZ = tbTransZ.Value;
            objeto.AcumularTranslacao(0, 0, delta);
            desenhaObjeto();
        }

        private void tbRotacaoX_Scroll(object sender, EventArgs e)
        {
            int delta = tbRotacaoX.Value - rotacaoX;
            rotacaoX = tbRotacaoX.Value;

            objeto.AcumularRotacao(delta, 0);
            desenhaObjeto();
        }

        private void tbRotacaoY_Scroll(object sender, EventArgs e)
        {
            int delta = tbRotacaoY.Value - rotacaoY;
            rotacaoY = tbRotacaoY.Value;

            objeto.AcumularRotacao(0, delta);
            desenhaObjeto();
        }

        private void tbRotacaoZ_Scroll(object sender, EventArgs e)
        {
            int delta = tbRotacaoZ.Value - rotacaoZ;
            rotacaoZ = tbRotacaoZ.Value;

            objeto.AcumularRotacaoZ(delta);
            desenhaObjeto();
        }


        private void tbEscalaG_Scroll(object sender, EventArgs e)
        {
            int delta = tbEscalaG.Value - escGAc;
            escGAc = tbEscalaG.Value;

            if (delta != 0)
            {
                float f = (float)Math.Pow(ESC_STEP, delta);   // fator incremental
                objeto.AcumularEscala(f, f, f);
                desenhaObjeto();
            }
        }

        private void tbEscalaX_Scroll(object sender, EventArgs e)
        {
            int delta = tbEscalaX.Value - escXAc;
            escXAc = tbEscalaX.Value;

            if (delta != 0)
            {
                float f = (float)Math.Pow(ESC_STEP, delta);
                objeto.AcumularEscala(f, 1f, 1f);
                desenhaObjeto();
            }
        }

        private void tbEscalaY_Scroll(object sender, EventArgs e)
        {
            int delta = tbEscalaY.Value - escYAc;
            escYAc = tbEscalaY.Value;

            if (delta != 0)
            {
                float f = (float)Math.Pow(ESC_STEP, delta);
                objeto.AcumularEscala(1f, f, 1f);
                desenhaObjeto();
            }
        }

        private void tbEscalaZ_Scroll(object sender, EventArgs e)
        {
            int delta = tbEscalaZ.Value - escZAc;
            escZAc = tbEscalaZ.Value;

            if (delta != 0)
            {
                float f = (float)Math.Pow(ESC_STEP, delta);
                objeto.AcumularEscala(1f, 1f, f);
                desenhaObjeto();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            reiniciar();
            objeto = new Objeto3D();
            objeto.lerArquivo(caminhoArquivo);
            desenhaObjeto();
        }

        private void controlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new ControlesForm())
                dlg.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new Form2(objeto);

            frm.ShowDialog(this);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            removerFaces = checkBox1.Checked;
            desenhaObjeto();
        }
    }
}

using System.Drawing;
using System.Windows.Forms;

namespace Objetos3D
{
    public partial class ControlesForm : Form
    {
        public ControlesForm()
        {
            InitializeComponent();

            Shown += (s, e) =>
            {
                texto.SelectionStart = 0;
                texto.SelectionLength = 0;
                ActiveControl = null;          // o form não entrega foco ao TextBox
            };
        }

        private void InitializeComponent()
        {
            this.texto = new TextBox();
            SuspendLayout();
            //
            // texto
            //
            texto.Dock = DockStyle.Fill;
            texto.Multiline = true;
            texto.ReadOnly = true;
            texto.BorderStyle = BorderStyle.None;
            texto.BackColor = SystemColors.Window;
            texto.Font = new Font("Segoe UI", 10F);
            texto.Text =
@"CONTROLES

🖱️  MOUSE
• Botão esquerdo (arrastar) ............... Translação X/Y
• Botão direito (arrastar) ................. Rotação X/Y
• Ctrl + Botão direito (arrastar) ......... Rotação Z
• Roda do mouse ............................. Escala uniforme
    ▶ segure X, Y ou Z para escalar só nesse eixo

⌨️  TECLADO
• X  .............................................. Seleciona eixo X para escala via roda
• Y  .............................................. Seleciona eixo Y para escala via roda
• Z  .............................................. Seleciona eixo Z para escala via roda
• *TrackBars* ................................ Deslocamento, rotação e escala finos
• Botões 10× / 100× .......................... Zoom rápido

⚙️  DICAS
• Clique em 'Reiniciar Transformações' para voltar ao estado inicial
";
            //
            // ControlesForm
            //
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 480);
            Controls.Add(texto);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Controles do Mouse e Teclado";
            ResumeLayout(false);
        }

        private TextBox texto;
    }
}

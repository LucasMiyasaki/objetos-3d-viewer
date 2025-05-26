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
                // evita que o TextBox receba foco quando a janela abre
                texto.SelectionStart = 0;
                texto.SelectionLength = 0;
                ActiveControl = null;
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
    ▶ segure X, Y ou Z para escalar apenas nesse eixo
• Roda do mouse + segurar L ............... Ajusta Z da luz

💡  LUZ
• Arraste o ícone da lâmpada .............. Define posição X/Y
• Segure L e use a roda .................... Aproxima/Afasta (eixo Z)

⌨️  TECLADO
• X / Y / Z ................................ Seleciona eixo para escala via roda
• L ......................................... Modo ajuste de luz Z (segurar)
• TrackBars ................................. Ajustes finos de posição, rotação e escala
• Botões 10× / 100× ........................ Zoom rápido

⚙️  DICAS
• Clique em 'Reiniciar Transformações' para voltar ao estado inicial
• Use Flat / Gouraud / Phong para trocar o sombreamento";
            // 
            // ControlesForm
            // 
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 580);
            Controls.Add(texto);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Controles do Mouse, Teclado e Luz";
            ResumeLayout(false);
        }

        private TextBox texto;
    }
}

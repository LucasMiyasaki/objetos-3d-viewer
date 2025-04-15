namespace Objetos3D
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            btnAbrirObjeto = new Button();
            nupEscala = new NumericUpDown();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nupEscala).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1143, 806);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnAbrirObjeto
            // 
            btnAbrirObjeto.Location = new Point(1206, 68);
            btnAbrirObjeto.Name = "btnAbrirObjeto";
            btnAbrirObjeto.Size = new Size(160, 29);
            btnAbrirObjeto.TabIndex = 1;
            btnAbrirObjeto.Text = "Abrir Objeto";
            btnAbrirObjeto.UseVisualStyleBackColor = true;
            btnAbrirObjeto.Click += btnAbrirObjeto_Click;
            // 
            // nupEscala
            // 
            nupEscala.DecimalPlaces = 2;
            nupEscala.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            nupEscala.Location = new Point(1216, 223);
            nupEscala.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nupEscala.Name = "nupEscala";
            nupEscala.Size = new Size(150, 27);
            nupEscala.TabIndex = 2;
            nupEscala.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nupEscala.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(1216, 182);
            label1.Name = "label1";
            label1.Size = new Size(64, 28);
            label1.TabIndex = 3;
            label1.Text = "Escala";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1403, 830);
            Controls.Add(label1);
            Controls.Add(nupEscala);
            Controls.Add(btnAbrirObjeto);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nupEscala).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button btnAbrirObjeto;
        private NumericUpDown nupEscala;
        private Label label1;
    }
}

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
            label1 = new Label();
            btn10x = new Button();
            btn100x = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1185, 130);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 2;
            label1.Text = "Zoom";
            // 
            // btn10x
            // 
            btn10x.Location = new Point(1185, 165);
            btn10x.Name = "btn10x";
            btn10x.Size = new Size(49, 29);
            btn10x.TabIndex = 3;
            btn10x.Text = "10x";
            btn10x.UseVisualStyleBackColor = true;
            btn10x.Click += btn10x_Click;
            // 
            // btn100x
            // 
            btn100x.Location = new Point(1240, 165);
            btn100x.Name = "btn100x";
            btn100x.Size = new Size(49, 29);
            btn100x.TabIndex = 4;
            btn100x.Text = "100x";
            btn100x.UseVisualStyleBackColor = true;
            btn100x.Click += btn100x_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1403, 830);
            Controls.Add(btn100x);
            Controls.Add(btn10x);
            Controls.Add(label1);
            Controls.Add(btnAbrirObjeto);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button btnAbrirObjeto;
        private Label label1;
        private Button btn10x;
        private Button btn100x;
    }
}

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
            btn10x = new Button();
            btn100x = new Button();
            menuStrip1 = new MenuStrip();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            abrirArquivoToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            label2 = new Label();
            panel2 = new Panel();
            label5 = new Label();
            tbTransZ = new TrackBar();
            label4 = new Label();
            tbTransY = new TrackBar();
            label6 = new Label();
            label3 = new Label();
            tbTransX = new TrackBar();
            panel3 = new Panel();
            label7 = new Label();
            tbRotacaoZ = new TrackBar();
            label8 = new Label();
            tbRotacaoY = new TrackBar();
            label9 = new Label();
            label10 = new Label();
            tbRotacaoX = new TrackBar();
            panel4 = new Panel();
            label15 = new Label();
            tbEscalaZ = new TrackBar();
            label11 = new Label();
            tbEscalaY = new TrackBar();
            label12 = new Label();
            tbEscalaX = new TrackBar();
            label13 = new Label();
            label14 = new Label();
            tbEscalaG = new TrackBar();
            panel5 = new Panel();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbTransZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbTransY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbTransX).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbRotacaoZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbRotacaoY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbRotacaoX).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbEscalaZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbEscalaY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbEscalaX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbEscalaG).BeginInit();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(12, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1116, 787);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btn10x
            // 
            btn10x.Location = new Point(13, 50);
            btn10x.Name = "btn10x";
            btn10x.Size = new Size(49, 29);
            btn10x.TabIndex = 3;
            btn10x.Text = "10x";
            btn10x.UseVisualStyleBackColor = true;
            btn10x.Click += btn10x_Click;
            // 
            // btn100x
            // 
            btn100x.Location = new Point(68, 50);
            btn100x.Name = "btn100x";
            btn100x.Size = new Size(49, 29);
            btn100x.TabIndex = 4;
            btn100x.Text = "100x";
            btn100x.UseVisualStyleBackColor = true;
            btn100x.Click += btn100x_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1403, 28);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            arquivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { abrirArquivoToolStripMenuItem });
            arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            arquivoToolStripMenuItem.Size = new Size(75, 24);
            arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // abrirArquivoToolStripMenuItem
            // 
            abrirArquivoToolStripMenuItem.Name = "abrirArquivoToolStripMenuItem";
            abrirArquivoToolStripMenuItem.Size = new Size(181, 26);
            abrirArquivoToolStripMenuItem.Text = "Abrir Arquivo";
            abrirArquivoToolStripMenuItem.Click += abrirArquivoToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label2);
            panel1.Location = new Point(1146, 47);
            panel1.Name = "panel1";
            panel1.Size = new Size(245, 49);
            panel1.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(50, 11);
            label2.Name = "label2";
            label2.Size = new Size(144, 25);
            label2.TabIndex = 0;
            label2.Text = "Transformações";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(label5);
            panel2.Controls.Add(tbTransZ);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(tbTransY);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(tbTransX);
            panel2.Location = new Point(1146, 102);
            panel2.Name = "panel2";
            panel2.Size = new Size(245, 148);
            panel2.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(13, 112);
            label5.Name = "label5";
            label5.Size = new Size(20, 23);
            label5.TabIndex = 10;
            label5.Text = "Z";
            // 
            // tbTransZ
            // 
            tbTransZ.AutoSize = false;
            tbTransZ.Location = new Point(32, 113);
            tbTransZ.Maximum = 1000;
            tbTransZ.Minimum = -1000;
            tbTransZ.Name = "tbTransZ";
            tbTransZ.Size = new Size(199, 34);
            tbTransZ.TabIndex = 9;
            tbTransZ.TickStyle = TickStyle.None;
            tbTransZ.Scroll += tbTransZ_Scroll;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(13, 80);
            label4.Name = "label4";
            label4.Size = new Size(19, 23);
            label4.TabIndex = 8;
            label4.Text = "Y";
            // 
            // tbTransY
            // 
            tbTransY.AutoSize = false;
            tbTransY.Location = new Point(32, 81);
            tbTransY.Maximum = 1000;
            tbTransY.Minimum = -1000;
            tbTransY.Name = "tbTransY";
            tbTransY.Size = new Size(199, 34);
            tbTransY.TabIndex = 7;
            tbTransY.TickStyle = TickStyle.None;
            tbTransY.Scroll += tbTransY_Scroll;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(13, 11);
            label6.Name = "label6";
            label6.Size = new Size(90, 23);
            label6.TabIndex = 6;
            label6.Text = "Translação";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(13, 47);
            label3.Name = "label3";
            label3.Size = new Size(20, 23);
            label3.TabIndex = 1;
            label3.Text = "X";
            // 
            // tbTransX
            // 
            tbTransX.AutoSize = false;
            tbTransX.Location = new Point(32, 48);
            tbTransX.Maximum = 1000;
            tbTransX.Minimum = -1000;
            tbTransX.Name = "tbTransX";
            tbTransX.Size = new Size(199, 34);
            tbTransX.TabIndex = 0;
            tbTransX.TickStyle = TickStyle.None;
            tbTransX.Scroll += tbTransX_Scroll;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(label7);
            panel3.Controls.Add(tbRotacaoZ);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(tbRotacaoY);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(label10);
            panel3.Controls.Add(tbRotacaoX);
            panel3.Location = new Point(1146, 256);
            panel3.Name = "panel3";
            panel3.Size = new Size(245, 151);
            panel3.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F);
            label7.Location = new Point(13, 112);
            label7.Name = "label7";
            label7.Size = new Size(20, 23);
            label7.TabIndex = 10;
            label7.Text = "Z";
            // 
            // tbRotacaoZ
            // 
            tbRotacaoZ.AutoSize = false;
            tbRotacaoZ.Location = new Point(32, 113);
            tbRotacaoZ.Maximum = 1000;
            tbRotacaoZ.Minimum = -1000;
            tbRotacaoZ.Name = "tbRotacaoZ";
            tbRotacaoZ.Size = new Size(199, 34);
            tbRotacaoZ.TabIndex = 9;
            tbRotacaoZ.TickStyle = TickStyle.None;
            tbRotacaoZ.Scroll += tbRotacaoZ_Scroll;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F);
            label8.Location = new Point(13, 80);
            label8.Name = "label8";
            label8.Size = new Size(19, 23);
            label8.TabIndex = 8;
            label8.Text = "Y";
            // 
            // tbRotacaoY
            // 
            tbRotacaoY.AutoSize = false;
            tbRotacaoY.Location = new Point(32, 81);
            tbRotacaoY.Maximum = 1000;
            tbRotacaoY.Minimum = -1000;
            tbRotacaoY.Name = "tbRotacaoY";
            tbRotacaoY.Size = new Size(199, 34);
            tbRotacaoY.TabIndex = 7;
            tbRotacaoY.TickStyle = TickStyle.None;
            tbRotacaoY.Scroll += tbRotacaoY_Scroll;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F);
            label9.Location = new Point(13, 11);
            label9.Name = "label9";
            label9.Size = new Size(72, 23);
            label9.TabIndex = 6;
            label9.Text = "Rotação";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F);
            label10.Location = new Point(13, 47);
            label10.Name = "label10";
            label10.Size = new Size(20, 23);
            label10.TabIndex = 1;
            label10.Text = "X";
            // 
            // tbRotacaoX
            // 
            tbRotacaoX.AutoSize = false;
            tbRotacaoX.Location = new Point(32, 48);
            tbRotacaoX.Maximum = 1000;
            tbRotacaoX.Minimum = -1000;
            tbRotacaoX.Name = "tbRotacaoX";
            tbRotacaoX.Size = new Size(199, 34);
            tbRotacaoX.TabIndex = 0;
            tbRotacaoX.TickStyle = TickStyle.None;
            tbRotacaoX.Scroll += tbRotacaoX_Scroll;
            // 
            // panel4
            // 
            panel4.BackColor = Color.White;
            panel4.Controls.Add(label15);
            panel4.Controls.Add(tbEscalaZ);
            panel4.Controls.Add(label11);
            panel4.Controls.Add(tbEscalaY);
            panel4.Controls.Add(btn100x);
            panel4.Controls.Add(btn10x);
            panel4.Controls.Add(label12);
            panel4.Controls.Add(tbEscalaX);
            panel4.Controls.Add(label13);
            panel4.Controls.Add(label14);
            panel4.Controls.Add(tbEscalaG);
            panel4.Location = new Point(1146, 413);
            panel4.Name = "panel4";
            panel4.Size = new Size(245, 221);
            panel4.TabIndex = 12;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 10F);
            label15.Location = new Point(13, 184);
            label15.Name = "label15";
            label15.Size = new Size(20, 23);
            label15.TabIndex = 12;
            label15.Text = "Z";
            // 
            // tbEscalaZ
            // 
            tbEscalaZ.AutoSize = false;
            tbEscalaZ.Location = new Point(32, 185);
            tbEscalaZ.Maximum = 50;
            tbEscalaZ.Minimum = -50;
            tbEscalaZ.Name = "tbEscalaZ";
            tbEscalaZ.Size = new Size(199, 34);
            tbEscalaZ.TabIndex = 11;
            tbEscalaZ.TickStyle = TickStyle.None;
            tbEscalaZ.Scroll += tbEscalaZ_Scroll;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10F);
            label11.Location = new Point(13, 150);
            label11.Name = "label11";
            label11.Size = new Size(19, 23);
            label11.TabIndex = 10;
            label11.Text = "Y";
            // 
            // tbEscalaY
            // 
            tbEscalaY.AutoSize = false;
            tbEscalaY.Location = new Point(32, 151);
            tbEscalaY.Maximum = 50;
            tbEscalaY.Minimum = -50;
            tbEscalaY.Name = "tbEscalaY";
            tbEscalaY.Size = new Size(199, 34);
            tbEscalaY.TabIndex = 9;
            tbEscalaY.TickStyle = TickStyle.None;
            tbEscalaY.Scroll += tbEscalaY_Scroll;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10F);
            label12.Location = new Point(13, 118);
            label12.Name = "label12";
            label12.Size = new Size(20, 23);
            label12.TabIndex = 8;
            label12.Text = "X";
            // 
            // tbEscalaX
            // 
            tbEscalaX.AutoSize = false;
            tbEscalaX.Location = new Point(32, 119);
            tbEscalaX.Maximum = 50;
            tbEscalaX.Minimum = -50;
            tbEscalaX.Name = "tbEscalaX";
            tbEscalaX.Size = new Size(199, 34);
            tbEscalaX.TabIndex = 7;
            tbEscalaX.TickStyle = TickStyle.None;
            tbEscalaX.Scroll += tbEscalaX_Scroll;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10F);
            label13.Location = new Point(13, 11);
            label13.Name = "label13";
            label13.Size = new Size(56, 23);
            label13.TabIndex = 6;
            label13.Text = "Escala";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 12F);
            label14.Location = new Point(13, 85);
            label14.Name = "label14";
            label14.Size = new Size(20, 28);
            label14.TabIndex = 1;
            label14.Text = "*";
            // 
            // tbEscalaG
            // 
            tbEscalaG.AutoSize = false;
            tbEscalaG.Location = new Point(32, 86);
            tbEscalaG.Maximum = 50;
            tbEscalaG.Minimum = -50;
            tbEscalaG.Name = "tbEscalaG";
            tbEscalaG.Size = new Size(199, 34);
            tbEscalaG.TabIndex = 0;
            tbEscalaG.TickStyle = TickStyle.None;
            tbEscalaG.Scroll += tbEscalaG_Scroll;
            // 
            // panel5
            // 
            panel5.BackColor = Color.White;
            panel5.Controls.Add(button1);
            panel5.Location = new Point(1146, 640);
            panel5.Name = "panel5";
            panel5.Size = new Size(245, 60);
            panel5.TabIndex = 13;
            // 
            // button1
            // 
            button1.Location = new Point(13, 15);
            button1.Name = "button1";
            button1.Size = new Size(218, 29);
            button1.TabIndex = 0;
            button1.Text = "Reiniciar Transformações";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1403, 830);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbTransZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbTransY).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbTransX).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbRotacaoZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbRotacaoY).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbRotacaoX).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbEscalaZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbEscalaY).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbEscalaX).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbEscalaG).EndInit();
            panel5.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Button btn10x;
        private Button btn100x;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem abrirArquivoToolStripMenuItem;
        private Panel panel1;
        private Label label2;
        private Panel panel2;
        private TrackBar tbTransX;
        private Label label3;
        private Label label5;
        private TrackBar tbTransZ;
        private TrackBar trackBar2;
        private Label label4;
        private TrackBar tbTransY;
        private TrackBar trackBar1;
        private Label label6;
        private Panel panel3;
        private Label label7;
        private TrackBar tbRotacaoZ;
        private Label label8;
        private TrackBar tbRotacaoY;
        private Label label9;
        private Label label10;
        private TrackBar tbRotacaoX;
        private Panel panel4;
        private Label label15;
        private TrackBar tbEscalaZ;
        private Label label11;
        private TrackBar tbEscalaY;
        private Label label12;
        private TrackBar tbEscalaX;
        private Label label13;
        private Label label14;
        private TrackBar tbEscalaG;
        private Panel panel5;
        private Button button1;
    }
}

namespace kursovaya
{
    partial class FormDebug
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.pictureDebug = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDebug)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(371, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "высота";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(371, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "ширина";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(323, 140);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Прямоугольник";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(225, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Круг";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.Location = new System.Drawing.Point(225, 78);
            this.hScrollBar2.Maximum = 75;
            this.hScrollBar2.Minimum = 15;
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(124, 21);
            this.hScrollBar2.TabIndex = 9;
            this.hScrollBar2.Value = 15;
            this.hScrollBar2.ValueChanged += new System.EventHandler(this.hScrollBar2_ValueChanged);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(225, 20);
            this.hScrollBar1.Maximum = 75;
            this.hScrollBar1.Minimum = 15;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(124, 21);
            this.hScrollBar1.TabIndex = 8;
            this.hScrollBar1.Value = 15;
            this.hScrollBar1.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged);
            // 
            // pictureDebug
            // 
            this.pictureDebug.Location = new System.Drawing.Point(12, 12);
            this.pictureDebug.Name = "pictureDebug";
            this.pictureDebug.Size = new System.Drawing.Size(181, 151);
            this.pictureDebug.TabIndex = 7;
            this.pictureDebug.TabStop = false;
            this.pictureDebug.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureDebug_Paint);
            // 
            // FormDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 183);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.hScrollBar2);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.pictureDebug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormDebug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор формы частиц";
            ((System.ComponentModel.ISupportInitialize)(this.pictureDebug)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.PictureBox pictureDebug;
    }
}
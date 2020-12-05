namespace kursovaya
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picDisplay = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.speedBar = new System.Windows.Forms.TrackBar();
            this.startButton = new System.Windows.Forms.Button();
            this.stepButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).BeginInit();
            this.SuspendLayout();
            // 
            // picDisplay
            // 
            this.picDisplay.Location = new System.Drawing.Point(12, 12);
            this.picDisplay.Name = "picDisplay";
            this.picDisplay.Size = new System.Drawing.Size(776, 426);
            this.picDisplay.TabIndex = 0;
            this.picDisplay.TabStop = false;
            this.picDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picDisplay_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // speedBar
            // 
            this.speedBar.Location = new System.Drawing.Point(282, 475);
            this.speedBar.Maximum = 9;
            this.speedBar.Name = "speedBar";
            this.speedBar.Size = new System.Drawing.Size(134, 45);
            this.speedBar.TabIndex = 3;
            this.speedBar.Value = 1;
            this.speedBar.ValueChanged += new System.EventHandler(this.speedBar_ValueChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(31, 455);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(107, 38);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Запуск";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stepButton
            // 
            this.stepButton.Location = new System.Drawing.Point(31, 499);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(107, 38);
            this.stepButton.TabIndex = 5;
            this.stepButton.Text = "Шаг";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Click += new System.EventHandler(this.stepButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(148, 455);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(107, 38);
            this.stopButton.TabIndex = 9;
            this.stopButton.Text = "Стоп";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(148, 499);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(107, 38);
            this.backButton.TabIndex = 10;
            this.backButton.Text = "Шаг назад";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(499, 499);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.stepButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.speedBar);
            this.Controls.Add(this.picDisplay);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDisplay;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar speedBar;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stepButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label label1;
    }
}


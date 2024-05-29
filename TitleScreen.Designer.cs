namespace PokerDraw
{
    partial class TitleScreen
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
            this.BackgroundImage = new System.Windows.Forms.PictureBox();
            this.StartFor2PLayersButton = new System.Windows.Forms.Button();
            this.StartFor3PLayersButton = new System.Windows.Forms.Button();
            this.StartFor4PLayersButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundImage)).BeginInit();
            this.SuspendLayout();
            // 
            // BackgroundImage
            // 
            this.BackgroundImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackgroundImage.Image = global::PokerDraw.Properties.Resources.bg_title;
            this.BackgroundImage.Location = new System.Drawing.Point(0, 0);
            this.BackgroundImage.Name = "BackgroundImage";
            this.BackgroundImage.Size = new System.Drawing.Size(1264, 681);
            this.BackgroundImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.BackgroundImage.TabIndex = 6;
            this.BackgroundImage.TabStop = false;
            // 
            // StartFor2PLayersButton
            // 
            this.StartFor2PLayersButton.BackColor = System.Drawing.Color.Transparent;
            this.StartFor2PLayersButton.BackgroundImage = global::PokerDraw.Properties.Resources.big_button;
            this.StartFor2PLayersButton.FlatAppearance.BorderSize = 0;
            this.StartFor2PLayersButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.StartFor2PLayersButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.StartFor2PLayersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartFor2PLayersButton.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartFor2PLayersButton.ForeColor = System.Drawing.Color.White;
            this.StartFor2PLayersButton.Location = new System.Drawing.Point(270, 495);
            this.StartFor2PLayersButton.Name = "StartFor2PLayersButton";
            this.StartFor2PLayersButton.Size = new System.Drawing.Size(224, 64);
            this.StartFor2PLayersButton.TabIndex = 7;
            this.StartFor2PLayersButton.Text = "2 ИГРОКА";
            this.StartFor2PLayersButton.UseVisualStyleBackColor = false;
            this.StartFor2PLayersButton.Click += new System.EventHandler(this.StartFor2PLayersButton_Click);
            // 
            // StartFor3PLayersButton
            // 
            this.StartFor3PLayersButton.BackColor = System.Drawing.Color.Transparent;
            this.StartFor3PLayersButton.BackgroundImage = global::PokerDraw.Properties.Resources.big_button;
            this.StartFor3PLayersButton.FlatAppearance.BorderSize = 0;
            this.StartFor3PLayersButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.StartFor3PLayersButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.StartFor3PLayersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartFor3PLayersButton.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartFor3PLayersButton.ForeColor = System.Drawing.Color.White;
            this.StartFor3PLayersButton.Location = new System.Drawing.Point(521, 495);
            this.StartFor3PLayersButton.Name = "StartFor3PLayersButton";
            this.StartFor3PLayersButton.Size = new System.Drawing.Size(224, 64);
            this.StartFor3PLayersButton.TabIndex = 8;
            this.StartFor3PLayersButton.Text = "3 ИГРОКА";
            this.StartFor3PLayersButton.UseVisualStyleBackColor = false;
            this.StartFor3PLayersButton.Click += new System.EventHandler(this.StartFor3PLayersButton_Click);
            // 
            // StartFor4PLayersButton
            // 
            this.StartFor4PLayersButton.BackColor = System.Drawing.Color.Transparent;
            this.StartFor4PLayersButton.BackgroundImage = global::PokerDraw.Properties.Resources.big_button;
            this.StartFor4PLayersButton.FlatAppearance.BorderSize = 0;
            this.StartFor4PLayersButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.StartFor4PLayersButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.StartFor4PLayersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartFor4PLayersButton.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartFor4PLayersButton.ForeColor = System.Drawing.Color.White;
            this.StartFor4PLayersButton.Location = new System.Drawing.Point(772, 495);
            this.StartFor4PLayersButton.Name = "StartFor4PLayersButton";
            this.StartFor4PLayersButton.Size = new System.Drawing.Size(224, 64);
            this.StartFor4PLayersButton.TabIndex = 9;
            this.StartFor4PLayersButton.Text = "4 ИГРОКА";
            this.StartFor4PLayersButton.UseVisualStyleBackColor = false;
            this.StartFor4PLayersButton.Click += new System.EventHandler(this.StartFor4PLayersButton_Click);
            // 
            // TitleScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.StartFor4PLayersButton);
            this.Controls.Add(this.StartFor3PLayersButton);
            this.Controls.Add(this.StartFor2PLayersButton);
            this.Controls.Add(this.BackgroundImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TitleScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главный экран";
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox BackgroundImage;
        private System.Windows.Forms.Button StartFor2PLayersButton;
        private System.Windows.Forms.Button StartFor3PLayersButton;
        private System.Windows.Forms.Button StartFor4PLayersButton;
    }
}


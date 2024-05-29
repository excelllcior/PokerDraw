namespace PokerDraw
{
    partial class NameEntryScreen
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
            this.PlayerNumberLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.TextBoxImage = new System.Windows.Forms.PictureBox();
            this.NextButton = new System.Windows.Forms.Button();
            this.BackgroundImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.TextBoxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundImage)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayerNumberLabel
            // 
            this.PlayerNumberLabel.AutoSize = true;
            this.PlayerNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.PlayerNumberLabel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PlayerNumberLabel.ForeColor = System.Drawing.Color.White;
            this.PlayerNumberLabel.Location = new System.Drawing.Point(12, 9);
            this.PlayerNumberLabel.Name = "PlayerNumberLabel";
            this.PlayerNumberLabel.Size = new System.Drawing.Size(72, 19);
            this.PlayerNumberLabel.TabIndex = 0;
            this.PlayerNumberLabel.Text = "ИГРОК 1";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.BackColor = System.Drawing.Color.Transparent;
            this.NameLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameLabel.ForeColor = System.Drawing.Color.Transparent;
            this.NameLabel.Location = new System.Drawing.Point(12, 46);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(99, 16);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "ВВЕДИТЕ ИМЯ:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NameTextBox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameTextBox.ForeColor = System.Drawing.Color.Black;
            this.NameTextBox.Location = new System.Drawing.Point(28, 74);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(150, 20);
            this.NameTextBox.TabIndex = 2;
            // 
            // TextBoxImage
            // 
            this.TextBoxImage.BackColor = System.Drawing.Color.Transparent;
            this.TextBoxImage.Image = global::PokerDraw.Properties.Resources.textbox;
            this.TextBoxImage.Location = new System.Drawing.Point(15, 65);
            this.TextBoxImage.Name = "TextBoxImage";
            this.TextBoxImage.Size = new System.Drawing.Size(179, 38);
            this.TextBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.TextBoxImage.TabIndex = 24;
            this.TextBoxImage.TabStop = false;
            // 
            // NextButton
            // 
            this.NextButton.BackColor = System.Drawing.Color.Transparent;
            this.NextButton.BackgroundImage = global::PokerDraw.Properties.Resources.button;
            this.NextButton.FlatAppearance.BorderSize = 0;
            this.NextButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.NextButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.NextButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.NextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextButton.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.NextButton.ForeColor = System.Drawing.Color.White;
            this.NextButton.Location = new System.Drawing.Point(41, 116);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(125, 38);
            this.NextButton.TabIndex = 22;
            this.NextButton.Text = "СЛЕДУЮЩИЙ";
            this.NextButton.UseVisualStyleBackColor = false;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // BackgroundImage
            // 
            this.BackgroundImage.Image = global::PokerDraw.Properties.Resources.bg_title;
            this.BackgroundImage.Location = new System.Drawing.Point(0, 0);
            this.BackgroundImage.Name = "BackgroundImage";
            this.BackgroundImage.Size = new System.Drawing.Size(960, 511);
            this.BackgroundImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BackgroundImage.TabIndex = 23;
            this.BackgroundImage.TabStop = false;
            // 
            // NameEntryScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 166);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.TextBoxImage);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.PlayerNumberLabel);
            this.Controls.Add(this.BackgroundImage);
            this.Name = "NameEntryScreen";
            this.Text = "Ввод имени";
            this.Load += new System.EventHandler(this.NameEntryScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TextBoxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PlayerNumberLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.PictureBox BackgroundImage;
        private System.Windows.Forms.PictureBox TextBoxImage;
    }
}
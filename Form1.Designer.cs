namespace PokerDraw
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
            this.buttonToNextPlayer = new System.Windows.Forms.Button();
            this.buttonChangeDealer = new System.Windows.Forms.Button();
            this.labelPlayer = new System.Windows.Forms.Label();
            this.labelDealer = new System.Windows.Forms.Label();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.labelRound = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonToNextPlayer
            // 
            this.buttonToNextPlayer.Location = new System.Drawing.Point(441, 171);
            this.buttonToNextPlayer.Name = "buttonToNextPlayer";
            this.buttonToNextPlayer.Size = new System.Drawing.Size(75, 23);
            this.buttonToNextPlayer.TabIndex = 0;
            this.buttonToNextPlayer.Text = "Next Player";
            this.buttonToNextPlayer.UseVisualStyleBackColor = true;
            this.buttonToNextPlayer.Click += new System.EventHandler(this.buttonToNextPlayer_Click);
            // 
            // buttonChangeDealer
            // 
            this.buttonChangeDealer.Location = new System.Drawing.Point(441, 200);
            this.buttonChangeDealer.Name = "buttonChangeDealer";
            this.buttonChangeDealer.Size = new System.Drawing.Size(75, 23);
            this.buttonChangeDealer.TabIndex = 1;
            this.buttonChangeDealer.Text = "Next Dealer";
            this.buttonChangeDealer.UseVisualStyleBackColor = true;
            this.buttonChangeDealer.Click += new System.EventHandler(this.buttonChangeDealer_Click);
            // 
            // labelPlayer
            // 
            this.labelPlayer.AutoSize = true;
            this.labelPlayer.Location = new System.Drawing.Point(74, 96);
            this.labelPlayer.Name = "labelPlayer";
            this.labelPlayer.Size = new System.Drawing.Size(36, 13);
            this.labelPlayer.TabIndex = 2;
            this.labelPlayer.Text = "Player";
            // 
            // labelDealer
            // 
            this.labelDealer.AutoSize = true;
            this.labelDealer.Location = new System.Drawing.Point(74, 109);
            this.labelDealer.Name = "labelDealer";
            this.labelDealer.Size = new System.Drawing.Size(67, 13);
            this.labelDealer.TabIndex = 3;
            this.labelDealer.Text = "Dealer Index";
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Location = new System.Drawing.Point(441, 229);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(75, 23);
            this.buttonStartGame.TabIndex = 4;
            this.buttonStartGame.Text = "Start Game";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // labelRound
            // 
            this.labelRound.AutoSize = true;
            this.labelRound.Location = new System.Drawing.Point(75, 122);
            this.labelRound.Name = "labelRound";
            this.labelRound.Size = new System.Drawing.Size(39, 13);
            this.labelRound.TabIndex = 5;
            this.labelRound.Text = "Round";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelRound);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.labelDealer);
            this.Controls.Add(this.labelPlayer);
            this.Controls.Add(this.buttonChangeDealer);
            this.Controls.Add(this.buttonToNextPlayer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonToNextPlayer;
        private System.Windows.Forms.Button buttonChangeDealer;
        private System.Windows.Forms.Label labelPlayer;
        private System.Windows.Forms.Label labelDealer;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Label labelRound;
    }
}


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
            this.labelGameTitle = new System.Windows.Forms.Label();
            this.buttonJoinTable = new System.Windows.Forms.Button();
            this.radioButton3Players = new System.Windows.Forms.RadioButton();
            this.radioButton2Players = new System.Windows.Forms.RadioButton();
            this.radioButton4Players = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelGameTitle
            // 
            this.labelGameTitle.AutoSize = true;
            this.labelGameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGameTitle.Location = new System.Drawing.Point(15, 9);
            this.labelGameTitle.Name = "labelGameTitle";
            this.labelGameTitle.Size = new System.Drawing.Size(85, 16);
            this.labelGameTitle.TabIndex = 0;
            this.labelGameTitle.Text = "Покер Дро";
            // 
            // buttonJoinTable
            // 
            this.buttonJoinTable.Location = new System.Drawing.Point(12, 139);
            this.buttonJoinTable.Name = "buttonJoinTable";
            this.buttonJoinTable.Size = new System.Drawing.Size(152, 23);
            this.buttonJoinTable.TabIndex = 1;
            this.buttonJoinTable.Text = "Присоединиться к столу";
            this.buttonJoinTable.UseVisualStyleBackColor = true;
            this.buttonJoinTable.Click += new System.EventHandler(this.buttonJoinTable_Click);
            // 
            // radioButton3Players
            // 
            this.radioButton3Players.AutoSize = true;
            this.radioButton3Players.Location = new System.Drawing.Point(6, 42);
            this.radioButton3Players.Name = "radioButton3Players";
            this.radioButton3Players.Size = new System.Drawing.Size(69, 17);
            this.radioButton3Players.TabIndex = 2;
            this.radioButton3Players.TabStop = true;
            this.radioButton3Players.Text = "3 игрока";
            this.radioButton3Players.UseVisualStyleBackColor = true;
            this.radioButton3Players.CheckedChanged += new System.EventHandler(this.radioButton3Players_CheckedChanged);
            // 
            // radioButton2Players
            // 
            this.radioButton2Players.AutoSize = true;
            this.radioButton2Players.Checked = true;
            this.radioButton2Players.Location = new System.Drawing.Point(6, 19);
            this.radioButton2Players.Name = "radioButton2Players";
            this.radioButton2Players.Size = new System.Drawing.Size(69, 17);
            this.radioButton2Players.TabIndex = 3;
            this.radioButton2Players.TabStop = true;
            this.radioButton2Players.Text = "2 игрока";
            this.radioButton2Players.UseVisualStyleBackColor = true;
            this.radioButton2Players.CheckedChanged += new System.EventHandler(this.radioButton2Players_CheckedChanged);
            // 
            // radioButton4Players
            // 
            this.radioButton4Players.AutoSize = true;
            this.radioButton4Players.Location = new System.Drawing.Point(6, 65);
            this.radioButton4Players.Name = "radioButton4Players";
            this.radioButton4Players.Size = new System.Drawing.Size(69, 17);
            this.radioButton4Players.TabIndex = 4;
            this.radioButton4Players.TabStop = true;
            this.radioButton4Players.Text = "4 игрока";
            this.radioButton4Players.UseVisualStyleBackColor = true;
            this.radioButton4Players.CheckedChanged += new System.EventHandler(this.radioButton4Players_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2Players);
            this.groupBox1.Controls.Add(this.radioButton4Players);
            this.groupBox1.Controls.Add(this.radioButton3Players);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 94);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Количество игроков";
            // 
            // TitleScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(177, 177);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonJoinTable);
            this.Controls.Add(this.labelGameTitle);
            this.Name = "TitleScreen";
            this.Text = "Главный экран";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelGameTitle;
        private System.Windows.Forms.Button buttonJoinTable;
        private System.Windows.Forms.RadioButton radioButton3Players;
        private System.Windows.Forms.RadioButton radioButton2Players;
        private System.Windows.Forms.RadioButton radioButton4Players;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}


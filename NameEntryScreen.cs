using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PokerDraw
{
    public partial class NameEntryScreen : Form
    {
        private int _numberOfPlayers;
        private List<string> _playerNames = new List<string>();

        public NameEntryScreen(int numberOfPlayers)
        {
            InitializeComponent();
            _numberOfPlayers = numberOfPlayers;
            PlayerNumberLabel.Parent = BackgroundImage;
            NameLabel.Parent = BackgroundImage;
            NameTextBox.Parent = BackgroundImage;
            TextBoxImage.Parent = BackgroundImage;
            NextButton.Parent = BackgroundImage;
        }

        private void NameEntryScreen_Load(object sender, EventArgs e)
        {
            PlayerNumberLabel.Text = $"Игрок: {_playerNames.Count + 1}";
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (_playerNames.Count < _numberOfPlayers)
            {
                string name = NameTextBox.Text.Trim();

                if (name.Length >= 3 && name.Length <= 15)
                {
                    _playerNames.Add(name);
                    NameTextBox.Text = "";
                    if (_playerNames.Count == _numberOfPlayers)
                    {
                        NameTextBox.Enabled = false;
                        PlayerNumberLabel.Text = "ИГРОКИ ГОТОВЫ";
                        NameLabel.Text = "ВСЕ ИМЕНА ВВЕДЕНЫ";
                        NextButton.Text = "НАЧАТЬ";
                    }
                    else
                        PlayerNumberLabel.Text = $"Игрок: {_playerNames.Count + 1}";
                }
                else
                    MessageBox.Show("Имя игрока должно содержать от 3 до 15 символов", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {
                PokerTableScreen form = new PokerTableScreen(_playerNames);
                form.Show();
                this.Close();
            }
        }
    }
}

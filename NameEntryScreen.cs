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
        }

        private void NameEntryScreen_Load(object sender, EventArgs e)
        {
            labelPlayerNumber.Text = $"Игрок: {_playerNames.Count + 1}";
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text.Trim();

            if (name.Length >= 3 && name.Length <= 15)
            {
                _playerNames.Add(name);
                textBoxName.Text = "";

                if (_playerNames.Count == _numberOfPlayers)
                {
                    textBoxName.Enabled = false;
                    buttonNext.Enabled = false;
                    buttonStart.Enabled = true;
                }
                else
                    labelPlayerNumber.Text = $"Игрок: {_playerNames.Count + 1}";
            }
            else
                MessageBox.Show("Имя игрока должно содержать от 3 до 15 символов", "Ошибка", MessageBoxButtons.OK);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {

        }
    }
}

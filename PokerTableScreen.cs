using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerDraw
{
    public partial class PokerTableScreen : Form
    {
        private readonly Table _table = new Table(10);

        public PokerTableScreen(List<string> playerNames)
        {
            InitializeComponent();

            foreach (string name in playerNames) 
            {
                _table.AddPlayer(name, 1000);
            }

            var playerGroupBoxes = new List<GroupBox>{
                groupBoxPlayer1, groupBoxPlayer2, groupBoxPlayer3, groupBoxPlayer4,
            };

            foreach (Control c in Controls)
            {
                if (c is GroupBox groupBox) 
                {
                    foreach (Control c2 in groupBox.Controls)
                    {
                        if (c2 is Button button)
                        {
                            button.Click += new EventHandler(UpdateUIOnClick);
                        }
                    }
                }
            }
        }

        private void UpdateUIOnClick(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            labelRound.Text = $"Раунд: {_table.CurrentGame.Round}";
            labelCurrentDealer.Text = $"Индекс дилера: {_table.DealerIndex}";
            labelCurrentPlayer.Text = $"Текущий игрок: {_table.GetPlayer(_table.CurrentPlayerIndex).Name}";
            labelPot.Text = $"Банк: {_table.CurrentGame.Pot}";

            var playerGroupBoxes = new List<GroupBox>{
                groupBoxPlayer1, groupBoxPlayer2, groupBoxPlayer3, groupBoxPlayer4,
            };

            for (int i = 0; i < _table.GetNumberOfPlayersInGame(); i++)
            {
                Player player = _table.GetPlayer(i);
                playerGroupBoxes[i].Visible = true;
                playerGroupBoxes[i].Controls[$"labelPlayer{i + 1}Name"].Text = $"Имя: {player.Name}";
                playerGroupBoxes[i].Controls[$"labelPlayer{i + 1}Bankroll"].Text = $"Баланс: {player.Bankroll}";
            }

            var currentPlayer = _table.GetPlayer(_table.CurrentPlayerIndex);

            labelCurrentPlayerName.Text = $"Ставка: {currentPlayer.Name}";
            labelCurrentPlayerBankroll.Text = $"Ставка: {currentPlayer.Bankroll}";
            labelCurrentPlayerBet.Text = $"Ставка: {currentPlayer.Bet}";
        }

        private void PokerTableScreen_Load(object sender, EventArgs e)
        {
            _table.AddGame();
            _table.StartCurrentGame();
            UpdateUI();
        }

        private void buttonFold_Click(object sender, EventArgs e)
        {

        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {

        }

        private void buttonBet_Click(object sender, EventArgs e)
        {

        }

        private void buttonCall_Click(object sender, EventArgs e)
        {

        }

        private void buttonRaise_Click(object sender, EventArgs e)
        {

        }

        private void buttonNextPlayer_Click(object sender, EventArgs e)
        {
            _table.SwitchToNextPlayerInGame();
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {

        }
    }
}

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
        private readonly int _numberOfPlayers;

        public PokerTableScreen(List<string> playerNames)
        {
            InitializeComponent();
            SubscribeOnClickRecursively(this);
            _numberOfPlayers = playerNames.Count;

            foreach (string name in playerNames) 
            {
                _table.AddPlayer(name, 1000);
            }

            var anteBarButtons = new List<Button> { buttonConfirmAnte, buttonCancelAnte };
            foreach (Button button in anteBarButtons)
            {
                button.Click += (s, e) => buttonNextPlayer.Enabled = true;
                button.Click += (s, e) =>
                {
                    foreach (var b in anteBarButtons)
                        b.Enabled = false;
                };
            }

            var actionBarButtons = new List<Button> { buttonFold, buttonCheck, buttonCall, buttonConfirmBet };
            foreach (Button button in actionBarButtons)
            {
                button.Click += (s, e) => {
                    DisableActionBarButtons();
                    buttonNextPlayer.Enabled = true;
                };
            }

            var betBarButtons = new List<Button> { buttonConfirmBet, buttonCancelBet };
            foreach (Button button in betBarButtons)
            {
                button.Click += (s, e) => buttonNextPlayer.Enabled = true;
            }
        }

        private void SubscribeOnClickRecursively(Control c)
        {
            if (c is Button)
            {
                c.Click += UpdateUIOnClick;
            }
            foreach (Control child in c.Controls)
            {
                SubscribeOnClickRecursively(child);
            }
        }

        // Методы для обновления элементов интерфейса
        private void UpdateUIOnClick(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            labelRound.Text = $"Раунд: {_table.GetCurrentGame().Round}";
            labelCurrentDealer.Text = $"Индекс дилера: {_table.CurrentDealerPosition}";
            labelCurrentPlayer.Text = $"Текущий игрок: {_table.GetPlayer(_table.CurrentPlayerPosition).Name}";
            labelPot.Text = $"Банк: {_table.GetCurrentGame().Pot}";

            var playerGroupBoxes = new List<GroupBox>{ 
                groupBoxPlayer1, groupBoxPlayer2, groupBoxPlayer3, groupBoxPlayer4
            };

            for (int i = 0; i < _numberOfPlayers; i++)
            {
                Player player = _table.GetPlayer(i);
                playerGroupBoxes[i].Visible = true;
                playerGroupBoxes[i].Controls[$"labelPlayer{i + 1}Name"].Text = $"Имя: {player.Name}";
                playerGroupBoxes[i].Controls[$"labelPlayer{i + 1}Bankroll"].Text = $"Баланс: {player.Bankroll}";

                if (i == _table.CurrentDealerPosition)
                    playerGroupBoxes[i].Text = "Дилер";
                else
                    playerGroupBoxes[i].Text = "";
            }

            var currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);

            labelCurrentPlayerName.Text = $"Ставка: {currentPlayer.Name}";
            labelCurrentPlayerBankroll.Text = $"Ставка: {currentPlayer.Bankroll}";
            labelCurrentPlayerBet.Text = $"Ставка: {currentPlayer.Bet}";
        }

        private void UpdateCurrentPlayerMoveLabel(string move)
        {
            var playerGroupBox = Controls[$"groupBoxPlayer{_table.CurrentPlayerPosition + 1}"];
            var playerMoveLabel = playerGroupBox.Controls[$"labelPlayer{_table.CurrentPlayerPosition + 1}Move"];
            playerMoveLabel.Text = $"Ход: {move}";
        }

        private void DisableActionBarButtons()
        {
            buttonFold.Enabled = false;
            buttonCheck.Enabled = false;
            buttonCall.Enabled = false;
            buttonBet.Enabled = false;
            buttonRaise.Enabled = false;
        }

        private void EnableActionBarButtons()
        {
            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);
            Game currentGame = _table.GetCurrentGame();

            buttonFold.Enabled = true;

            if (currentPlayer.Bet == currentGame.Bet)
            {
                buttonCheck.Enabled = true;
            }

            if (currentGame.Bet == _table.Ante && currentPlayer.Bankroll > 0
                && (currentGame.Round != 2 || _table.CurrentPlayerPosition == _table.CurrentDealerPosition)
                && (currentGame.Round != 5 || _table.CurrentPlayerPosition == _table.CurrentDealerPosition))
            {
                buttonBet.Enabled = true;
            }

            buttonCall.Text = "Колл";
            int amountToCall = currentGame.Bet - currentPlayer.Bet;
            if (currentPlayer.Bet < currentGame.Bet && currentPlayer.Bankroll >= amountToCall)
            {
                buttonCall.Text = $"Колл ({amountToCall})";
                buttonCall.Enabled = true;
            }

            int amountToRaise = amountToCall + 1;
            if (currentGame.Bet != _table.Ante && currentPlayer.Bet <= currentGame.Bet
                && currentPlayer.Bankroll > amountToRaise
                && (currentGame.Round != 2 || _table.CurrentPlayerPosition == _table.CurrentDealerPosition)
                && (currentGame.Round != 5 || _table.CurrentPlayerPosition == _table.CurrentDealerPosition))
            {
                buttonRaise.Enabled = true;
            }
        }

        // Загрузка формы
        private void PokerTableScreen_Load(object sender, EventArgs e)
        {
            groupBoxAnteBar.Visible = true;
            buttonStartGame.Enabled = false;
            _table.AddGame();
            _table.StartCurrentGame();
            UpdateUI();
        }

        // Кнопка для начала новой игры
        private void buttonStartGame_Click(object sender, EventArgs e)
        {

        }

        // Кнопка для передачи хода/логика наступления раундов
        private void buttonNextPlayer_Click(object sender, EventArgs e)
        {
            _table.SwitchToNextPlayerInGame();
            int round = _table.GetCurrentGame().Round;

            if (round == 0)
            {
                buttonConfirmAnte.Enabled = true;
                buttonCancelAnte.Enabled = true;
            }
            else if (round == 1)
            {
                groupBoxAnteBar.Visible = false;
                groupBoxActionBar.Visible = true;
                EnableActionBarButtons();
            }
        }

        // Кнопки для подтверждения/отмены внесения анте
        private void buttonConfirmAnte_Click(object sender, EventArgs e)
        {
            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);
            Game currentGame = _table.GetCurrentGame();
            currentPlayer.PlaceBet(_table.Ante);
            currentGame.IncreasePot(_table.Ante);
            currentGame.SetMaxBet(_table.Ante);
        }

        private void buttonCancelAnte_Click(object sender, EventArgs e)
        {
            _table.GetPlayer(_table.CurrentPlayerPosition).Fold();
        }

        // Кнопки для совершения хода
        private void buttonFold_Click(object sender, EventArgs e)
        {
            _table.GetPlayer(_table.CurrentPlayerPosition).Fold();
            UpdateCurrentPlayerMoveLabel("Фолд");
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            UpdateCurrentPlayerMoveLabel("Чек");
        }

        private void buttonCall_Click(object sender, EventArgs e)
        {
            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);
            Game currentGame = _table.GetCurrentGame();
            int amountToCall = _table.GetCurrentGame().Bet - currentPlayer.Bet;
            currentPlayer.PlaceBet(amountToCall);
            currentGame.IncreasePot(amountToCall);
            UpdateCurrentPlayerMoveLabel($"Колл ({amountToCall})");
        }

        private void buttonBet_Click(object sender, EventArgs e)
        {
            groupBoxBetBar.Text = "Введите сумму ставки";
            groupBoxBetBar.Visible = true;

            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);
            int minAmountToBet = 1;
            numericUpDownBet.Minimum = minAmountToBet;
            numericUpDownBet.Value = minAmountToBet;
            numericUpDownBet.Maximum = currentPlayer.Bankroll;
        }

        private void buttonRaise_Click(object sender, EventArgs e)
        {
            groupBoxBetBar.Text = "Введите сумму рейза";
            groupBoxBetBar.Visible = true;

            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);
            Game currentGame = _table.GetCurrentGame();
            int minAmountToRaise = currentGame.Bet - currentPlayer.Bet + 1;
            numericUpDownBet.Minimum = minAmountToRaise;
            numericUpDownBet.Value = minAmountToRaise;
            numericUpDownBet.Maximum = currentPlayer.Bankroll;
        }

        private void buttonConfirmBet_Click(object sender, EventArgs e)
        {
            groupBoxBetBar.Visible = false;
            groupBoxActionBar.Visible = true;

            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);
            Game currentGame = _table.GetCurrentGame();
            int amountToBetOrRaise = int.Parse(numericUpDownBet.Value.ToString());
            currentPlayer.PlaceBet(amountToBetOrRaise);
            currentGame.IncreasePot(amountToBetOrRaise);
            currentGame.SetMaxBet(currentPlayer.Bet);
            UpdateCurrentPlayerMoveLabel($"Колл ({amountToBetOrRaise})");
        }

        private void buttonCancelBet_Click(object sender, EventArgs e)
        {
            groupBoxBetBar.Visible = false;
            groupBoxActionBar.Visible = true;
        }
    }
}
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
            SubscribeOnClickRecursively(this);

            foreach (string name in playerNames) 
            {
                _table.AddPlayer(name, 1000);
            }

            var actionBarButtons = new List<Button> { 
                buttonFold, buttonCheck, buttonCall, buttonConfirmBet 
            };

            foreach (Button button in actionBarButtons) 
            {
                button.Click += (s, e) => buttonNextPlayer.Enabled = true;
                button.Click += (s, e) =>
                {
                    foreach (var b in actionBarButtons)
                        b.Enabled = false;
                };
            }

            var anteBarButtons = new List<Button> { buttonConfirmAnte, buttonCancelAnte };

            foreach (Button button in anteBarButtons)
            {
                button.Click += (s, e) => buttonNextPlayer.Enabled = true;
                button.Click += (s, e) => button.Enabled = false;
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
            labelCurrentDealer.Text = $"Индекс дилера: {_table.DealerIndex}";
            labelCurrentPlayer.Text = $"Текущий игрок: {_table.GetPlayer(_table.CurrentPlayerIndex).Name}";
            labelPot.Text = $"Банк: {_table.GetCurrentGame().Pot}";

            var playerGroupBoxes = new List<GroupBox>{ 
                groupBoxPlayer1, groupBoxPlayer2, groupBoxPlayer3, groupBoxPlayer4
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

        private void UpdateCurrentPlayerMoveLabel(string move)
        {
            var playerGroupBox = Controls[$"groupBoxPlayer{_table.CurrentPlayerIndex + 1}"];
            var playerMoveLabel = playerGroupBox.Controls[$"labelPlayer{_table.CurrentPlayerIndex + 1}Move"];
            playerMoveLabel.Text = $"Ход: {move}";
        }

        private void UpdateActionBarButtons()
        {
            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerIndex);
            Game currentGame = _table.GetCurrentGame();

            if (currentPlayer.Bet == currentGame.Bet)
            {
                buttonCheck.Enabled = true;
            }

            if (currentGame.Bet == _table.Ante && currentPlayer.Bankroll > 0
                && (currentGame.Round != 2 || _table.CurrentPlayerIndex == _table.DealerIndex)
                && (currentGame.Round != 5 || _table.CurrentPlayerIndex == _table.DealerIndex))
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
                && (currentGame.Round != 2 || _table.CurrentPlayerIndex == _table.DealerIndex)
                && (currentGame.Round != 5 || _table.CurrentPlayerIndex == _table.DealerIndex))
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
        }

        // Кнопки для подтверждения/отмены внесения анте
        private void buttonConfirmAnte_Click(object sender, EventArgs e)
        {
            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerIndex);
            Game currentGame = _table.GetCurrentGame();
            currentPlayer.PlaceBet(_table.Ante);
            currentGame.IncreasePot(_table.Ante);
            currentGame.SetMaxBet(_table.Ante);
        }

        private void buttonCancelAnte_Click(object sender, EventArgs e)
        {
            _table.GetPlayer(_table.CurrentPlayerIndex).Fold();
        }

        // Кнопки для совершения хода
        private void buttonFold_Click(object sender, EventArgs e)
        {
            _table.GetPlayer(_table.CurrentPlayerIndex).Fold();
            UpdateCurrentPlayerMoveLabel("Фолд");
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            UpdateCurrentPlayerMoveLabel("Чек");
        }

        private void buttonCall_Click(object sender, EventArgs e)
        {
            groupBoxBetBar.Text = "Введите сумму ставки";

            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerIndex);
            Game currentGame = _table.GetCurrentGame();
            int amountToCall = _table.GetCurrentGame().Bet - currentPlayer.Bet;
            currentPlayer.PlaceBet(amountToCall);
            currentGame.IncreasePot(amountToCall);
            UpdateCurrentPlayerMoveLabel($"Колл ({amountToCall})");
        }

        private void buttonBet_Click(object sender, EventArgs e)
        {
            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerIndex);
            int minAmountToBet = 1;
            numericUpDownBet.Minimum = minAmountToBet;
            numericUpDownBet.Value = minAmountToBet;
            numericUpDownBet.Maximum = currentPlayer.Bankroll;
        }

        private void buttonRaise_Click(object sender, EventArgs e)
        {
            groupBoxBetBar.Text = "Введите сумму рейза";

            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerIndex);
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

            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerIndex);
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
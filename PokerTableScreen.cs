using PokerDraw.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Resources;
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
            // Обработчики нажатия на кнопки для подтверждения/отмены внесения анте
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
            // Обработчики нажатия на кнопки для совершения хода
            var actionBarButtons = new List<Button> { buttonFold, buttonCheck, buttonCall, buttonConfirmBet };
            foreach (Button button in actionBarButtons)
            {
                button.Click += (s, e) => {
                    DisableActionBarButtons();
                    buttonNextPlayer.Enabled = true;
                };
            }
            // Обработчики нажатия на кнопки для подтверждения/отмены внесения ставки
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

            var playerPanels = new List<Panel>{ 
                panelPlayer1, panelPlayer2, panelPlayer3, panelPlayer4
            };

            for (int i = 0; i < _numberOfPlayers; i++)
            {
                Player player = _table.GetPlayer(i);
                playerPanels[i].Visible = true;
                playerPanels[i].Controls[$"labelPlayer{i + 1}Name"].Text = player.Name.ToUpper();
                playerPanels[i].Controls[$"labelPlayer{i + 1}Bankroll"].Text = $"{player.Bankroll}$".ToUpper();

                if (i == _table.CurrentDealerPosition)
                    playerPanels[i].Text = "Дилер";
                else
                    playerPanels[i].Text = "";
            }

            var currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);

            labelCurrentPlayerName.Text = $"Имя: {currentPlayer.Name}";
            labelCurrentPlayerBankroll.Text = $"Баланс: {currentPlayer.Bankroll}";
            labelCurrentPlayerBet.Text = $"Ставка: {currentPlayer.Bet}";

            UpdateCurrentPlayerCardsImages();
        }

        // Доделать
        private void UpdateCurrentPlayerCardsImages()
        {
            ResourceManager rm = Resources.ResourceManager;
            var cardImages = new List<PictureBox>{
                pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5
            };
            var currentPlayerPos = _table.CurrentPlayerPosition;
            var player = _table.GetPlayer(currentPlayerPos);

            if (player.Hand.GetNumberOfCards() > 0)
            {
                cardImages[0].Image = (Bitmap)rm.GetObject($"card_back");
                for (int i = 0; i < 5; i++)
                {
                    Card card = player.Hand.GetCard(i);
                    if (card.IsFaceUp)
                        cardImages[i].Image = (Bitmap)rm.GetObject($"{card.Suit}_{card.Rank}".ToLower());
                    else
                        cardImages[i].Image = (Bitmap)rm.GetObject($"card_back");
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    cardImages[i].Visible = false;
                }
            }
        }

        private void UpdateCurrentPlayerMoveLabel(string move)
        {
            var playerPanel = Controls[$"panelPlayer{_table.CurrentPlayerPosition + 1}"];
            var playerMoveLabel = playerPanel.Controls[$"labelPlayer{_table.CurrentPlayerPosition + 1}Move"];
            playerMoveLabel.Text = move.ToUpper();
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
            StartGame();
            UpdateUI();
        }

        // Кнопка для начала новой игры
        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        // Кнопка для передачи хода/логика наступления раундов
        private void buttonNextPlayer_Click(object sender, EventArgs e)
        {
            _table.SwitchToNextPlayerInGame();

            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);
            Game currentGame = _table.GetCurrentGame();
            int round = _table.GetCurrentGame().Round;

            if (round == 0)
            {
                panelAnte.Text = "Сделать вступительный взнос?";
                buttonConfirmAnte.Enabled = true;
                buttonCancelAnte.Enabled = true;

                if (_table.CurrentPlayerPosition == _table.CurrentDealerPosition)
                {
                    buttonConfirmAnte.Enabled = false;
                    buttonCancelAnte.Enabled = false;
                    panelAnte.Text = "Дилер делает вступительный взнос автоматически";
                    currentPlayer.PlaceBet(_table.Ante);
                    currentGame.IncreasePot(_table.Ante);
                    currentGame.SetMaxBet(_table.Ante);
                }
            }
            else if (round == 1)
            {
                panelAnte.Visible = false;
                panelAction.Visible = true;
                EnableActionBarButtons();
            }
            else if (round == 2)
            {
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
            panelBet.Text = "Введите сумму ставки";
            panelBet.Visible = true;

            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);
            int minAmountToBet = 1;
            numericUpDownBet.Minimum = minAmountToBet;
            numericUpDownBet.Value = minAmountToBet;
            numericUpDownBet.Maximum = currentPlayer.Bankroll;
        }

        private void buttonRaise_Click(object sender, EventArgs e)
        {
            panelBet.Text = "Введите сумму рейза";
            panelBet.Visible = true;

            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);
            Game currentGame = _table.GetCurrentGame();
            int minAmountToRaise = currentGame.Bet - currentPlayer.Bet + 1;
            numericUpDownBet.Minimum = minAmountToRaise;
            numericUpDownBet.Value = minAmountToRaise;
            numericUpDownBet.Maximum = currentPlayer.Bankroll;
        }

        private void buttonConfirmBet_Click(object sender, EventArgs e)
        {
            panelBet.Visible = false;
            panelAction.Visible = true;

            Player currentPlayer = _table.GetPlayer(_table.CurrentPlayerPosition);
            Game currentGame = _table.GetCurrentGame();
            int amountToBetOrRaise = int.Parse(numericUpDownBet.Value.ToString());
            currentPlayer.PlaceBet(amountToBetOrRaise);
            currentGame.IncreasePot(amountToBetOrRaise);
            currentGame.SetMaxBet(currentPlayer.Bet);

            if (panelBet.Text == "Введите сумму рейза")
                UpdateCurrentPlayerMoveLabel($"Рейз ({amountToBetOrRaise})");
            else
                UpdateCurrentPlayerMoveLabel($"Бет ({amountToBetOrRaise})");
        }

        private void buttonCancelBet_Click(object sender, EventArgs e)
        {
            panelBet.Visible = false;
            panelAction.Visible = true;
        }

        public void StartGame()
        {
            panelAnte.Visible = true;
            buttonStartGame.Enabled = false;
            _table.AddGame();
            _table.StartCurrentGame();
        }
    }
}
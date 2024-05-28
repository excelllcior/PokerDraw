using PokerDraw.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private readonly Table Table = new Table(10);
        private readonly int NumberOfPlayers;

        public PokerTableScreen(List<string> playerNames)
        {
            InitializeComponent();
            CreateUI();

            NumberOfPlayers = playerNames.Count;
            foreach (string name in playerNames)
            {
                Table.AddPlayer(name, 1000);
            }
            var buttons = new List<Button> {
                AnteCancelButton, AnteConfirmButton, DealCardsButton, NextButton, 
                ConfirmCardsChangeButton, HideCardsButton,
                FoldButton, CheckButton, CallButton, BetButton, RaiseButton, 
                BetCancelButton, BetConfirmButton
            };
            foreach (Button button in buttons)
            {
                button.Click += UpdateUIOnClick;
            }
        }

        private void CreateUI()
        {
            int formWidth = BackgroundImage.Width;
            // Кнопка - Начать игру
            StartGameButton.Parent = BackgroundImage;
            StartGameButton.Location = new Point((formWidth - StartGameButton.Width) / 2, 612);
            // Кнопка - Раздать карты
            DealCardsButton.Parent = BackgroundImage;
            DealCardsButton.Location = new Point((formWidth - DealCardsButton.Width) / 2, 612);
            // Кнопка - Подтвердить замену карт
            HideCardsButton.Parent = BackgroundImage;
            HideCardsButton.Location = new Point((formWidth - HideCardsButton.Width) / 2, 612);
            // Кнопка - Подтвердить замену карт
            ConfirmCardsChangeButton.Parent = BackgroundImage;
            ConfirmCardsChangeButton.Location = new Point(formWidth - ConfirmCardsChangeButton.Width - 20, 612);
            // Кнопка - Передать ход
            NextButton.Parent = BackgroundImage;
            NextButton.Location = new Point(formWidth - NextButton.Width - 20, 612);
            // Панель - Выбор хода
            MoveSelectionPanel.Parent = BackgroundImage;
            MoveSelectionPanel.Location = new Point((formWidth - MoveSelectionPanel.Width) / 2, 610);
            // Панель - Анте
            AntePanel.Parent = BackgroundImage;
            AntePanel.Location = new Point((formWidth - AntePanel.Width) / 2, 610);
            // Панель - Замена карт
            ChangeCardsPanel.Parent = BackgroundImage;
            ChangeCardsPanel.Location = new Point((formWidth - ChangeCardsPanel.Width) / 2, 610);
            // Лейблы - Банк игры
            PotLabel.Parent = PotImage;
            PotLabel.Location = new Point(0, 0);
            PotValueLabel.Parent = PotImage;
            PotValueLabel.Location = new Point(0, 26);
            // Картинка - Банк игры
            PotImage.Parent = BackgroundImage;
            PotImage.Location = new Point((formWidth - PotImage.Width) / 2, 360);
            // Лейблы - Раунд
            RoundNameLabel.Parent = RoundImage;
            RoundNameLabel.Location = new Point(0, 0);
            RoundInfoLabel.Parent = RoundImage;
            RoundInfoLabel.Location = new Point(0, 26);
            // Картинка - Раунд
            RoundImage.Parent = BackgroundImage;
            RoundImage.Location = new Point((formWidth - RoundImage.Width) / 2, 260);
            // Лейблы - Панель игрока
            var playerImages = new List<PictureBox> {
                Player1Image, Player2Image, Player3Image, Player4Image
            };
            var playerNameLables = new List<Label> {
                Player1NameLabel, Player2NameLabel, Player3NameLabel, Player4NameLabel
            };
            var playerBankrollLables = new List<Label> {
                Player1BankrollLabel, Player2BankrollLabel, Player3BankrollLabel, Player4BankrollLabel
            };
            var playerMoveLables = new List<Label> {
                Player1MoveLabel, Player2MoveLabel, Player3MoveLabel, Player4MoveLabel
            };
            var playerDealerImages = new List<PictureBox> {
                Player1DealerImage, Player2DealerImage, Player3DealerImage, Player4DealerImage
            };
            var playerCrossImages = new List<PictureBox> { 
                Player1CrossImage, Player2CrossImage, Player3CrossImage, Player4CrossImage
            };
            for (int i = 0; i < 4; i++)
            {
                playerCrossImages[i].Parent = playerImages[i];
                playerCrossImages[i].Location = new Point(19, 36);
                playerDealerImages[i].Parent = playerImages[i];
                playerDealerImages[i].Location = new Point(19, 36);
                playerNameLables[i].Parent = playerImages[i];
                playerNameLables[i].Location = new Point(0, 0);
                playerBankrollLables[i].Parent = playerImages[i];
                playerBankrollLables[i].Location = new Point(0, 24);
                playerMoveLables[i].Parent = playerImages[i];
                playerMoveLables[i].Location = new Point(0, 50);
            }
            Player1Image.Parent = BackgroundImage;
            Player1Image.Location = new Point(formWidth - Player1Image.Width - 180, 320);
            Player2Image.Parent = BackgroundImage;
            Player2Image.Location = new Point(formWidth - Player2Image.Width - 340, 485);
            Player3Image.Parent = BackgroundImage;
            Player3Image.Location = new Point(340, 485);
            Player4Image.Parent = BackgroundImage;
            Player4Image.Location = new Point(180, 320);
            // Картинки - Карты игроков
            var player1CardImages = new List<PictureBox> {
                Player1Card1Image, Player1Card2Image, Player1Card3Image, Player1Card4Image, Player1Card5Image
            };
            for (int i = 4; i >= 0; i--)
            {
                player1CardImages[i].Parent = BackgroundImage;
                player1CardImages[i].Location = new Point(870 + i * 30, 258);
            }
            var player2CardImages = new List<PictureBox> {
                Player2Card1Image, Player2Card2Image, Player2Card3Image, Player2Card4Image, Player2Card5Image
            };
            for (int i = 4; i >= 0; i--)
            {
                player2CardImages[i].Parent = BackgroundImage;
                player2CardImages[i].Location = new Point(710 + i * 30, 420);
            }
            var player3CardImages = new List<PictureBox> {
                Player3Card1Image, Player3Card2Image, Player3Card3Image, Player3Card4Image, Player3Card5Image
            };
            for (int i = 4; i >= 0; i--)
            {
                player3CardImages[i].Parent = BackgroundImage;
                player3CardImages[i].Location = new Point(360 + i * 30, 420);
            }
            var player4CardImages = new List<PictureBox> {
                Player4Card1Image, Player4Card2Image, Player4Card3Image, Player4Card4Image, Player4Card5Image
            };
            for (int i = 4; i >= 0; i--)
            {
                player4CardImages[i].Parent = BackgroundImage;
                player4CardImages[i].Location = new Point(200 + i * 30, 258);
            }
        }

        private void UpdateUIOnClick(object sender, EventArgs e)
        {
            UpdatePlayerAndTableImages();
            UpdateButtons();
        }

        private void UpdatePlayerAndTableImages()
        {
            ResourceManager rm = Resources.ResourceManager;
            PotValueLabel.Text = $"${Table.GetCurrentGame().Pot}";
            var playerImages = new List<PictureBox>{
                Player1Image, Player2Image, Player3Image, Player4Image
            };
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                Player player = Table.GetPlayer(i);
                PictureBox playerImage = playerImages[i];
                playerImage.Visible = true;
                Label nameLabel = (Label)playerImage.Controls[$"Player{i + 1}NameLabel"];
                Label bankrollLabel = (Label)playerImage.Controls[$"Player{i + 1}BankrollLabel"];
                Label moveLabel = (Label)playerImage.Controls[$"Player{i + 1}MoveLabel"];
                nameLabel.Text = player.Name.ToUpper();
                bankrollLabel.Text = $"{player.Bankroll}$".ToUpper();
                PictureBox dealerImage = (PictureBox)playerImage.Controls[$"Player{i + 1}DealerImage"];
                PictureBox crossImage = (PictureBox)playerImage.Controls[$"Player{i + 1}CrossImage"];

                if (player.IsInGame)
                {
                    nameLabel.ForeColor = Color.White;
                    bankrollLabel.ForeColor = Color.White;
                    moveLabel.ForeColor = Color.PaleGoldenrod;
                    if (i == Table.CurrentPlayerPosition)
                        playerImage.Image = (Bitmap)rm.GetObject("player_panel_selected");
                    else
                        playerImage.Image = (Bitmap)rm.GetObject("player_panel");
                }
                else
                {
                    nameLabel.ForeColor = Color.Silver;
                    bankrollLabel.ForeColor = Color.Silver;
                    moveLabel.ForeColor = Color.Silver;
                    playerImage.Image = (Bitmap)rm.GetObject("player_panel_folded");
                    if (player.Bankroll < Table.Ante)
                    {
                        moveLabel.Text = "ВЫБЫЛ";
                        crossImage.Visible = true;
                    }
                }

                if (i == Table.CurrentDealerPosition)
                    dealerImage.Visible = true;
                else
                    dealerImage.Visible = false;
            }
            if (NumberOfPlayers >= 2)
            {
                var player1CardImages = new List<PictureBox> {
                    Player1Card1Image, Player1Card2Image, Player1Card3Image, Player1Card4Image, Player1Card5Image
                };
                UpdatePlayerCardsImages(player1CardImages, 0);
                var player2CardImages = new List<PictureBox> {
                    Player2Card1Image, Player2Card2Image, Player2Card3Image, Player2Card4Image, Player2Card5Image
                };
                UpdatePlayerCardsImages(player2CardImages, 1);

                if (NumberOfPlayers >= 3)
                {
                    var player3CardImages = new List<PictureBox> {
                         Player3Card1Image, Player3Card2Image, Player3Card3Image, Player3Card4Image, Player3Card5Image
                    };
                    UpdatePlayerCardsImages(player3CardImages, 2);

                    if (NumberOfPlayers == 4)
                    {
                        var player4CardImages = new List<PictureBox> {
                            Player4Card1Image, Player4Card2Image, Player4Card3Image, Player4Card4Image, Player4Card5Image
                        };
                        UpdatePlayerCardsImages(player4CardImages, 3);
                    }
                }
            }
        }

        private void UpdatePlayerCardsImages(List<PictureBox> playerCardImages, int playerPosition)
        {
            ResourceManager rm = Resources.ResourceManager;
            for (int i = 4; i >= 0; i--)
            {
                Player player = Table.GetPlayer(playerPosition);
                if (player.Hand.GetNumberOfCards() > 0)
                {
                    playerCardImages[i].Visible = true;
                    Card card = player.Hand.GetCard(i);
                    if (card.IsFaceUp)
                        playerCardImages[i].Image = (Bitmap)rm.GetObject($"{card.Suit}_{card.Rank}".ToLower());
                    else
                        playerCardImages[i].Image = (Bitmap)rm.GetObject("card_back");
                }
                else
                    playerCardImages[i].Visible = false;
            }
        }

        private void UpdateButtons()
        {
            ResourceManager rm = Resources.ResourceManager;
            var buttons = new List<Button> {
                AnteCancelButton, AnteConfirmButton, DealCardsButton, NextButton,
                HideCardsButton, ConfirmCardsChangeButton,
                FoldButton, CheckButton, CallButton, BetButton, RaiseButton, ChangeCard1Button,
                ChangeCard2Button, ChangeCard3Button, ChangeCard4Button, ChangeCard5Button
            };
            foreach (var button in buttons)
            {
                if (button.Enabled)
                    button.BackgroundImage = (Bitmap)rm.GetObject("button");
                else
                    button.BackgroundImage = (Bitmap)rm.GetObject("button_disabled");
            }
        }

        private void UpdateCurrentPlayerMoveLabel(string move)
        {
            var playerImage = BackgroundImage.Controls[$"Player{Table.CurrentPlayerPosition + 1}Image"];
            var playerMoveLabel = playerImage.Controls[$"Player{Table.CurrentPlayerPosition + 1}MoveLabel"];
            playerMoveLabel.Text = move.ToUpper();
        }

        private void UpdateOnGameStart()
        {
            if (Table.GetNumberOfEliminatedPLayers() < NumberOfPlayers - 1)
            {
                RoundNameLabel.Text = "ВСТУПИТЕЛЬНЫЙ ВЗНОС";
                RoundInfoLabel.Text = $"ДЛЯ УЧАСТИЯ В ИГРЕ НЕОБХОДИМО ВНЕСТИ ВСТУПИТЕЛЬНЫЙ ВЗНОС ${Table.Ante}";
                StartGameButton.Visible = false;
                NextButton.Visible = true;
                AnteConfirmButton.Enabled = true;
                AnteCancelButton.Enabled = true;
                AntePanel.Visible = true;
                Table.AddGame();
                Table.StartCurrentGame();
                Game currentGame = Table.GetCurrentGame();
                var playerImages = new List<PictureBox> {
                    Player1Image, Player2Image, Player3Image, Player4Image
                };
                for (int i = 0; i < NumberOfPlayers; i++)
                {
                    Player player = Table.GetPlayer(i);
                    PictureBox playerImage = playerImages[i];
                    if (player.IsInGame)
                    {
                        playerImage.Controls[$"Player{i + 1}MoveLabel"].Text = "";
                    }
                }

                Player dealer = Table.GetPlayer(Table.CurrentDealerPosition);
                var dealerImage = BackgroundImage.Controls[$"Player{Table.CurrentDealerPosition + 1}Image"];
                var dealerMoveLabel = dealerImage.Controls[$"Player{Table.CurrentDealerPosition + 1}MoveLabel"];
                dealerMoveLabel.Text = "В игре".ToUpper();
                dealer.PlaceBet(Table.Ante);
                currentGame.IncreasePot(Table.Ante);
                currentGame.SetMaxBet(Table.Ante);
                UpdatePlayerAndTableImages();
                UpdateButtons();
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    "СТОЛ ЗАКРЫТ. ПРИЧИНА: ОСТАЛСЯ ТОЛЬКО ОДИН ИГРОК, СПОСОБНЫЙ ВНЕСТИ АНТЕ",
                    "ЗАКРЫТИЕ", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                    this.Close();
            }
        }

        private void PokerTableScreen_Load(object sender, EventArgs e)
        {
            UpdateOnGameStart();
        }

        private void DealCardsButton_Click(object sender, EventArgs e)
        {
            Table.Deck.Shuffle();
            Table.DealCardsToPlayersInGame();
            DealCardsButton.Visible = false;
            for (int i = 0; i < NumberOfPlayers; i++)
                Table.GetPlayer(i).Hand.SortCards();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (Table.GetNumberOfPlayersInGame() != 1)
            {
                NextButton.Enabled = false;
                Table.SwitchToNextPlayerInGame();

                Player currentPlayer = Table.GetPlayer(Table.CurrentPlayerPosition);
                int round = Table.GetCurrentGame().Round;

                if (round == 0)
                {
                    AnteConfirmButton.Enabled = true;
                    AnteCancelButton.Enabled = true;
                    if (Table.CurrentPlayerPosition == Table.CurrentDealerPosition)
                    {
                        AntePanel.Visible = false;
                        DealCardsButton.Visible = true;
                        NextButton.Enabled = true;
                    }
                }
                else if (round == 1)
                {
                    RoundNameLabel.Text = "ПЕРВЫЙ РАУНД СТАВОК";
                    RoundInfoLabel.Text = "СДЕЛАЙТЕ ХОД, ВЫБРАВ ОДНО ИЗ ДОСТУПНЫХ ДЕЙСТВИЙ";
                    MoveSelectionPanel.Visible = true;
                    currentPlayer.Hand.Show();
                    EnableMovePanelButtons();
                }
                else if (round == 2)
                {
                    currentPlayer.Hand.Show();
                    EnableMovePanelButtons();
                }
                else if (round == 3)
                {
                    RoundNameLabel.Text = "РАУНД ЗАМЕН";
                    RoundInfoLabel.Text = "ВЫБЕРИТЕ КАРТЫ, КОТОРЫЕ НЕОБХОДИМО ЗАМЕНИТЬ";
                    ChangeCardButtonsText();
                    ClearPlayerInGameMoveLabels();
                    currentPlayer.Hand.Show();
                    HideCardsButton.Visible = false;
                    ConfirmCardsChangeButton.Visible = true;
                    EnableChangeCardPanelButtons();
                    if (MoveSelectionPanel.Visible)
                        MoveSelectionPanel.Visible = false;
                    if (!ChangeCardsPanel.Visible)
                        ChangeCardsPanel.Visible = true;
                }
                else if (round == 4)
                {
                    RoundNameLabel.Text = "ВТОРОЙ РАУНД СТАВОК";
                    RoundInfoLabel.Text = "СДЕЛАЙТЕ ХОД, ВЫБРАВ ОДНО ИЗ ДОСТУПНЫХ ДЕЙСТВИЙ";
                    HideCardsButton.Visible = false;
                    MoveSelectionPanel.Visible = true;
                    currentPlayer.Hand.Show();
                    EnableMovePanelButtons();
                }
                else if (round == 5)
                {
                    currentPlayer.Hand.Show();
                    EnableMovePanelButtons();
                }
                else if (round == 6)
                {
                    ClearPlayerInGameMoveLabels();
                    MoveSelectionPanel.Visible = false;
                    NextButton.Visible = false;
                    StartGameButton.Visible = true;
                    for (int i = 0; i < NumberOfPlayers; i++)
                    {
                        Table.GetPlayer(i).Hand.Show();
                    }
                    Table.DetermineWinners();
                    RoundNameLabel.Text = "РАУНД «ФИНАЛЬНОЕ ОТКРЫТИЕ»";
                    RoundInfoLabel.Text = $"КОЛИЧЕСТВО ПОБЕДИТЕЛЕЙ: {Table.GetCurrentGame().GetNumberOfWinners()}";
                    var currentGame = Table.GetCurrentGame();
                    decimal numberOfWinners = currentGame.GetNumberOfWinners();
                    decimal prize = Math.Round(currentGame.Pot / numberOfWinners, MidpointRounding.ToEven);
                    for (int i = 0; i < currentGame.GetNumberOfWinners(); i++)
                    {
                        currentGame.GetWinner(i).IncreaseBankroll((int)prize);
                    }
                }
            }
            else
            {
                RoundNameLabel.Text = "РАУНД «ФИНАЛЬНОЕ ОТКРЫТИЕ»";
                RoundInfoLabel.Text = $"В ИГРЕ ОСТАЛСЯ ТОЛЬКО ОДИН ИГРОК";
                ClearPlayerInGameMoveLabels();
                AntePanel.Visible = false;
                MoveSelectionPanel.Visible = false;
                NextButton.Visible = false;
                StartGameButton.Visible = true;
                for (int i = 0; i < NumberOfPlayers; i++)
                {
                    Table.GetPlayer(i).Hand.Show();
                }
                Table.DetermineWinners();
                var currentGame = Table.GetCurrentGame();
                for (int i = 0; i < currentGame.GetNumberOfWinners(); i++)
                {
                    currentGame.GetWinner(i).IncreaseBankroll(currentGame.Pot);
                }
            }
        }

        private void AnteConfirmButton_Click(object sender, EventArgs e)
        {
            Player currentPlayer = Table.GetPlayer(Table.CurrentPlayerPosition);
            Game currentGame = Table.GetCurrentGame();
            currentPlayer.PlaceBet(Table.Ante);
            currentGame.IncreasePot(Table.Ante);
            currentGame.SetMaxBet(Table.Ante);
            UpdateCurrentPlayerMoveLabel("В игре");
            AnteConfirmButton.Enabled = false;
            AnteCancelButton.Enabled = false;
            NextButton.Enabled = true;
        }

        private void AnteCancelButton_Click(object sender, EventArgs e)
        {
            Table.GetPlayer(Table.CurrentPlayerPosition).Fold();
            UpdateCurrentPlayerMoveLabel("Не в игре");
            AnteConfirmButton.Enabled = false;
            AnteCancelButton.Enabled = false;
            NextButton.Enabled = true;
        }

        private void EnableMovePanelButtons()
        {
            DisableMovePanelButtons();
            Player currentPlayer = Table.GetPlayer(Table.CurrentPlayerPosition);
            Game currentGame = Table.GetCurrentGame();

            FoldButton.Enabled = true;

            if (currentPlayer.Bet == currentGame.Bet)
            {
                CheckButton.Enabled = true;
            }

            if ((currentGame.Bet == Table.Ante || (currentGame.Round == 4 && currentPlayer.Bet == currentGame.Bet)) 
                && currentPlayer.Bankroll > 0 && (currentGame.Round != 2) && (currentGame.Round != 5))
            {
                BetButton.Enabled = true;
            }

            CallButton.Text = "КОЛЛ";
            int amountToCall = currentGame.Bet - currentPlayer.Bet;
            if (currentPlayer.Bet < currentGame.Bet && currentPlayer.Bankroll >= amountToCall)
            {
                CallButton.Text = $"КОЛЛ ${amountToCall}";
                CallButton.Enabled = true;
            }

            int amountToRaise = amountToCall + 1;
            if (((currentGame.Round == 1 && currentGame.Bet != Table.Ante) 
                || (currentGame.Round == 4 && currentPlayer.Bet < currentGame.Bet))
                && currentPlayer.Bankroll > amountToRaise
                && (currentGame.Round != 2) && (currentGame.Round != 5))
            {
                RaiseButton.Enabled = true;
            }
        }

        private void DisableMovePanelButtons()
        {
            FoldButton.Enabled = false;
            CheckButton.Enabled = false;
            CallButton.Enabled = false;
            BetButton.Enabled = false;
            RaiseButton.Enabled = false;
        }

        private void FoldButton_Click(object sender, EventArgs e)
        {
            Table.GetPlayer(Table.CurrentPlayerPosition).Fold();
            UpdateCurrentPlayerMoveLabel("Фолд");
            Table.GetPlayer(Table.CurrentPlayerPosition).Hand.Hide();
            DisableMovePanelButtons();
            NextButton.Enabled = true;
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            UpdateCurrentPlayerMoveLabel("Чек");
            Table.GetPlayer(Table.CurrentPlayerPosition).Hand.Hide();
            DisableMovePanelButtons();
            NextButton.Enabled = true;
        }

        private void CallButton_Click(object sender, EventArgs e)
        {
            Player currentPlayer = Table.GetPlayer(Table.CurrentPlayerPosition);
            Game currentGame = Table.GetCurrentGame();
            int amountToCall = Table.GetCurrentGame().Bet - currentPlayer.Bet;
            currentPlayer.PlaceBet(amountToCall);
            currentGame.IncreasePot(amountToCall);
            UpdateCurrentPlayerMoveLabel($"Колл ${amountToCall}");
            Table.GetPlayer(Table.CurrentPlayerPosition).Hand.Hide();
            DisableMovePanelButtons();
            NextButton.Enabled = true;
        }

        private void BetButton_Click(object sender, EventArgs e)
        {
            MoveSelectionPanel.Visible = false;
            BetPanel.Visible = true;
            Player currentPlayer = Table.GetPlayer(Table.CurrentPlayerPosition);
            int minAmountToBet = 1;
            numericUpDownBet.Minimum = minAmountToBet;
            numericUpDownBet.Value = minAmountToBet;
            numericUpDownBet.Maximum = currentPlayer.Bankroll;
        }

        private void RaiseButton_Click(object sender, EventArgs e)
        {
            MoveSelectionPanel.Visible = false;
            BetPanel.Visible = true;
            Player currentPlayer = Table.GetPlayer(Table.CurrentPlayerPosition);
            Game currentGame = Table.GetCurrentGame();
            int minAmountToRaise = currentGame.Bet - currentPlayer.Bet + 1;
            numericUpDownBet.Minimum = minAmountToRaise;
            numericUpDownBet.Value = minAmountToRaise;
            numericUpDownBet.Maximum = currentPlayer.Bankroll;
        }

        private void BetConfirmButton_Click(object sender, EventArgs e)
        {
            BetPanel.Visible = false;
            MoveSelectionPanel.Visible = true;
            Player currentPlayer = Table.GetPlayer(Table.CurrentPlayerPosition);
            Game currentGame = Table.GetCurrentGame();
            int amountToBetOrRaise = int.Parse(numericUpDownBet.Value.ToString());
            if (currentGame.Bet == Table.Ante)
                UpdateCurrentPlayerMoveLabel($"БЕТ ${amountToBetOrRaise}");
            else
                UpdateCurrentPlayerMoveLabel($"РАЙЗ ${amountToBetOrRaise}");

            currentPlayer.PlaceBet(amountToBetOrRaise);
            currentGame.IncreasePot(amountToBetOrRaise);
            currentGame.SetMaxBet(currentPlayer.Bet);
            Table.GetPlayer(Table.CurrentPlayerPosition).Hand.Hide();
            DisableMovePanelButtons();
            NextButton.Enabled = true;
        }

        private void BetCancelButton_Click(object sender, EventArgs e)
        {
            NextButton.Enabled = true;
            BetPanel.Visible = false;
            MoveSelectionPanel.Visible = true;
        }

        private void EnableChangeCardPanelButtons()
        {
            ChangeCard1Button.Enabled = true;
            ChangeCard2Button.Enabled = true;
            ChangeCard3Button.Enabled = true;
            ChangeCard4Button.Enabled = true;
            ChangeCard5Button.Enabled = true;
        }

        private void ChangeCard(int cardPosition)
        {
            Player currentPlayer = Table.GetPlayer(Table.CurrentPlayerPosition);
            Card card = Table.Deck.DealTopCard();
            currentPlayer.Hand.ChangeCard(cardPosition, card);
            currentPlayer.Hand.GetCard(cardPosition).TurnFaceUp();
        }

        private void ChangeCardButtonsText()
        {
            var changeCardButtons = new List<Button> {
                ChangeCard1Button, ChangeCard2Button, ChangeCard3Button, ChangeCard4Button, ChangeCard5Button 
            };
            Player currentPlayer = Table.GetPlayer(Table.CurrentPlayerPosition);
            for (int i = 0; i < currentPlayer.Hand.GetNumberOfCards(); i++)
            {
                Card card = currentPlayer.Hand.GetCard(i);
                string text = "";
                switch (card.Rank)
                {
                    case Rank.Two:
                        text += "2";
                        break;
                    case Rank.Three:
                        text += "3";
                        break;
                    case Rank.Four:
                        text += "4";
                        break;
                    case Rank.Five:
                        text += "5";
                        break;
                    case Rank.Six:
                        text += "6";
                        break;
                    case Rank.Seven:
                        text += "7";
                        break;
                    case Rank.Eight:
                        text += "8";
                        break;
                    case Rank.Nine:
                        text += "9";
                        break;
                    case Rank.Ten:
                        text += "10";
                        break;
                    case Rank.Jack:
                        text += "Валет";
                        break;
                    case Rank.Queen:
                        text += "Дама";
                        break;
                    case Rank.King:
                        text += "Король";
                        break;
                    case Rank.Ace:
                        text += "Туз";
                        break;
                }
                switch (card.Suit)
                {
                    case Suit.Spades:
                        text += " ♠";
                        break;
                    case Suit.Clubs:
                        text += " ♣";
                        break;
                    case Suit.Diamonds:
                        text += " ♦";
                        break;
                    case Suit.Hearts:
                        text += " ♥";
                        break;
                }
                changeCardButtons[i].Text = text;
            }
        }

        private void ChangeCard1Button_Click(object sender, EventArgs e)
        {
            Player currentPlayer = Table.GetPlayer(Table.CurrentPlayerPosition);
            currentPlayer.Hand.GetCard(0);
            ChangeCard1Button.Enabled = false;
            UpdateButtons();
            ChangeCard(0);
        }

        private void ChangeCard2Button_Click(object sender, EventArgs e)
        {
            ChangeCard2Button.Enabled = false;
            UpdateButtons();
            ChangeCard(1);
        }

        private void ChangeCard3Button_Click(object sender, EventArgs e)
        {
            ChangeCard3Button.Enabled = false;
            UpdateButtons();
            ChangeCard(2);
        }

        private void ChangeCard4Button_Click(object sender, EventArgs e)
        {
            ChangeCard4Button.Enabled = false;
            UpdateButtons();
            ChangeCard(3);
        }

        private void ChangeCard5Button_Click(object sender, EventArgs e)
        {
            ChangeCard5Button.Enabled = false;
            UpdateButtons();
            ChangeCard(4);
        }

        private void HideCardsButton_Click(object sender, EventArgs e)
        {
            Player currentPlayer = Table.GetPlayer(Table.CurrentPlayerPosition);
            currentPlayer.Hand.Hide();
            NextButton.Enabled = true;
            HideCardsButton.Enabled = false;
            ConfirmCardsChangeButton.Visible = false;
        }

        private void ConfirmCardsChangeButton_Click(object sender, EventArgs e)
        {
            HideCardsButton.Enabled = true;
            HideCardsButton.Visible = true;
            ChangeCardsPanel.Visible = false;
            ConfirmCardsChangeButton.Visible = false;
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            UpdateOnGameStart();
        }

        private void ClearPlayerInGameMoveLabels()
        {
            var playerImages = new List<PictureBox>{
                Player1Image, Player2Image, Player3Image, Player4Image
            };
            for (int i = 0; i < NumberOfPlayers; i++) 
            {
                Player player = Table.GetPlayer(i);
                PictureBox playerImage = playerImages[i];
                if (player.IsInGame) 
                {
                    playerImage.Controls[$"Player{i + 1}MoveLabel"].Text = "";
                }
            }
        }
    }
}
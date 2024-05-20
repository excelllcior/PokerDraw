using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerDraw.MVVM.Model
{
    public class Table
    {
        private readonly List<Player> _players = new List<Player>();
        private readonly List<Game> _games = new List<Game>();
        public Deck Deck { get; } = new Deck();
        public Player CurrentPlayer;
        public Game CurrentGame;
        public int DealerPosition { get; private set; } = 0;
        public int Ante { get; private set; }

        public Table(int ante)
        {
            Ante = ante;
        }

        public int GetNumberOfPlayersInGame()
        {
            int count = 0;

            foreach (var player in _players) 
            {
                if (player.IsInGame) count++;
            }
            return count;
        }

        public Player GetPlayer(int index)
        {
            return _players[index];
        }

        public void AddPlayer(string name, int bankroll)
        {
            Player player = new Player(name, bankroll);
            _players.Add(player);
        }

        public void AddGame()
        {
            Game game = new Game();
            _games.Add(game);
            CurrentGame = game;
        }

        public void DealCardsToPlayersInPlay()
        {
            foreach (Player player in _players) 
            {
                if (player.IsInGame)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Card card = Deck.DealTopCard();
                        player.Hand.AddCard(card);
                    }
                }
            }
        }

        public void ChangeCurrentPlayerCards(List<int> indexes)
        {
            foreach (int index in indexes)
            {
                Card card = Deck.DealTopCard();
                CurrentPlayer.Hand.ChangeCard(index, card);
            }
        }

        public void SwitchToNextPlayerInGame()
        {
            int currentPlayerIndex = _players.IndexOf(CurrentPlayer);
            int lastPlayerIndex = _players.IndexOf(_players.Last());
            int currentLastPlayerIndex = DealerPosition - 1;

            if (currentLastPlayerIndex < 0)
                currentLastPlayerIndex = lastPlayerIndex;

            if (currentPlayerIndex != currentLastPlayerIndex)
            {
                if (currentPlayerIndex == lastPlayerIndex)
                    currentPlayerIndex = 0;
                else 
                    currentPlayerIndex++;

                CurrentPlayer = _players[currentPlayerIndex];
            }
            else
            {
                CurrentGame.IncreaseRound();
                CurrentPlayer = _players[DealerPosition];
            }

            if (!CurrentPlayer.IsInGame)
                SwitchToNextPlayerInGame();
        }

        public void SwitchToNextDealerInGame()
        {
            if (DealerPosition != _players.IndexOf(_players.Last()))
                DealerPosition++;
            else
                DealerPosition = 0;

            if (!_players[DealerPosition].IsInGame) 
                SwitchToNextDealerInGame();
        }

        public void StartCurrentGame()
        {
            foreach (Player player in _players)
            {
                if (player.Bankroll != 0 && !player.IsInGame)
                    player.ResetIsInGame();
            }

            if (_games.Count > 1)
                SwitchToNextDealerInGame();

            CurrentPlayer = _players[DealerPosition];
            Deck.ResetCards();
            Deck.Shuffle();
        }

        public void DetermineWinners()
        {
            Ranking highestRanking = Ranking.HighCard;
            List<Player> tempWinners = new List<Player>();

            foreach (Player player in _players)
            {
                if (player.IsInGame)
                {
                    player.Hand.SortCards();
                    player.Hand.Evaluate();

                    if (player.Hand.Ranking > highestRanking)
                    {
                        tempWinners.Clear();
                        tempWinners.Add(player);
                        highestRanking = player.Hand.Ranking;
                    }
                    else if (player.Hand.Ranking == highestRanking)
                        tempWinners.Add(player);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (tempWinners.Count > 1)
                {
                    CompareKickers(tempWinners, i);
                }
            }

            if (tempWinners.Count > 1)
            {
                foreach (Player player in tempWinners)
                {
                    CurrentGame.AddWinner(player);
                }
            }
        }

        private void CompareKickers(List<Player> tempWinners, int kickerIndex)
        {
            int highestKickerValue = 0;

            foreach (Player player in tempWinners)
            {
                Card kicker = player.Hand.GetCard(kickerIndex);
                int currentKickerRank = (int)kicker.Rank;

                if (currentKickerRank > highestKickerValue)
                {
                    tempWinners.Clear();
                    tempWinners.Add(player);
                    highestKickerValue = currentKickerRank;
                }
                else if (currentKickerRank == highestKickerValue)
                    tempWinners.Add(player);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace PokerDraw
{
    public class Table
    {
        private readonly List<Player> _players = new List<Player>();
        private readonly List<Game> _games = new List<Game>();
        public Deck Deck { get; } = new Deck();
        public int CurrentPlayerPosition;
        public int CurrentDealerPosition { get; private set; } = 0;
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

        public int GetNumberOfEliminatedPLayers()
        {
            int count = 0;

            foreach (var player in _players)
            {
                if (player.Bankroll < Ante) count++;
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

        public Game GetCurrentGame()
        {
            return _games.Last();
        }

        public void AddGame()
        {
            Game game = new Game();
            _games.Add(game);
        }

        public void DealCardsToPlayersInGame()
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (Player player in _players) 
                {
                    if (player.IsInGame)
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
                _players[CurrentPlayerPosition].Hand.ChangeCard(index, card);
            }
        }

        public void SwitchToNextPlayerInGame()
        {
            int lastPlayerIndex = _players.IndexOf(_players.Last());

            if (CurrentPlayerPosition != CurrentDealerPosition)
            {
                if (CurrentPlayerPosition == lastPlayerIndex)
                    CurrentPlayerPosition = 0;
                else 
                    CurrentPlayerPosition++;
            }
            else
            {
                _games.Last().IncreaseRound();

                if (CurrentDealerPosition + 1 > lastPlayerIndex)
                    CurrentPlayerPosition = 0;
                else
                    CurrentPlayerPosition = CurrentDealerPosition + 1;
            }

            if (!_players[CurrentPlayerPosition].IsInGame)
                SwitchToNextPlayerInGame();
        }

        private void SwitchToNextDealerInGame()
        {
            if (CurrentDealerPosition != _players.IndexOf(_players.Last()))
                CurrentDealerPosition++;
            else
                CurrentDealerPosition = 0;

            if (!_players[CurrentDealerPosition].IsInGame) 
                SwitchToNextDealerInGame();
        }

        public void StartCurrentGame()
        {
            Deck.ResetCards();
            foreach (Player player in _players)
            {
                player.Fold();
                if (player.Bankroll >= Ante)
                {
                    player.ResetBet();
                    player.ResetIsInGame();
                }
            }
            if (_games.Count > 1)
                SwitchToNextDealerInGame();

            int lastPlayerIndex = _players.IndexOf(_players.Last());
            if (CurrentDealerPosition + 1 > lastPlayerIndex)
                CurrentPlayerPosition = 0;
            else
                CurrentPlayerPosition = CurrentDealerPosition + 1;

            if (!_players[CurrentPlayerPosition].IsInGame) 
                SwitchToNextPlayerInGame();
        }

        public void DetermineWinners()
        {
            Ranking highestRanking = Ranking.HighCard;
            List<Player> tempWinners = new List<Player>();
            foreach (Player player in _players)
            {
                if (player.IsInGame)
                {
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
                int maxRank = 0;
                foreach (Player player in _players)
                {
                    if (player.Hand.Ranking == highestRanking && player.IsInGame)
                    {
                        Hand hand = player.Hand;
                        int rank = (int)hand.GetCard(i).Rank;
                        if (rank > maxRank)
                        {
                            tempWinners.Clear();
                            tempWinners.Add(player);
                            maxRank = rank;
                        }
                        else if (rank == maxRank)
                            tempWinners.Add(player);
                    }
                    CompareKickers(tempWinners, 0);
                }
                if (tempWinners.Count == 1)
                    break;
            }

            foreach (Player player in tempWinners)
            {   
                _games.Last().AddWinner(player);
            }
        }

        private void CompareKickers(List<Player> tempWinners, int kickerIndex)
        {
            int highestKickerValue = 0;
            var temp = new List<Player>();
            foreach (Player player in tempWinners)
            {
                Card kicker = player.Hand.GetCard(kickerIndex);
                int currentKickerRank = (int)kicker.Rank;

                if (currentKickerRank > highestKickerValue)
                {
                    temp.Clear();
                    temp.Add(player);
                    highestKickerValue = currentKickerRank;
                }
                else if (currentKickerRank == highestKickerValue)
                    temp.Add(player);
            }
        }
    }
}

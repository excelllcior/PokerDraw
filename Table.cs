﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerDraw
{
    public class Table
    {
        private readonly List<Player> _players = new List<Player>();
        private readonly List<Game> _games = new List<Game>();
        public Deck Deck { get; } = new Deck();
        public Game CurrentGame;
        public int CurrentPlayerIndex;
        public int DealerIndex { get; private set; } = 0;
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
                _players[CurrentPlayerIndex].Hand.ChangeCard(index, card);
            }
        }

        public void SwitchToNextPlayerInGame()
        {
            int lastPlayerIndex = _players.IndexOf(_players.Last());

            if (CurrentPlayerIndex != DealerIndex)
            {
                if (CurrentPlayerIndex == lastPlayerIndex)
                    CurrentPlayerIndex = 0;
                else 
                    CurrentPlayerIndex++;
            }
            else{
                CurrentGame.IncreaseRound();

                if (DealerIndex + 1 == lastPlayerIndex)
                    CurrentPlayerIndex = 0;
                else
                    CurrentPlayerIndex = DealerIndex + 1;
            }

            if (!_players[CurrentPlayerIndex].IsInGame)
                SwitchToNextPlayerInGame();
        }

        private void SwitchToNextDealerInGame()
        {
            if (DealerIndex != _players.IndexOf(_players.Last()))
                DealerIndex++;
            else
                DealerIndex = 0;

            if (!_players[DealerIndex].IsInGame) 
                SwitchToNextDealerInGame();
        }

        public void StartCurrentGame()
        {
            Deck.ResetCards();

            foreach (Player player in _players)
            {
                if (player.Bankroll != 0)
                {
                    player.Hand.Clear();
                    player.ResetBet();
                    player.ResetIsInGame();
                }
            }

            if (_games.Count > 1)
                SwitchToNextDealerInGame();

            CurrentPlayerIndex = DealerIndex;
            _players[CurrentPlayerIndex].PlaceBet(Ante);
            CurrentGame.IncreasePot(Ante);
            CurrentGame.SetMaxBet(Ante);
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

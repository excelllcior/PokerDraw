using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int CurrentPlayerIndex = _players.IndexOf(CurrentPlayer);

            if (CurrentPlayerIndex != _players.Count - 1)
            {
                CurrentPlayerIndex++;
                CurrentPlayer = _players[CurrentPlayerIndex];
            }
            else
            {
                CurrentGame.IncreaseRound();
                CurrentPlayer = _players[0];
            }

            if (!CurrentPlayer.IsInGame)
                SwitchToNextPlayerInGame();
        }

        public void SwitchToNextDealerInGame()
        {
            if (DealerPosition != _players.Count - 1)
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
            List<Player> winners = new List<Player>();

            foreach (Player player in _players)
            {
                if (player.IsInGame)
                {
                    player.Hand.Evaluate();
                    if (player.Hand.Ranking > highestRanking)
                    {
                        winners.Clear();
                        winners.Add(player);
                        highestRanking = player.Hand.Ranking;
                    }
                    else if (player.Hand.Ranking == highestRanking)
                    {
                        winners.Add(player);
                    }
                }
            }

            foreach (Player winner in winners) 
            {
                CurrentGame.AddWinner(winner);
            }
        }
    }
}

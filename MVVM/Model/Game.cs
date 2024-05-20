using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerDraw.MVVM.Model
{
    public class Game
    {
        public int Pot { get; private set; } = 0;
        public int Bet { get; private set; } = 0;
        public int Round { get; private set; } = 0;
        private readonly List<Player> _winners = new List<Player>();

        public int GetNumberOfWinners()
        {
            return _winners.Count;
        }

        public Player GetWinner(int index)
        {
            return _winners[index];
        }

        public void AddWinner(Player player)
        {
            _winners.Add(player);
        }

        public void IncreasePot(int amount)
        {
            Pot += amount;
        }

        public void SetMaxBet(int playersBet)
        {
            if (Bet < playersBet)
            {
                Bet = playersBet;
            }
        }

        public void IncreaseRound()
        {
            Round++;
        }
    }
}

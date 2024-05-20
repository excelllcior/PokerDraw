using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerDraw.MVVM.Model
{
    public class Player
    {
        public string Name { get; private set; }
        public Hand Hand { get; private set; } = new Hand();
        public int Bankroll { get; private set; }
        public int Bet { get; private set; } = 0;
        public bool IsInGame { get; private set; } = true;

        public Player(string name, int bankroll)
        {
            Name = name;
            Bankroll = bankroll;
        }

        public void ResetIsInGame()
        {
            IsInGame = true;
        }

        public void Fold()
        {
            IsInGame = false;
            Hand.Clear();
        }

        public void ResetBet()
        {
            Bet = 0;
        }

        public void IncreaseBankroll(int amount) 
        {
            Bankroll += amount;
        }

        public void PlaceBet(int amount)
        {
            Bet += amount;
            Bankroll -= amount;
        }
    }
}
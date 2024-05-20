using System;
using System.Collections.Generic;

namespace PokerDraw.MVVM.Model
{
    public class Deck
    {
        private readonly List<Card> _cards = new List<Card>();

        public Deck()
        {
            ResetCards();
        }

        public void ResetCards()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    Card card = new Card(rank, suit);
                    _cards.Add(card);
                }
            }
        }

        public void Shuffle()
        {
            Random random = new Random();
            int n = _cards.Count;

            while (n > 0)
            {
                n--;
                int k = random.Next(n + 1);
                (_cards[n], _cards[k]) = (_cards[k], _cards[n]);
            }
        }

        public Card DealTopCard()
        {
            if (_cards.Count != 0)
            {
                Card card = _cards[0];
                _cards.RemoveAt(0);
                return card;
            }
            else
                throw new Exception("Невозможно выдать карту. Колода пуста.");
        }
    }
}
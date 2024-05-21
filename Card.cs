using System;

namespace PokerDraw
{
    public enum Suit
    {
        Spades,
        Clubs,
        Hearts,
        Diamonds
    }

    public enum Rank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public class Card
    {
        public Rank Rank { get; }
        public Suit Suit { get; }
        public bool IsFaceUp { get; private set; } = false;

        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public void TurnFaceUp()
        {
            IsFaceUp = true;
        }

        public void TurnFaceDown()
        {
            IsFaceUp = false;
        }
    }
}
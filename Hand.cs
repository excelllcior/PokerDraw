using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerDraw
{
    public enum Ranking
    {
        HighCard = 1,
        Pair,
        TwoPairs,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    public class Hand
    {
        private readonly List<Card> _cards = new List<Card>();
        public Ranking Ranking { get; private set; }

        public int GetNumberOfCards()
        {
            return _cards.Count;
        }

        public Card GetCard(int index)
        {
            return _cards[index];
        }

        public void Show()
        {
            foreach (Card card in _cards)
                card.TurnFaceUp();
        }

        public void Hide() 
        {
            foreach(Card card in _cards)
                card.TurnFaceDown();
        }

        public void AddCard(Card card)
        {
            if (_cards.Count < 5) 
                _cards.Add(card);
            else 
                throw new Exception("Невозможно добавить карту. В руке не может быть больше 5 карт.");
        }

        public void ChangeCard(int index, Card newCard)
        {
            _cards[index] = newCard;
        }

        public void SortCards()
        {
            var sortedCards = new List<Card>();
            sortedCards = _cards.OrderByDescending(c => c.Rank).ToList();
            _cards.Clear();
            foreach (Card card in sortedCards)
            {
                _cards.Add(card);
            }
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public void Evaluate()
        {
            if (IsRoyalFlush())
                Ranking = Ranking.RoyalFlush;
            else if (IsStraightFlush())
                Ranking = Ranking.StraightFlush;
            else if (IsFourOfAKind())
                Ranking = Ranking.FourOfAKind;
            else if (IsFullHouse())
                Ranking = Ranking.FullHouse;
            else if (IsFlush())
                Ranking = Ranking.Flush;
            else if (IsStraight())
                Ranking = Ranking.Straight;
            else if (IsThreeOfAKind())
                Ranking = Ranking.ThreeOfAKind;
            else if (IsTwoPair())
                Ranking = Ranking.TwoPairs;
            else if (IsPair())
                Ranking = Ranking.Pair;
            else
                Ranking = Ranking.HighCard;
        }

        private bool IsPair()
        {
            return _cards.GroupBy(c => c.Rank).Count(group => group.Count() == 2) == 1;
        }

        private bool IsTwoPair()
        {
            return _cards.GroupBy(c => c.Rank).Count(group => group.Count() == 2) == 2;
        }

        private bool IsThreeOfAKind()
        {
            return _cards.GroupBy(c => c.Rank).Any(group => group.Count() == 3);
        }

        private bool IsStraight()
        {
            var distinctValues = _cards.Select(c => (int)c.Rank).Distinct().OrderBy(v => v).ToList();
            if (distinctValues.Count < 5) return false;

            for (int i = 0; i < distinctValues.Count - 1; i++)
            {
                if (distinctValues[i + 1] - distinctValues[i] != 1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsFlush()
        {
            return _cards.Select(c => c.Suit).Distinct().Count() == 1;
        }

        private bool IsFullHouse()
        {
            return IsThreeOfAKind() && IsPair();
        }

        private bool IsFourOfAKind()
        {
            return _cards.GroupBy(c => c.Rank).Any(group => group.Count() == 4);
        }

        private bool IsStraightFlush()
        {
            return IsStraight() && IsFlush();
        }

        private bool IsRoyalFlush()
        {
            return IsStraightFlush() && _cards.Any(c => c.Rank == Rank.Ace);
        }
    }
}
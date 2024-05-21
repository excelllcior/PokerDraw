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

        public void AddCard(Card card)
        {
            if (_cards.Count < 5) 
                _cards.Add(card);
            else 
                throw new Exception("Невозможно добавить карту. В руке не может быть больше 5 карт.");
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public void ChangeCard(int index, Card newCard)
        {
            _cards[index] = newCard;
        }

        public void SortCards()
        {
            _cards.OrderBy(rank => rank).ToList();
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
            IEnumerable<IGrouping<Rank, Card>> groupsOfRanks = _cards.GroupBy(card => card.Rank);

            foreach (var group in groupsOfRanks)
            {
                if (group.Count() == 2)
                    return true;
            }
            return false;
        }

        private bool IsTwoPair()
        {
            IEnumerable<IGrouping<Rank, Card>> groupsOfRanks = _cards.GroupBy(card => card.Rank);
            int pairCount = 0;

            foreach (var group in groupsOfRanks)
            {
                if (group.Count() == 2)
                    pairCount++;
            }
            return pairCount == 2;
        }

        private bool IsThreeOfAKind()
        {
            IEnumerable<IGrouping<Rank, Card>> groupsOfRanks = _cards.GroupBy(card => card.Rank);

            foreach (var group in groupsOfRanks)
            {
                if (group.Count() == 3)
                    return true;
            }
            return false;
        }

        private bool IsStraight()
        {
            List<int> ranks = _cards.Select(cards => (int)cards.Rank).OrderBy(rank => rank).ToList();

            for (int i = 1; i < ranks.Count; i++)
            {
                if (ranks[i] != ranks[i - 1] + 1)
                    return false;
            }
            return true;
        }

        private bool IsFlush()
        {
            List<Suit> suits = _cards.Select(card => card.Suit).Distinct().ToList();
            return suits.Count == 1;
        }

        private bool IsFullHouse()
        {
            IEnumerable<IGrouping<Rank, Card>> groupsOfRanks = _cards.GroupBy(card => card.Rank);

            if (groupsOfRanks.Count() == 2)
            {
                if (IsTwoPair() && IsThreeOfAKind())
                    return true;
            }
            return false;
        }

        private bool IsFourOfAKind()
        {
            IEnumerable<IGrouping<Rank, Card>> groupsOfRanks = _cards.GroupBy(card => card.Rank);
            foreach (var group in groupsOfRanks)
            {
                if (group.Count() == 4)
                    return true;
            }
            return false;
        }

        private bool IsStraightFlush()
        {
            return IsFlush() && IsStraight();
        }

        private bool IsRoyalFlush()
        {
            bool hasRoyalCards = _cards.Any(card => new List<Rank>{ Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace }.Contains(card.Rank));
            return hasRoyalCards && IsFlush();
        }
    }
}
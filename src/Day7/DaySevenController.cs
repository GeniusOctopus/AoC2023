using System.Collections;
using System.Linq;

namespace AoC2023.Day7
{
    internal class DaySevenController
    {
        public static void Run()
        {
            string[] input = File.ReadAllLines("Day7/input.txt");
            var hands = input.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)).Select(x => new CamelCardHand(x.First().Select(y => y), int.Parse(x.Last())));

            //var fiveOfAKind = hands.Where(hand => GetHandTypePart1(hand) == HandType.FiveOfAKind).OrderDescending().ToList();
            //var fourOfAKind = hands.Where(hand => GetHandTypePart1(hand) == HandType.FourOfAKind).OrderDescending().ToList();
            //var fullHouse = hands.Where(hand => GetHandTypePart1(hand) == HandType.FullHouse).OrderDescending().ToList();
            //var threeOfAKind = hands.Where(hand => GetHandTypePart1(hand) == HandType.ThreeOfAKind).OrderDescending().ToList();
            //var twoPair = hands.Where(hand => GetHandTypePart1(hand) == HandType.TwoPair).OrderDescending().ToList();
            //var onePair = hands.Where(hand => GetHandTypePart1(hand) == HandType.OnePair).OrderDescending().ToList();
            //var highCard = hands.Where(hand => GetHandTypePart1(hand) == HandType.HighCard).OrderDescending().ToList();

            var fiveOfAKind = hands.Where(hand => GetHandTypePart2(hand) == HandType.FiveOfAKind).OrderDescending().ToList();
            var fourOfAKind = hands.Where(hand => GetHandTypePart2(hand) == HandType.FourOfAKind).OrderDescending().ToList();
            var fullHouse = hands.Where(hand => GetHandTypePart2(hand) == HandType.FullHouse).OrderDescending().ToList();
            var threeOfAKind = hands.Where(hand => GetHandTypePart2(hand) == HandType.ThreeOfAKind).OrderDescending().ToList();
            var twoPair = hands.Where(hand => GetHandTypePart2(hand) == HandType.TwoPair).OrderDescending().ToList();
            var onePair = hands.Where(hand => GetHandTypePart2(hand) == HandType.OnePair).OrderDescending().ToList();
            var highCard = hands.Where(hand => GetHandTypePart2(hand) == HandType.HighCard).OrderDescending().ToList();

            var ranked = new List<CamelCardHand>();
            ranked.AddRange(highCard);
            ranked.AddRange(onePair);
            ranked.AddRange(twoPair);
            ranked.AddRange(threeOfAKind);
            ranked.AddRange(fullHouse);
            ranked.AddRange(fourOfAKind);
            ranked.AddRange(fiveOfAKind);

            var sum = 0;

            for (int i = 1; i <= ranked.Count; i++)
            {
                sum += ranked[i - 1].Bid * i;
            }

            Console.WriteLine(sum);
        }

        private static HandType GetHandTypePart1(CamelCardHand hand)
        {
            if (hand.Cards.GroupBy(x => x).Select(x => x.Count()).All(x => x == 5))
                return HandType.FiveOfAKind;

            else if (hand.Cards.GroupBy(x => x).Select(x => x.Count()).Any(x => x == 4))
                return HandType.FourOfAKind;

            else if (hand.Cards.GroupBy(x => x).Select(x => x.Count()).Any(x => x == 3) && hand.Cards.GroupBy(x => x).Select(x => x.Count()).Any(x => x == 2))
                return HandType.FullHouse;

            else if (hand.Cards.GroupBy(x => x).Select(x => x.Count()).Count() == 3 && hand.Cards.GroupBy(x => x).Select(x => x.Count()).Any(x => x == 3))
                return HandType.ThreeOfAKind;

            else if (hand.Cards.GroupBy(x => x).Select(x => x.Count()).Count() == 3 && hand.Cards.GroupBy(x => x).Select(x => x.Count()).Where(x => x == 2).Count() == 2)
                return HandType.TwoPair;

            else if (hand.Cards.GroupBy(x => x).Select(x => x.Count()).Count() == 4 && hand.Cards.GroupBy(x => x).Select(x => x.Count()).Any(x => x == 2))
                return HandType.OnePair;

            else if (hand.Cards.GroupBy(x => x).Select(x => x.Count()).Count() == 5)
                return HandType.HighCard;

            else return HandType.HighCard;
        }

        private static HandType GetHandTypePart2(CamelCardHand hand)
        {
            var jokerCount = hand.Cards.Count(x => x == 'J');
            var normalType = GetHandTypePart1(hand);

            if (jokerCount == 5 ||
                jokerCount == 4 ||
                jokerCount == 3 && normalType == HandType.OnePair ||
                jokerCount == 2 && normalType == HandType.ThreeOfAKind ||
                jokerCount == 1 && normalType == HandType.FourOfAKind ||
                normalType == HandType.FiveOfAKind)
                return HandType.FiveOfAKind;

            else if (jokerCount == 3 ||
                jokerCount == 2 && normalType == HandType.TwoPair ||
                jokerCount == 2 && normalType == HandType.OnePair ||
                jokerCount == 1 && normalType == HandType.ThreeOfAKind ||
                normalType == HandType.FourOfAKind)
                return HandType.FourOfAKind;

            else if (jokerCount == 1 && normalType == HandType.TwoPair ||
                normalType == HandType.FullHouse)
                return HandType.FullHouse;

            else if (jokerCount == 2 ||
                jokerCount == 1 && normalType == HandType.OnePair ||
                normalType == HandType.ThreeOfAKind)
                return HandType.ThreeOfAKind;

            else if (normalType == HandType.TwoPair)
                return HandType.TwoPair;

            else if (jokerCount == 1 ||
                normalType == HandType.OnePair)
                return HandType.OnePair;

            else return HandType.HighCard;
        }
    }

    enum HandType
    {
        FiveOfAKind,
        FourOfAKind,
        FullHouse,
        ThreeOfAKind,
        TwoPair,
        OnePair,
        HighCard
    }

    record struct CamelCardHand(IEnumerable<char> Cards, int Bid) : IComparable<CamelCardHand>
    {
        public int CompareTo(CamelCardHand other)
        {
            int value = 0;

            int i = 0;
            while (i < Cards.Count() && Transform(Cards.ElementAt(i)) == Transform(other.Cards.ElementAt(i)))
            {
                value = 0;
                i++;
            }

            if (i < Cards.Count() && Transform(Cards.ElementAt(i)) > Transform(other.Cards.ElementAt(i)))
            {
                value = -1;
            }

            if (i < Cards.Count() && Transform(Cards.ElementAt(i)) < Transform(other.Cards.ElementAt(i)))
            {
                value = 1;
            }

            return value;
        }

        private int Transform(char c)
        {
            return c switch
            {
                '2' => 2,
                '3' => 3,
                '4' => 4,
                '5' => 5,
                '6' => 6,
                '7' => 7,
                '8' => 8,
                '9' => 9,
                'T' => 10,
                'J' => 1,
                'Q' => 12,
                'K' => 13,
                'A' => 14,
                _ => 0
            };
        }
    }
}

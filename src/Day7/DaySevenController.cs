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

            var fiveOfAKind = hands.Where(hand => hand.Cards.GroupBy(x => x).Select(x => x.Count()).All(x => x == 5)).OrderDescending().ToList();
            var fourOfAKind = hands.Where(hand => hand.Cards.GroupBy(x => x).Select(x => x.Count()).Any(x => x == 4)).OrderDescending().ToList();
            var fullHouse = hands.Where(hand => hand.Cards.GroupBy(x => x).Select(x => x.Count()).Any(x => x== 3) && hand.Cards.GroupBy(x => x).Select(x => x.Count()).Any(x => x == 2)).OrderDescending().ToList();
            var threeOfAKind = hands.Where(hand => hand.Cards.GroupBy(x => x).Select(x => x.Count()).Count() == 3 && hand.Cards.GroupBy(x => x).Select(x => x.Count()).Any(x => x == 3)).OrderDescending().ToList();
            var twoPair = hands.Where(hand => hand.Cards.GroupBy(x => x).Select(x => x.Count()).Count() == 3 && hand.Cards.GroupBy(x => x).Select(x => x.Count()).Where(x => x == 2).Count() == 2).OrderDescending().ToList();
            var onePair = hands.Where(hand => hand.Cards.GroupBy(x => x).Select(x => x.Count()).Count() == 4 && hand.Cards.GroupBy(x => x).Select(x => x.Count()).Any(x => x == 2)).OrderDescending().ToList();
            var highCard = hands.Where(hand => hand.Cards.GroupBy(x => x).Select(x => x.Count()).Count() == 5).OrderDescending().ToList();

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
    }

    record struct CamelCardHand(IEnumerable<char> Cards, int Bid) : IComparable<CamelCardHand>
    {
        public int Compare(CamelCardHand x, CamelCardHand y)
        {
            return x.Cards.Sum(x => x) > y.Cards.Sum(x => x) ? 1 : 0;
        }

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
                'J' => 11,
                'Q' => 12,
                'K' => 13,
                'A' => 14,
                _ => 0
            };
        }
    }
}

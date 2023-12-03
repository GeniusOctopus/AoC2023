using System.Text.RegularExpressions;

namespace AoC2023.Day3
{
    internal partial class DayThreeController
    {
        [GeneratedRegex(@"\d+")]
        public static partial Regex NumberRegex();

        private static readonly Dictionary<(int x, int y), List<int>> gears = [];

        public static void Run()
        {
            string[] input = File.ReadAllLines("Day3/input.txt");

            (int x, int y, int length, int value, (bool adjacent, bool hasGear) adjacency)[] numbers = [];
            numbers = input.SelectMany((line, y) =>
                          NumberRegex().Matches(line)
                            .Select(match =>
                                (match.Index, y, match.Length, int.Parse(match.Value),
                                GetAdjacency((match.Index, y, match.Length, int.Parse(match.Value)), input)))
                                )
                      .ToArray();

            Console.WriteLine(numbers.Where(number => number.adjacency.adjacent).Sum(number => number.value));
            Console.WriteLine(gears.Where(coords => coords.Value.Count == 2).Sum(coords => coords.Value.Aggregate(1, (acc, n) => acc * n)));
        }

        private static (bool adjacentToChar, bool adjacentToGear) GetAdjacency((int x, int y, int length, int value) number, string[] input)
        {
            (bool adjacentToChar, bool adjacentToGear) hasAdjacent = (false, false);

            for (int x = (number.x > 0 ? -1 : 0); x <= (number.x + number.length < input[0].Length ? number.length : number.length - 1); x++)
            {
                for (int y = (number.y > 0 ? -1 : 0); y <= (number.y < input.Length - 1 ? 1 : 0); y++)
                {
                    // Part 1
                    if (!hasAdjacent.adjacentToChar && !char.IsDigit(input[number.y + y][number.x + x]) && input[number.y + y][number.x + x] != '.')
                        hasAdjacent.adjacentToChar = true;

                    // Part 2
                    if (!hasAdjacent.adjacentToGear && input[number.y + y][number.x + x] == '*')
                    {
                        if (!gears.TryGetValue((number.x + x, number.y + y), out _))
                        {
                            gears.Add((number.x + x, number.y + y), new List<int> { number.value });
                        }
                        else
                        {
                            gears[(number.x + x, number.y + y)].Add(number.value);
                        }

                        hasAdjacent.adjacentToGear = true;
                    }
                }
            }

            return hasAdjacent;
        }
    }
}

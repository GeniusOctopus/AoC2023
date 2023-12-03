using System.Text.RegularExpressions;

namespace AoC2023.Day3
{
    internal partial class DayThreeController
    {
        [GeneratedRegex(@"\d+")]
        public static partial Regex NumberRegex();

        public static void Run()
        {
            string[] input = File.ReadAllLines("Day3/input.txt");
            var sum = 0;

            (int x, int y, int length, int value)[] numbers = [];
            numbers = input.SelectMany((line, y) =>
                          NumberRegex().Matches(line).Select(match =>
                              (match.Index, y, match.Length, int.Parse(match.Value))))
                      .ToArray();

            foreach (var number in numbers)
            {
                var hasAdjacentSpecialChar = false;

                for (int x = (number.x > 0 ? -1 : 0); !hasAdjacentSpecialChar && x <= (number.x + number.length < input[0].Length ? number.length : number.length - 1); x++)
                {
                    for (int y = (number.y > 0 ? -1 : 0); !hasAdjacentSpecialChar && y <= (number.y < input.Length - 1 ? 1 : 0); y++)
                    {
                        if (!char.IsDigit(input[number.y + y][number.x + x]) && input[number.y + y][number.x + x] != '.')
                            hasAdjacentSpecialChar = true;
                    }
                }

                if (hasAdjacentSpecialChar)
                    sum += number.value;
            }

            Console.WriteLine(sum);
        }
    }
}

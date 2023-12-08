using System.Numerics;
using System.Text.RegularExpressions;

namespace AoC2023.Day8
{
    internal partial class DayEightController
    {
        public static void Run()
        {
            string[] input = File.ReadAllLines("Day8/input.txt");

            var instructions = input.First();

            Dictionary<string, (string left, string right)> tree = [];

            for (int i = 2;  i < input.Length; i++)
            {
                var match = Node().Match(input[i]);

                tree[match.Groups[1].Value] = (match.Groups[2].Value, match.Groups[3].Value);
            }

            var found = false;
            var count = 0;
            var currentNode = tree.First(x => x.Key == "AAA").Key;

            while (!found)
            {
                for (int i = 0; !found && i < instructions.Length; i++)
                {
                    if (instructions[i] == 'L')
                    {
                        currentNode = tree[currentNode].left;
                        count++;
                    }
                    else
                    {
                        currentNode = tree[currentNode].right;
                        count++;
                    }

                    if (currentNode == "ZZZ")
                    {
                        found = true;
                    }
                }
            }

            Console.WriteLine(count);
        }

        [GeneratedRegex(@"^([A-Z]{3}) = \(([A-Z]{3}), ([A-Z]{3})\)$")]
        private static partial Regex Node();
    }
}

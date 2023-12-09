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

            for (int i = 2; i < input.Length; i++)
            {
                var match = Node().Match(input[i]);

                tree[match.Groups[1].Value] = (match.Groups[2].Value, match.Groups[3].Value);
            }

            var found = false;
            var startNodes = tree.Where(x => x.Key.EndsWith("A")).Select(x => x.Key).ToList();
            var counts = new long[startNodes.Count];

            for (int nodeIndex = 0; nodeIndex < startNodes.Count; nodeIndex++)
            {
                while (!found)
                {
                    for (int i = 0; !found && i < instructions.Length; i++)
                    {
                        if (instructions[i] == 'L')
                        {
                            startNodes[nodeIndex] = tree[startNodes[nodeIndex]].left;
                        }
                        else
                        {
                            startNodes[nodeIndex] = tree[startNodes[nodeIndex]].right;
                        }

                        counts[nodeIndex]++;

                        if (startNodes[nodeIndex].EndsWith("Z"))
                        {
                            found = true;
                        }
                    }
                }

                found = false;
            }

            long kgv = counts.Aggregate(1L, (acc, n) => acc = acc * n / GGT(acc, n));

            Console.WriteLine(kgv);
        }

        private static long GGT(long a, long b)
        {
            while (b != 0)
            {
                long h = b;
                b = a % b;
                a = h;
            }
            return a;
        }

        [GeneratedRegex(@"^([A-Z1-2]{3}) = \(([A-Z1-2]{3}), ([A-Z1-2]{3})\)$")]
        private static partial Regex Node();
    }
}

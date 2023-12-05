namespace AoC2023.Day5
{
    internal partial class DayFiveController
    {
        public static void Run()
        {
            string[] input = File.ReadAllLines("Day5/input.txt");

            Dictionary<long, long> seeds = input[0][7..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => new KeyValuePair<long, long>(long.Parse(x), long.Parse(x))).ToDictionary();
            List<List<(long destination, long source, long length)>> maps = [];

            for (long i = 1; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    maps.Add([]);
                }
                else
                {
                    if (!char.IsDigit(input[i][0]))
                        continue;

                    var numbers = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
                    maps.Last().Add((numbers[0], numbers[1], numbers[2]));
                }
            }

            foreach (var pair in seeds)
            {
                for (int j = 0; j < maps.Count; j++)
                {
                    var found = false;

                    for (int k = 0; !found && k < maps[j].Count; k++)
                    {
                        if (seeds[pair.Key] >= maps[j][k].source && seeds[pair.Key] <= maps[j][k].source + (maps[j][k].length - 1))
                        {
                            var distanceToSource = seeds[pair.Key] - maps[j][k].source;
                            seeds[pair.Key] = maps[j][k].destination + distanceToSource;
                            found = true;
                        }
                    }

                    found = false;
                }
            }

            Console.WriteLine(seeds.Min(x => x.Value));
        }
    }
}
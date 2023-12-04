using System.Collections.Generic;

namespace AoC2023.Day4
{
    internal class DayFourController
    {
        public static void Run()
        {
            string[] input = File.ReadAllLines("Day4/input.txt");

            var winningNumbers = input
                .SelectMany((line, n) => line.Split(':').Skip(1)
                .Select(sequence =>
                    (sequence.Split('|', StringSplitOptions.RemoveEmptyEntries)
                        .First().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => int.Parse(x)))
                            .Intersect(
                    sequence.Split('|', StringSplitOptions.RemoveEmptyEntries).Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x))))
                );

            Console.WriteLine(winningNumbers.Sum(x => x.Aggregate(0, (acc, n) => acc = x.Any() ? (int)Math.Pow(2, x.Count() - 1) : 0)));

            List<(IEnumerable<int> set, int count)> winningSetsWithCount = winningNumbers.Select(x => (x, 1)).ToList();

            for (int i = 0; i < winningSetsWithCount.Count; i++)
            {
                for (int j = 0; j < winningSetsWithCount[i].count; j++)
                {
                    winningSetsWithCount.Skip(i + 1)
                            .Take(winningSetsWithCount[i].set.Count()).ToList()
                                .ForEach(subset =>
                                {
                                    var s = winningSetsWithCount.First(x => x.set == subset.set);
                                    var index = winningSetsWithCount.IndexOf(subset);
                                    s.count++;
                                    winningSetsWithCount[index] = s;
                                });
                }
            }

            Console.WriteLine(winningSetsWithCount.Sum(x => x.count));
        }
    }
}

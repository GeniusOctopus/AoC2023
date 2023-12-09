namespace AoC2023.Day9
{
    internal class DayNineController
    {
        public static void Run()
        {
            string[] input = File.ReadAllLines("Day9/input.txt");
            var histories = input.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)).ToList();
            var extrapolation = new List<List<List<int>>>();

            foreach (var history in histories)
            {
                var differences = new List<List<int>>
                {
                    history.ToList()
                };

                while (!differences.Last().All(x => x == 0))
                {
                    differences.Add([]);

                    for (int i = 0; i < differences[^2].Count - 1; i++)
                    {
                        differences.Last().Add(differences[^2][i + 1] - differences[^2][i]);
                    }
                }

                extrapolation.Add(differences);
            }

            // Part1
            //foreach (var list in extrapolation)
            //{
            //    for (int i = list.Count - 1; i > 0; i--)
            //    {
            //        list[i - 1].Add(list[i - 1].Last() + list[i].Last());
            //    }
            //}

            //var sum = extrapolation.Sum(x => x.First().Last());

            // Part2
            for (int i = 0; i < extrapolation.Count; i++)
            {
                for (int j = extrapolation[i].Count - 1; j > 0; j--)
                {
                    extrapolation[i][j - 1] = extrapolation[i][j - 1].Prepend(extrapolation[i][j - 1].First() - extrapolation[i][j].First()).ToList();
                }
            }

            var sum = extrapolation.Sum(x => x.First().First());

            Console.WriteLine(sum);
        }
    }
}

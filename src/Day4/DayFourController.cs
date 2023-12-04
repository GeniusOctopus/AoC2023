namespace AoC2023.Day4
{
    internal class DayFourController
    {
        public static void Run()
        {
            string[] input = File.ReadAllLines("Day4/input.txt");

            var sum1 = input
                .SelectMany((line, n) => line.Split(':').Skip(1)
                .Select(sequence =>
                    (sequence.Split('|', StringSplitOptions.RemoveEmptyEntries)
                        .First().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => int.Parse(x)))
                            .Intersect(
                    sequence.Split('|', StringSplitOptions.RemoveEmptyEntries).Last().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x))))
                ).Sum(x => x.Aggregate(0, (acc, n) => acc = x.Any() ? (int)Math.Pow(2, x.Count() - 1) : 0));

            Console.WriteLine(sum1);
        }
    }
}

namespace AoC2023.Day6
{
    internal class DaySixController
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Day6/input.txt");
            var firstLine1 = input[0].Split(':')[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);
            var secondLine1 = input[1].Split(':')[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);
            IEnumerable<(long time, long distance)> races1 = firstLine1.Zip(secondLine1);

            var firstLine2 = long.Parse(input[0].Split(':')[1].Trim().Replace(" ", string.Empty));
            var secondLine2 = long.Parse(input[1].Split(':')[1].Trim().Replace(" ", string.Empty));
            IEnumerable<(long time, long distance)> races2 = [(firstLine2, secondLine2)];

            Calculate(races1);
            Calculate(races2);
        }

        public static void Calculate(IEnumerable<(long time, long distance)> races)
        {
            List<int> possibleStartTimes = [];

            foreach (var race in races)
            {
                // f(x) = ax^2 + bx + c
                double a = 1;
                double b = race.time * (-1);
                double c = race.distance + 1;

                double discriminant = b * b / 4 - c;
                double coefficient = -b / 2;
                double x1 = coefficient - Math.Sqrt(discriminant);
                double x2 = coefficient + Math.Sqrt(discriminant);

                possibleStartTimes.Add((int)Math.Floor(x2) - (int)Math.Ceiling(x1) + 1);
            }

            Console.WriteLine(possibleStartTimes.Aggregate(1, (acc, n) => acc * n));
        }
    }
}

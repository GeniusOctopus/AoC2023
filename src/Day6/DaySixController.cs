namespace AoC2023.Day6
{
    internal class DaySixController
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Day6/input.txt");
            var firstLine = input[0].Split(':')[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            var secondLine = input[1].Split(':')[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            IEnumerable<(int time, int distance)> races = firstLine.Zip(secondLine);

            List<int> possibleStartTimes = [];

            foreach (var race  in races)
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

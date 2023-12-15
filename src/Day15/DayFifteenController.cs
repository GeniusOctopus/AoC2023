namespace AoC2023.Day15
{
    internal class DayFifteenController
    {
        public static void Run()
        {
            string input = File.ReadAllText("Day15/input.txt");

            var commands = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var sum = commands.Sum(c => c.Aggregate(0, (acc, next) => acc = (acc + (int)next) * 17 % 256));

            Console.WriteLine(sum);
        }
    }
}

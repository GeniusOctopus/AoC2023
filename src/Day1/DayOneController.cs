namespace AoC2023.Day1
{
    internal class DayOneController
    {
        public void Run()
        {
            var input = File.ReadAllLines("Day1/input.txt");
            Console.WriteLine(input.Sum(line => (line.First(char.IsDigit) - '0') * 10 + line.Last(char.IsDigit) - '0'));
        }
    }
}

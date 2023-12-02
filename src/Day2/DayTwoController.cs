namespace AoC2023.Day2
{
    internal class DayTwoController
    {
        private const string Red = "red";
        private const string Green = "green";
        private const string Blue = "blue";

        private static readonly Dictionary<string, int> _cubes = new()
        {
            {Red, 12},
            {Green, 13},
            {Blue, 14}
        };

        private static readonly Dictionary<string, int> _minimumCubes = new()
        {
            {Red, 1},
            {Green, 1},
            {Blue, 1}
        };

        public static void Run()
        {
            string[] input = File.ReadAllLines("Day2/input.txt");
            var sumPart1 = 0;
            var sumPart2 = 0;

            foreach (var line in input)
            {
                string[] game = line.Split(':', ';');
                int gameNumber = int.Parse(game[0].Split(' ')[1]);
                var gameIsPossible = true;

                for (int i = 1; i < game.Length; i++)
                {
                    string[] set = game[i].Split(',');

                    foreach (var take in set)
                    {
                        string[] pair = take.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        var amount = int.Parse(pair[0]);

                        if (amount > _cubes[pair[1]])
                            gameIsPossible = false;

                        if (amount > _minimumCubes[pair[1]])
                            _minimumCubes[pair[1]] = amount;
                    }
                }

                if (gameIsPossible)
                    sumPart1 += gameNumber;

                sumPart2 += _minimumCubes[Red] * _minimumCubes[Green] * _minimumCubes[Blue];
                _minimumCubes[Red] = 1;
                _minimumCubes[Green] = 1;
                _minimumCubes[Blue] = 1;
            }

            Console.WriteLine(sumPart1);
            Console.WriteLine(sumPart2);
        }
    }
}

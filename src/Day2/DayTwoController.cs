namespace AoC2023.Day2
{
    internal class DayTwoController
    {
        private const int MaxRed = 12;
        private const int MaxGreen = 13;
        private const int MaxBlue = 14;
        private const string Red = "red";
        private const string Green = "green";
        private const string Blue = "blue";

        public static void Run()
        {
            string[] input = File.ReadAllLines("Day2/input.txt");

            var sum = 0;

            foreach (var line in input)
            {
                string[] game = line.Split(':', ';');
                int gameNumber = int.Parse(game[0].Split(' ')[1]);
                bool gameIsPossible = true;

                for (int i = 1; gameIsPossible && i < game.Length; i++)
                {
                    string[] set = game[i].Split(',');

                    foreach (var take in set)
                    {
                        bool redPossible = true;
                        bool greenPossible = true;
                        bool bluePossible = true;
                        string[] pair = take.Split(' ');

                        switch (pair[2])
                        {
                            case Red:
                                if (int.Parse(pair[1]) > MaxRed)
                                    redPossible = false;
                                break;
                            case Green:
                                if (int.Parse(pair[1]) > MaxGreen)
                                    greenPossible = false;
                                break;
                            case Blue:
                                if (int.Parse(pair[1]) > MaxBlue)
                                    bluePossible = false;
                                break;
                        }

                        if (!(redPossible && greenPossible && bluePossible))
                        {
                            gameIsPossible = false;
                            break;
                        }
                    }
                }

                if (gameIsPossible)
                    sum += gameNumber;
            }

            Console.WriteLine(sum);
        }
    }
}

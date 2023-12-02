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

            var sumPart1 = 0;
            var sumPart2 = 0;

            foreach (var line in input)
            {
                string[] game = line.Split(':', ';');
                int gameNumber = int.Parse(game[0].Split(' ')[1]);
                bool gameIsPossible = true;
                int minimumRed = 1;
                int minimumGreen = 1;
                int minimumBlue = 1;

                for (int i = 1; i < game.Length; i++)
                {
                    string[] set = game[i].Split(',');

                    foreach (var take in set)
                    {
                        bool redPossible = true;
                        bool greenPossible = true;
                        bool bluePossible = true;
                        string[] pair = take.Split(' ');
                        int amount = int.Parse(pair[1]);

                        switch (pair[2])
                        {
                            case Red:
                                if (amount > MaxRed)
                                    redPossible = false;

                                if (amount > minimumRed)
                                    minimumRed = amount;
                                break;
                            case Green:
                                if (amount > MaxGreen)
                                    greenPossible = false;

                                if (amount > minimumGreen)
                                    minimumGreen = amount;
                                break;
                            case Blue:
                                if (amount > MaxBlue)
                                    bluePossible = false;

                                if (amount > minimumBlue)
                                    minimumBlue = amount;
                                break;
                        }

                        if (!(redPossible && greenPossible && bluePossible) && gameIsPossible)
                        {
                            gameIsPossible = false;
                        }
                    }
                }

                if (gameIsPossible)
                    sumPart1 += gameNumber;

                sumPart2 += minimumRed * minimumGreen * minimumBlue;
            }

            Console.WriteLine(sumPart1);
            Console.WriteLine(sumPart2);
        }
    }
}

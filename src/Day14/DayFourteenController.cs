namespace AoC2023.Day14
{
    internal class DayFourteenController
    {
        public static void Run()
        {
            string[] input = File.ReadAllLines("Day14/input.txt");
            List<string> rotatedInput = [];

            // Rotate input for easier interaction
            for (int x = 0; x < input[0].Length; x++)
            {
                rotatedInput.Add("");
                for (int y = input.Length - 1; y >= 0; y--)
                {
                    rotatedInput[x] = rotatedInput[x] + input[y][x];
                }
            }

            // Find all round rocks left to a squared rock or eol
            var rockLines = new List<RockLine>();
            for (int y = 0; y < rotatedInput.Count; y++)
            {
                rockLines.Add(new RockLine([], new(rotatedInput[0].Length, y)));

                for (int x = 0; x < rotatedInput[0].Length; x++)
                {
                    if (rotatedInput[y][x] == 'O')
                    {
                        rockLines.Last().RoundStones.Add(new(x, y));
                    }
                    else if (rotatedInput[y][x] == '#')
                    {
                        rockLines.Last().SquaredStone = new(x, y);
                        rockLines.Add(new RockLine([], new(rotatedInput[0].Length, y)));
                    }
                }
            }

            // Move all rocks to the right as far as possible
            var sum = 0;
            for (int i = 0; i < rockLines.Count; i++)
            {
                var offset = 1;
                for (int roundRockIndex = rockLines[i].RoundStones.Count - 1; roundRockIndex >= 0;  roundRockIndex--)
                {
                    rockLines[i].RoundStones[roundRockIndex].X = rockLines[i].SquaredStone.X - offset;
                    sum += rockLines[i].RoundStones[roundRockIndex].X + 1;
                    offset++;
                }
            }

            Console.WriteLine(sum);
        }

        class RockLine(List<Point> roundStones, Point squaredStone)
        {
            public List<Point> RoundStones { get; set; } = roundStones;
            public Point SquaredStone { get; set; } = squaredStone;
        }

        class Point(int x, int y)
        {
            public int X { get; set; } = x;
            public int Y { get; set; } = y;
        }
    }
}

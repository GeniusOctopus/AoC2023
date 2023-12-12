namespace AoC2023.Day11
{
    internal class DayElevenController
    {
        public static void Run()
        {
            List<string> input = File.ReadAllLines("Day11/input.txt").ToList();

            // Get Galaxies coordinates and assign index
            var galaxies = new List<Galaxy>();
            var index = 1;
            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        galaxies.Add(new Galaxy(x, y, index));
                        index++;
                    }
                }
            }

            // Part2
            // Expansion is just a correction of galaxies coordinates
            long expansionParameter = 1000000;
            long yCoord = 0;
            long yExpansiontimes = 0;
            for (int y = 0; y < input.Count; y++)
            {
                if (input[y].All(c => c == '.'))
                {
                    for (int i = 0; i < galaxies.Count; i++)
                    {
                        if (galaxies[i].Y > yCoord)
                        {
                            galaxies[i] = new Galaxy(galaxies[i].X, galaxies[i].Y + expansionParameter - 1, galaxies[i].Index);
                        }
                    }
                    yCoord += expansionParameter - 1 + yExpansiontimes;
                    yExpansiontimes++;
                }
                else
                {
                    yCoord++;
                }
            }

            long xCoord = 0;
            long xExpansiontimes = 0;
            for (int x = 0; x < input[0].Length; x++)
            {
                if (input.All(c => c[x] == '.'))
                {
                    for (int i = 0; i < galaxies.Count; i++)
                    {
                        if (galaxies[i].X > xCoord)
                        {
                            galaxies[i] = new Galaxy(galaxies[i].X + expansionParameter - 1, galaxies[i].Y, galaxies[i].Index);
                        }
                    }

                    xCoord += expansionParameter - 1 + xExpansiontimes;
                    xExpansiontimes++;
                }
                else
                {
                    xCoord++;
                }
            }

            // Build Galaxy Pairs
            var galaxyPairs = new List<(Galaxy g1, Galaxy g2, int distance)>();
            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = galaxies.Count - 1; j > i; j--)
                {
                    galaxyPairs.Add((galaxies[i], galaxies[j], -1));
                }
            }
            
            // Calculate distance between galaxies
            long sum = 0;
            for (int i = 0; i < galaxyPairs.Count; i++)
            {
                long deltaX = Math.Abs(galaxyPairs[i].g1.X - galaxyPairs[i].g2.X);
                long deltaY = Math.Abs(galaxyPairs[i].g1.Y - galaxyPairs[i].g2.Y);

                sum += deltaX + deltaY;
            }

            Console.WriteLine(sum);
        }
    }

    struct Galaxy(long x, long y, int index)
    {
        public long X { get; set; } = x;
        public long Y { get; set; } = y;
        public int Index { get; set; } = index;
    }
}

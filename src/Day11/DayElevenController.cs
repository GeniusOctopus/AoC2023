using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AoC2023.Day11
{
    internal class DayElevenController
    {
        public static void Run()
        {
            List<string> input = File.ReadAllLines("Day11/input.txt").ToList();

            // Zeilenexpansion
            for (int y = input.Count - 1; y >= 0; y--)
            {
                if (input[y].All(c => c == '.'))
                {
                    input.Insert(y, new string('.', input[y].Length));
                }
            }

            // Spaltenexpansion
            for (int x = input[0].Length - 1; x >= 0; x--)
            {
                if (input.All(c => c[x] == '.'))
                {
                    for (int i = 0; i < input.Count; i++)
                    {
                        input[i] = input[i].Insert(x, ".");
                    }
                }
            }

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

            // Build Galaxy Pairs
            var galaxyPairs = new List<(Galaxy g1, Galaxy g2, int distance)>();
            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = galaxies.Count - 1; j > i; j--)
                {
                    galaxyPairs.Add((galaxies[i], galaxies[j], -1));
                }
            }

            // Calculate Path between galaxies
            var sum = 0;
            for (int i = 0; i < galaxyPairs.Count; i++)
            {
                var deltaX = 0;
                var deltaY = 0;

                if (galaxyPairs[i].g1.X > galaxyPairs[i].g2.X)
                {
                    deltaX = galaxyPairs[i].g1.X - galaxyPairs[i].g2.X;
                }
                else
                {
                    deltaX = galaxyPairs[i].g2.X - galaxyPairs[i].g1.X;
                }

                if (galaxyPairs[i].g1.Y > galaxyPairs[i].g2.Y)
                {
                    deltaY = galaxyPairs[i].g1.Y - galaxyPairs[i].g2.Y;
                }
                else
                {
                    deltaY = galaxyPairs[i].g2.Y - galaxyPairs[i].g1.Y;
                }

                sum += deltaX + deltaY;
            }

            Console.WriteLine(sum);
        }
    }

    struct Galaxy(int x, int y, int index)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public int Index { get; set; } = index;
    }
}

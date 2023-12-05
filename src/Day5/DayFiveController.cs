namespace AoC2023.Day5
{
    internal partial class DayFiveController
    {
        public static void Run()
        {
            string[] input = File.ReadAllLines("Day5/input.txt");

            List<long> seeds = input[0][7..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
            List<SeedRange> seedRanges = [];
            List<List<PlantMap>> plans = [];

            // Create Seed Ranges
            for (int i = 0; i < seeds.Count - 1; i += 2)
            {
                seedRanges.Add(new SeedRange(seeds[i], seeds[i + 1]));
            }

            // Create Maps
            for (long i = 1; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    plans.Add([]);
                }
                else
                {
                    if (!char.IsDigit(input[i][0]))
                        continue;

                    var numbers = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
                    plans.Last().Add(new PlantMap(numbers[0], numbers[1], numbers[2]));
                }
            }

            var currentSeedRanges = new List<SeedRange>();
            currentSeedRanges.AddRange(seedRanges);
            foreach (var plan in plans)
            {
                var newRanges = new List<SeedRange>();

                foreach (var map in plan)
                {
                    for (int i = currentSeedRanges.Count - 1; i >= 0; i--)
                    {
                        var seedRange = currentSeedRanges[i];
                        if (seedRange.Start < map.SourceEnd && seedRange.CalculatedEnd > map.SoureceStart)
                        {
                            currentSeedRanges.RemoveAt(i);

                            var newStart = Math.Max(map.SoureceStart, seedRange.Start);
                            var newEnd = Math.Min(map.SourceEnd, seedRange.CalculatedEnd);

                            if (seedRange.Start < newStart)
                            {
                                currentSeedRanges.Add(SeedRange.NewSeedRange(seedRange.Start, newStart - 1));
                            }

                            if (seedRange.CalculatedEnd > newEnd)
                            {
                                currentSeedRanges.Add(SeedRange.NewSeedRange(newEnd+1, seedRange.CalculatedEnd));
                            }

                            newRanges.Add(SeedRange.NewSeedRange(newStart + map.Offset, newEnd + map.Offset));
                        }
                    }
                }

                currentSeedRanges.AddRange(newRanges);
            }

            Console.WriteLine(currentSeedRanges.Min(x => x.Start));
        }

        struct PlantMap(long destStart, long sourceStart, long length)
        {
            public long DestStart { get; set; } = destStart;
            public long SoureceStart { get; set; } = sourceStart;
            public long Length { get; set; } = length;
            public long DestEnd { get => DestStart + Length - 1; }
            public long SourceEnd { get => SoureceStart + Length - 1; }
            public long Offset { get => DestStart - SoureceStart; }
        };

        struct SeedRange(long start, long length)
        {
            public long Start { get; set; } = start;
            public long Length { get; set; } = length;
            public long CalculatedEnd { get => Start + Length - 1; }

            public static SeedRange NewSeedRange(long start, long end)
            {
                return new SeedRange(start, end - start + 1);
            }

        };
    }
}
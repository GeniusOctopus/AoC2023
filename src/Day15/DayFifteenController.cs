namespace AoC2023.Day15
{
    internal class DayFifteenController
    {
        public static void Run()
        {
            string input = File.ReadAllText("Day15/input.txt");

            var commands = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var sum = commands.Sum(c => Hash(c));

            Console.WriteLine(sum);

            Dictionary<int, List<(string label, int focalLength)>> boxes = [];

            foreach (var command in commands)
            {
                var tokens = command.Split('-', '=');
                var box = Hash(tokens[0]);

                if (!boxes.ContainsKey(box))
                {
                    boxes.Add(box, []);
                }

                if (command.Contains('='))
                {
                    if (!boxes[box].Any(x => x.label == tokens[0]))
                    {
                        boxes[box].Add((tokens[0], int.Parse(tokens[1])));
                    }
                    else
                    {
                        var index = boxes[box].IndexOf(boxes[box].First(x => x.label == tokens[0]));
                        boxes[box][index] = (tokens[0], int.Parse(tokens[1]));
                    }
                }
                else if (command.Contains('-'))
                {
                    if (boxes[box].Any(x => x.label == tokens[0]))
                        boxes[box].Remove(boxes[box].First(x => x.label == tokens[0]));
                }
            }

            var sum2 = 0;

            for (int boxIndex = 0; boxIndex < 256; boxIndex++)
            {
                if (boxes.ContainsKey(boxIndex))
                {
                    for (int slot = 0; slot < boxes[boxIndex].Count; slot++)
                    {
                        sum2 += (boxIndex + 1) * (slot + 1) * boxes[boxIndex][slot].focalLength;
                    }
                }
            }

            Console.WriteLine(sum2);
        }

        private static int Hash(string input)
        {
            return input.Aggregate(0, (acc, next) => acc = (acc + (int)next) * 17 % 256);
        }
    }
}

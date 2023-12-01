namespace AoC2023.Day1
{
    internal class DayOneController
    {
        public static void Run()
        {
            string[] input = File.ReadAllLines("Day1/input.txt");
            // Part 1
            Console.WriteLine(CalculateSum(input));
            // Part 2
            Console.WriteLine(CalculateSumWithNumberWords(input));
        }

        private static int CalculateSum(string[] input)
        {
            return input.Sum(line =>
                (line.First(char.IsDigit) - '0') * 10
                + line.Last(char.IsDigit) - '0');
        }

        private static int CalculateSumWithNumberWords(string[] input)
        {
            (string word, int value)[] numberWords = [("one", 1), ("two", 2), ("three", 3), ("four", 4), ("five", 5), ("six", 6), ("seven", 7), ("eight", 8), ("nine", 9)];
            int sum = 0;

            foreach (var line in input)
            {
                var firstDigit = 0;
                var lastDigit = 0;

                for (int left = 0; left < line.Length; left++)
                {
                    if (char.IsDigit(line[left]))
                    {
                        firstDigit = line[left] - '0';
                        break;
                    }

                    foreach (var (word, value) in numberWords)
                    {
                        if (line[left..].StartsWith(word))
                        {
                            firstDigit = value;
                            break;
                        }
                    }

                    if (firstDigit > 0)
                        break;
                }

                for (int right = line.Length - 1; right >= 0; right--)
                {
                    if (char.IsDigit(line[right]))
                    {
                        lastDigit = line[right] - '0';
                        break;
                    }

                    foreach (var (word, value) in numberWords)
                    {
                        if (line[..(right + 1)].EndsWith(word))
                        {
                            lastDigit = value;
                            break;
                        }
                    }

                    if (lastDigit > 0)
                        break;
                }

                sum += firstDigit * 10 + lastDigit;
            }

            return sum;
        }
    }
}

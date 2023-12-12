using System.Text.RegularExpressions;

namespace AoC2023.Day12
{
    internal class DayTwelveController
    {
        public static void Run()
        {
            string[] input = File.ReadAllLines("Day12/input.txt");

            var springGroups = input.Select(x => new SpringGroup(x.Split(' ').First(), x.Split(' ').Last().Split(',').Select(a => int.Parse(a)).ToList())).ToList();

            for (int sg = 0; sg < springGroups.Count; sg++)
            {
                var patterns = new List<string>();
                var newPatterns = new List<string>();

                for (int i = 0; i < springGroups[sg].Pattern.Length; i++)
                {
                    for (int j = 0; j < springGroups[sg].Groups[0]; j++)
                    {
                        var pattern = "".PadLeft(springGroups[sg].Pattern.Length, '.');
                        var arr = pattern.ToCharArray();

                        for (int k = 0; k < springGroups[sg].Groups[0]; k++)
                        {
                            if (i + k < springGroups[sg].Pattern.Length)
                            {
                                arr[i + k] = '#';
                            }
                        }

                        patterns.Add(new string(arr));
                    }
                }

                var visitedGroups = 1;
                while (visitedGroups < springGroups[sg].Groups.Count)
                {
                    foreach (var pat in patterns)
                    {
                        for (int i = pat.LastIndexOf('#') + 2; i < springGroups[sg].Pattern.Length; i++)
                        {
                            var arr = pat.ToCharArray();
                            for (int k = 0; k < springGroups[sg].Groups[visitedGroups]; k++)
                            {
                                if (i + k < springGroups[sg].Pattern.Length)
                                {
                                    arr[i + k] = '#';
                                }
                            }
                            newPatterns.Add(new string(arr));
                        }
                    }
                    patterns.Clear();
                    patterns.AddRange(newPatterns);
                    newPatterns.Clear();
                    visitedGroups++;
                }

                springGroups[sg] = new SpringGroup(springGroups[sg].Pattern, springGroups[sg].Groups) { GeneratedPatterns = patterns };
            }

            for (int i = 0; i < springGroups.Count; i++)
            {
                var invalidPatterns = springGroups[i].GeneratedPatterns.Where(x => !PatternIsValid(x, springGroups[i].Groups, springGroups[i].Pattern));
                var newPatternList = springGroups[i].GeneratedPatterns.Except(invalidPatterns);
                springGroups[i] = new SpringGroup(springGroups[i].Pattern, springGroups[i].Groups) { GeneratedPatterns = newPatternList.ToList() };
            }

            Console.WriteLine(springGroups.Sum(x => x.GeneratedPatterns.Count));
        }

        static bool PatternIsValid(string pattern, List<int> springs, string originalPattern)
        {
            var regexStart = "^[.]{0,}";
            var regexEnd = "[.]{0,}$";
            var regexMid = "";

            for (int i = 0; i < springs.Count; i++)
            {
                regexMid += $"[#]{{{springs[i]}}}";
                if (i < springs.Count - 1)
                {
                    regexMid += "[.]{1,}";
                }
            }

            var regex = regexStart + regexMid + regexEnd;
            var isValidGeneratedPattern = Regex.Match(pattern, regex).Success;
            originalPattern = originalPattern.Replace(".", "[.]");
            var matchesWithOriginalPattern = Regex.Match(pattern, originalPattern.Replace("?", ".")).Success;

            return isValidGeneratedPattern && matchesWithOriginalPattern;
        }
    }

    struct SpringGroup(string pattern, List<int> groups)
    {
        public string Pattern { get; set; } = pattern;
        public List<string> GeneratedPatterns { get; set; }
        public List<int> Groups { get; set; } = groups;
    }
}

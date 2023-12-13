namespace AoC2023.Day13
{
    internal class DayThirteenController
    {
        public static void Run()
        {
            string[] input = File.ReadAllLines("Day13/input.txt");

            var springs = new List<List<string>>();
            springs.Add([]);

            for (int i = 0; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    springs.Add([]);
                }
                else
                {
                    springs.Last().Add(input[i]);
                }
            }

            var sum = 0;

            foreach (var spring in springs)
            {
                for (int y = 1; y < spring.Count; y++)
                {
                    var upTiles = new List<string>();
                    var downTiles = new List<string>();

                    for (int x = 0; x < spring[0].Length; x++)
                    {
                        var column = "";
                        for (int dy = 0; dy < spring.Count; dy++)
                        {
                            column += spring[dy][x].ToString();
                        }

                        var up = column[..y];
                        var down = new string(column.Skip(y).Take(spring.Count - y).ToArray());

                        if (up.Length < down.Length)
                        {
                            down = new string(down.Take(up.Length).Reverse().ToArray());
                        }
                        else if (down.Length < up.Length)
                        {
                            up = new string(up.Reverse().Take(down.Length).ToArray());
                        }

                        upTiles.Add(up);
                        downTiles.Add(down);
                    }

                    var isReflection = true;

                    for (int i = 0; i < upTiles.Count; i++)
                    {
                        if (upTiles[i] != downTiles[i])
                        {
                            isReflection = false; break;
                        }
                    }

                    if (isReflection)
                    {
                        sum += y * 100;
                    }
                }
            }

            foreach (var spring in springs)
            {
                for (int x = 1; x < spring[0].Length; x++)
                {
                    var leftTiles = new List<string>();
                    var rightTiles = new List<string>();

                    for (int y = 0; y < spring.Count; y++)
                    {
                        var row = spring[y];

                        var left = row[..x];
                        var right = new string(row.Skip(x).Take(spring[0].Length - x).ToArray());

                        if (left.Length < right.Length)
                        {
                            right = new string(right.Take(left.Length).Reverse().ToArray());
                        }
                        else if (right.Length < left.Length)
                        {
                            left = new string(left.Reverse().Take(right.Length).ToArray());
                        }

                        leftTiles.Add(left);
                        rightTiles.Add(right);
                    }

                    var isReflection = true;

                    for (int i = 0; i < leftTiles.Count; i++)
                    {
                        if (leftTiles[i] != rightTiles[i])
                        {
                            isReflection = false; break;
                        }
                    }

                    if (isReflection)
                    {
                        sum += x;
                    }
                }
            }

            Console.WriteLine(sum);
        }
    }
}

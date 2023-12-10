namespace AoC2023.Day10
{
    internal class DayTenController
    {
        static readonly string[] input = File.ReadAllLines("Day10/input.txt");
        static readonly char[,] map = new char[input.Length, input[0].Length];
        static readonly (int xmin, int xmax) xBound = (0, input[0].Length - 1);
        static readonly (int ymin, int ymax) yBound = (0, input.Length - 1);

        public static void Run()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = '°';
                }
            }

            (int x, int y) start = input.SelectMany((line, y) => line.Where(x => x == 'S').Select(s => (line.IndexOf(s), y))).First();
            var startPipe = new Pipe(start.x, start.y, Direction.Undefined, PipeTile.Start, null, null, null, null);
            map[start.y, start.x] = 'S';

            startPipe = GetConnectedSuroundingPipes(startPipe, start.x, start.y);
            var currentPipe = startPipe;

            if (currentPipe.NorthPipe != null)
            {
                currentPipe = GetPipeInDirection(Direction.North, currentPipe.PipeTile, currentPipe.X, currentPipe.Y);
            }
            else if (currentPipe.EastPipe != null)
            {
                currentPipe = GetPipeInDirection(Direction.East, currentPipe.PipeTile, currentPipe.X, currentPipe.Y);
            }
            else if (currentPipe.SouthPipe != null)
            {
                currentPipe = GetPipeInDirection(Direction.South, currentPipe.PipeTile, currentPipe.X, currentPipe.Y);
            }
            else
            {
                currentPipe = GetPipeInDirection(Direction.West, currentPipe.PipeTile, currentPipe.X, currentPipe.Y);
            }

            map[currentPipe.Y, currentPipe.X] = '─';
            var count = 1;

            while (currentPipe.PipeTile != PipeTile.Start)
            {
                if (currentPipe.ComingFrom != Direction.North && currentPipe.NorthPipe != null)
                {
                    currentPipe = GetPipeInDirection(Direction.North, currentPipe.PipeTile, currentPipe.X, currentPipe.Y);
                }
                else if (currentPipe.ComingFrom != Direction.East && currentPipe.EastPipe != null)
                {
                    currentPipe = GetPipeInDirection(Direction.East, currentPipe.PipeTile, currentPipe.X, currentPipe.Y);
                }
                else if (currentPipe.ComingFrom != Direction.South && currentPipe.SouthPipe != null)
                {
                    currentPipe = GetPipeInDirection(Direction.South, currentPipe.PipeTile, currentPipe.X, currentPipe.Y);
                }
                else if (currentPipe.ComingFrom != Direction.West && currentPipe.WestPipe != null)
                {
                    currentPipe = GetPipeInDirection(Direction.West, currentPipe.PipeTile, currentPipe.X, currentPipe.Y);
                }

                switch (currentPipe.PipeTile)
                {
                    case PipeTile.Vertical:
                        map[currentPipe.Y, currentPipe.X] = '│';
                        break;
                    case PipeTile.Horizontal:
                        map[currentPipe.Y, currentPipe.X] = '─';
                        break;
                    case PipeTile.BendNE:
                        map[currentPipe.Y, currentPipe.X] = '└';
                        break;
                    case PipeTile.BendSE:
                        map[currentPipe.Y, currentPipe.X] = '┌';
                        break;
                    case PipeTile.BendSW:
                        map[currentPipe.Y, currentPipe.X] = '┐';
                        break;
                    case PipeTile.BendNW:
                        map[currentPipe.Y, currentPipe.X] = '┘';
                        break;
                }

                count++;
            }

            Console.WriteLine(count / 2);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static Pipe GetPipeInDirection(Direction direction, PipeTile? current, int x, int y)
        {
            var pipe = new Pipe(x, y, null, PipeTile.Start, null, null, null, null);

            if (direction == Direction.North && y > yBound.ymin)
            {
                var c = input[y - 1][x];
                pipe.Y -= 1;
                pipe.ComingFrom = Direction.South;
                pipe.PipeTile = GetPipeTileFromCharInDirection(Direction.North, pipe.SouthPipe, c);
                pipe = GetConnectedSuroundingPipes(pipe, pipe.X, pipe.Y);
            }
            else if (direction == Direction.East && x < xBound.xmax)
            {
                var c = input[y][x + 1];
                pipe.X += 1;
                pipe.ComingFrom = Direction.West;
                pipe.PipeTile = GetPipeTileFromCharInDirection(Direction.East, pipe.WestPipe, c);
                pipe = GetConnectedSuroundingPipes(pipe, pipe.X, pipe.Y);
            }
            else if (direction == Direction.South && y < yBound.ymax)
            {
                var c = input[y + 1][x];
                pipe.Y += 1;
                pipe.ComingFrom = Direction.North;
                pipe.PipeTile = GetPipeTileFromCharInDirection(Direction.South, pipe.NorthPipe, c);
                pipe = GetConnectedSuroundingPipes(pipe, pipe.X, pipe.Y);
            }
            // West
            else
            {
                var c = input[y][x - 1];
                pipe.X -= 1;
                pipe.ComingFrom = Direction.East;
                pipe.PipeTile = GetPipeTileFromCharInDirection(Direction.West, pipe.EastPipe, c);
                pipe = GetConnectedSuroundingPipes(pipe, pipe.X, pipe.Y);
            }

            return pipe;
        }

        static Pipe GetConnectedSuroundingPipes(Pipe pipe, int x, int y)
        {
            if (y > yBound.ymin)
            {
                var c = input[y - 1][x];
                pipe.NorthPipe = GetPipeTileFromCharInDirection(Direction.North, pipe.PipeTile, c);
            }
            if (x < xBound.xmax)
            {
                var c = input[y][x + 1];
                pipe.EastPipe = GetPipeTileFromCharInDirection(Direction.East, pipe.PipeTile, c);
            }
            if (y < yBound.ymax)
            {
                var c = input[y + 1][x];
                pipe.SouthPipe = GetPipeTileFromCharInDirection(Direction.South, pipe.PipeTile, c);
            }
            if (x > xBound.xmin)
            {
                var c = input[y][x - 1];
                pipe.WestPipe = GetPipeTileFromCharInDirection(Direction.West, pipe.PipeTile, c);
            }

            return pipe;
        }

        static PipeTile? GetPipeTileFromCharInDirection(Direction direction, PipeTile? previousPipe, char c)
        {
            if (direction == Direction.North)
            {
                return c switch
                {
                    '|' => previousPipe != PipeTile.Horizontal && previousPipe != PipeTile.BendSW && previousPipe != PipeTile.BendSE ? PipeTile.Vertical : null,
                    '7' => previousPipe != PipeTile.Horizontal && previousPipe != PipeTile.BendSW && previousPipe != PipeTile.BendSE ? PipeTile.BendSW : null,
                    'F' => previousPipe != PipeTile.Horizontal && previousPipe != PipeTile.BendSW && previousPipe != PipeTile.BendSE ? PipeTile.BendSE : null,
                    //'S' => PipeTile.Start,
                    _ => null
                };
            }
            else if (direction == Direction.East)
            {
                // knowing that start behaves like horizontal
                return c switch
                {
                    '-' => previousPipe != PipeTile.Vertical && previousPipe != PipeTile.BendSW && previousPipe != PipeTile.BendNW ? PipeTile.Horizontal : null,
                    'J' => previousPipe != PipeTile.Vertical && previousPipe != PipeTile.BendSW && previousPipe != PipeTile.BendNW ? PipeTile.BendNW : null,
                    '7' => previousPipe != PipeTile.Vertical && previousPipe != PipeTile.BendSW && previousPipe != PipeTile.BendNW ? PipeTile.BendSW : null,
                    'S' => previousPipe != PipeTile.Vertical && previousPipe != PipeTile.BendSW && previousPipe != PipeTile.BendNW ? PipeTile.Start : null,
                    _ => null
                };
            }
            else if (direction == Direction.South)
            {
                return c switch
                {
                    '|' => previousPipe != PipeTile.Horizontal && previousPipe != PipeTile.BendNW && previousPipe != PipeTile.BendNE ? PipeTile.Vertical : null,
                    'L' => previousPipe != PipeTile.Horizontal && previousPipe != PipeTile.BendNW && previousPipe != PipeTile.BendNE ? PipeTile.BendNE : null,
                    'J' => previousPipe != PipeTile.Horizontal && previousPipe != PipeTile.BendNW && previousPipe != PipeTile.BendNE ? PipeTile.BendNW : null,
                    //'S' => PipeTile.Start,
                    _ => null
                };
            }
            else
            {
                // knowing that start behaves like horizontal
                return c switch
                {
                    '-' => previousPipe != PipeTile.Vertical && previousPipe != PipeTile.BendNE && previousPipe != PipeTile.BendSE ? PipeTile.Horizontal : null,
                    'L' => previousPipe != PipeTile.Vertical && previousPipe != PipeTile.BendNE && previousPipe != PipeTile.BendSE ? PipeTile.BendNE : null,
                    'F' => previousPipe != PipeTile.Vertical && previousPipe != PipeTile.BendNE && previousPipe != PipeTile.BendSE ? PipeTile.BendSE : null,
                    'S' => previousPipe != PipeTile.Vertical && previousPipe != PipeTile.BendNE && previousPipe != PipeTile.BendSE ? PipeTile.Start : null,
                    _ => null
                };
            }
        }
    }

    enum Direction
    {
        North,
        East,
        South,
        West,
        Undefined
    }

    struct Pipe(int x, int y, Direction? comingFrom, PipeTile pipeTile, PipeTile? northPipe, PipeTile? eastPipe, PipeTile? southPipe, PipeTile? westPipe)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public Direction? ComingFrom { get; set; } = comingFrom;
        public PipeTile? PipeTile { get; set; } = pipeTile;
        public PipeTile? NorthPipe { get; set; } = northPipe;
        public PipeTile? EastPipe { get; set; } = eastPipe;
        public PipeTile? SouthPipe { get; set; } = southPipe;
        public PipeTile? WestPipe { get; set; } = westPipe;
    }

    enum PipeTile
    {
        Start,
        Vertical,
        Horizontal,
        BendNE,
        BendNW,
        BendSW,
        BendSE
    }
}

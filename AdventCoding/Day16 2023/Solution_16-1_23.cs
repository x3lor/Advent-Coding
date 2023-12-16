public class Solution_16_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        string[] grid = Input_16_23.input.Split('\n');
        string[] shaddow = new string[grid.Length];

        var loopDetection = new List<PositionAndDirection>();

        for (int i=0; i<grid.Length; i++) {
            shaddow[i] = new string('.', grid[0].Length);
        }

        var stack = new Stack<PositionAndDirection>();
        stack.Push(new PositionAndDirection(0, 0, Direction.Right));

        while (stack.Count > 0) {

            var current = stack.Pop();

            var x = current.X;
            var y = current.Y;
            var direction = current.Direction;

            while (true) {

                // if out of grid -> continue
                if (x < 0 || x >= grid[0].Length || y < 0 || y >= grid.Length)
                    break;

                shaddow[y] = shaddow[y].Remove(x, 1).Insert(x, "#");

                // if out of grid -> continue
                if (x < 0 || x >= grid[0].Length || y < 0 || y >= grid.Length)
                    break;

                if (grid[y][x] == '-' && loopDetection.Any(l => l.X == x && l.Y==y) && (direction == Direction.Top || direction == Direction.Bottom)) {
                    break;            
                }

                if (grid[y][x] == '|' && loopDetection.Any(l => l.X == x && l.Y==y) && (direction == Direction.Left || direction == Direction.Right)) {
                    break;            
                }

                // handle current position
                switch (grid[y][x]) {
                    case '.': { 
                        break; 
                    }
                    case '\\': {
                        switch (direction) {
                            case Direction.Top:    direction = Direction.Left;   break;
                            case Direction.Bottom: direction = Direction.Right;  break;
                            case Direction.Left:   direction = Direction.Top;    break;
                            case Direction.Right:  direction = Direction.Bottom; break;
                        }
                        break;
                    }
                    case '/': {
                        switch (direction) {
                            case Direction.Top:    direction = Direction.Right;   break;
                            case Direction.Bottom: direction = Direction.Left;  break;
                            case Direction.Left:   direction = Direction.Bottom;    break;
                            case Direction.Right:  direction = Direction.Top; break;
                        }
                        break;
                    }
                    case '-': {
                        switch (direction) {
                            case Direction.Top:    {
                                direction = Direction.Left; 
                                stack.Push(new PositionAndDirection(x, y, Direction.Right)); 
                                loopDetection.Add(new PositionAndDirection(x, y, Direction.Left));
                                loopDetection.Add(new PositionAndDirection(x, y, Direction.Right));
                                break;
                            }
                            case Direction.Bottom: {
                                direction = Direction.Left; 
                                stack.Push(new PositionAndDirection(x, y, Direction.Right));
                                loopDetection.Add(new PositionAndDirection(x, y, Direction.Left));
                                loopDetection.Add(new PositionAndDirection(x, y, Direction.Right));
                                break;}
                            case Direction.Left:   break;
                            case Direction.Right:  break;
                        }
                        break;
                    }

                    case '|': {
                        switch (direction) {
                            case Direction.Top:    break;
                            case Direction.Bottom: break;
                            case Direction.Left:   {
                                direction = Direction.Top; 
                                stack.Push(new PositionAndDirection(x, y, Direction.Bottom));
                                loopDetection.Add(new PositionAndDirection(x, y, Direction.Top));
                                loopDetection.Add(new PositionAndDirection(x, y, Direction.Bottom));
                                break;
                            }
                            case Direction.Right:  {
                                direction = Direction.Top; 
                                stack.Push(new PositionAndDirection(x, y, Direction.Bottom));
                                loopDetection.Add(new PositionAndDirection(x, y, Direction.Top));
                                loopDetection.Add(new PositionAndDirection(x, y, Direction.Bottom));
                                break;
                            }
                        }
                        break;
                    }
                }
            
                // do step
                switch (direction) {
                    case Direction.Top:    y--; break;
                    case Direction.Bottom: y++; break;
                    case Direction.Left:   x--; break;
                    case Direction.Right:  x++; break;
                }

                // if out of grid -> continue
                if (x < 0 || x >= grid[0].Length || y < 0 || y >= grid.Length)
                    break;
            }
        }

        var sum = 0;

        foreach(var line in shaddow) {
            sum += line.Count(c => c == '#');
        }

        Console.WriteLine($"Done! Sum: {sum}");
    }

    public enum Direction {
        Top, Left, Bottom, Right
    }

    class PositionAndDirection {

        public PositionAndDirection(int x, int y, Direction direction) {
            X = x;
            Y = y;
            Direction = direction;
        }

        public int X { get; }
        public int Y { get; }

        public Direction Direction { get; }
    }
}
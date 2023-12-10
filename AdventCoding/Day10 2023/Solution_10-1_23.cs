public class Solution_10_1_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting...");

        string[] grid = Input_10_23.input.Split('\n');

        var posX = 0;
        var posY = 0;

        // find S
        for (int y=0; y<grid.Length; y++) {
            if (grid[y].Contains('S')) {
                posY = y;
                posX = grid[y].IndexOf('S')-1;
                break;
            }
        }
        
        var steps = 1;
        var commingFrom = Direction.Right;

        while (grid[posY][posX] != 'S') {

            switch (grid[posY][posX]) {
                case '|': {
                    if (commingFrom == Direction.Top) {
                        posY++;
                    } else {
                        posY--;
                    }
                    break;
                }
                case '-': {
                    if (commingFrom == Direction.Left) {
                        posX++;
                    } else {
                        posX--;
                    }
                    break;
                }
                case 'L': {
                    if (commingFrom == Direction.Top) {
                        posX++;
                        commingFrom = Direction.Left;
                    } else {
                        posY--;
                        commingFrom = Direction.Bottom;
                    }
                    break;
                }
                case 'J': {
                    if (commingFrom == Direction.Top) {
                        posX--;
                        commingFrom = Direction.Right;
                    } else {
                        posY--;
                        commingFrom = Direction.Bottom;
                    }
                    break;
                }
                case '7': {
                    if (commingFrom == Direction.Left) {
                        posY++;
                        commingFrom = Direction.Top;
                    } else {
                        posX--;
                        commingFrom = Direction.Right;
                    }
                    break;
                }
                case 'F': {
                    if ( commingFrom == Direction.Right) {
                        posY++;
                        commingFrom = Direction.Top;
                    } else {
                        posX++;
                        commingFrom = Direction.Left;
                    }
                    break;
                }
                default: throw new Exception("Should not happen!");
            }

            steps++;

        }

        Console.WriteLine($"Done ... steps: {steps/2}");
    }

    public enum Direction {
        Left, Right, Top, Bottom
    }
}
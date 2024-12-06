
public class Solution_6_1_24 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var grid = new Grid(Input_6_24.input);
        grid.Start();
        Console.WriteLine($"done! Sum: {Grid.Counter}");
    }

    private class Grid {

        private char[,] grid;
        private int xStart;
        private int yStart;
        private int width;
        private int height;

        public Grid(string input) 
        {
            var lines = input.Split('\n').ToList();
            height = lines.Count;
            width = lines[0].Length;
            grid = CreateCharArray(lines, height, width);

            for (int y=0; y<lines.Count; y++) 
            {
                var x = lines[y].IndexOf('^');
                if (x != -1) 
                {
                    yStart = y;
                    xStart = x;
                    break;
                }
            }
        }

        static char[,] CreateCharArray(List<string> strings, int height, int width)
        {
            char[,] charArray = new char[height, width];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    charArray[y, x] = strings[y][x];
                
            return charArray;
        }

        private enum Direction { UP, RIGHT, DOWN, LEFT }

        public void Start() {

            var direction = Direction.UP;

            var currentX = xStart;
            var currentY = yStart;

            while (currentX >= 0 && currentX < width && 
                   currentY >= 0 && currentY < height) 
            {

                Mark(currentX, currentY);
                
                if (!CanMove(currentX, currentY, direction)) 
                {
                    direction = (Direction)(((int)direction+1)%4);
                    continue;
                }

                switch (direction) 
                {
                    case Direction.UP:    currentY--; break;
                    case Direction.RIGHT: currentX++; break;
                    case Direction.DOWN:  currentY++; break;
                    case Direction.LEFT:  currentX--; break;
                }
            }
        }

        private bool CanMove(int x, int y, Direction dir) {

            int nextX = x;
            int nextY = y;

            switch (dir) {
                case Direction.UP:    nextY--; break;
                case Direction.RIGHT: nextX++; break;
                case Direction.DOWN:  nextY++; break;
                case Direction.LEFT:  nextX--; break;
            }

            if (nextX < 0 || nextX >= width || nextY < 0 || nextY >= height)
                return true;

            return grid[nextY, nextX] != '#';
        }

        public static int Counter = 0;
        private void Mark(int x, int y)
        {
            if (grid[y,x] != 'X') 
            {
                grid[y,x] = 'X';
                Counter++;
            }
        }
    }
}
#pragma warning disable CS8602

using System.Drawing;

public class Solution_21_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var start = new Point(-1,-1);

        var grid = Input_21_23.input.Split('\n');
        var gridHeight = grid.Length;
        var gridWidth = grid[0].Length;

        for (int y=0; y<gridHeight; y++) {
            var indexOfS = grid[y].IndexOf('S');
            if (indexOfS != -1) {
                start = new Point(indexOfS, y);
                break;
            }
        }

        var currentSet = new HashSet<Point> { start };
        var nextSet = new HashSet<Point>();

        const int steps = 64;
        
        for (int i=0; i<steps; i++) {
            foreach (var point in currentSet) {

                var left = new Point(point.X-1, point.Y);
                if (left.X > 0 && IsFreeSpace(grid, left)) {
                    nextSet.Add(left);
                }

                var right = new Point(point.X+1, point.Y);
                if (right.X < gridWidth && IsFreeSpace(grid, right)) {
                    nextSet.Add(right);
                }

                var up = new Point(point.X, point.Y-1);
                if (up.X > 0 && IsFreeSpace(grid, up)) {
                    nextSet.Add(up);
                }

                var down = new Point(point.X, point.Y+1);
                if (down.X < gridHeight && IsFreeSpace(grid, down)) {
                    nextSet.Add(down);
                }
            }

            currentSet = nextSet;
            nextSet = new HashSet<Point>();
        }
        
        Console.WriteLine($"Done! fields: {currentSet.Count}");
    }

    private static bool IsFreeSpace(string[] grid, Point p) {
        return grid[p.Y][p.X] == '.' || grid[p.Y][p.X] == 'S';
    }

    

}
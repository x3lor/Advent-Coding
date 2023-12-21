#pragma warning disable CS8602

using System.Drawing;

public class Solution_21_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var start = new Point(-1,-1);

        var oldgrid = Input_21_23.input.Split('\n');

        var gridHeight = oldgrid.Length;
        var gridWidth = oldgrid[0].Length;

        for (int y=0; y<gridHeight; y++) {
            var indexOfS = oldgrid[y].IndexOf('S');
            if (indexOfS != -1) {
                start = new Point(indexOfS+gridWidth, y+gridHeight);
                break;
            }
        }

        for (int y=0; y<gridHeight; y++) {
            oldgrid[y] = oldgrid[y] + oldgrid[y] + oldgrid[y];
        }

        var grid = new string[gridHeight*3];
        for (int y=0; y<gridHeight; y++) {
            for (int sub=0; sub<3; sub++) {
                grid[y+sub*gridHeight] = oldgrid[y];
            }
        }

        gridHeight = grid.Length;
        gridWidth = grid[0].Length;

        var currentSet = new HashSet<Point> { start };
        var nextSet = new HashSet<Point>();

        const int steps = 65+131;
        
        for (int i=0; i<steps; i++) {
            foreach (var point in currentSet) {

                var left = new Point(point.X-1, point.Y);
                if (left.X >= 0 && IsFreeSpace(grid, left)) {
                    nextSet.Add(left);
                }

                var right = new Point(point.X+1, point.Y);
                if (right.X < gridWidth && IsFreeSpace(grid, right)) {
                    nextSet.Add(right);
                }

                var up = new Point(point.X, point.Y-1);
                if (up.X >= 0 && IsFreeSpace(grid, up)) {
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
        
        foreach(var point in currentSet) {
            MarkGrid(grid, point);
        }

        PrintGrid(grid);

        Console.WriteLine($"Done! fields: {currentSet.Count}");
    }

    private static bool IsFreeSpace(string[] grid, Point p) {
        return grid[p.Y][p.X] == '.' || grid[p.Y][p.X] == 'S';
    }

    private void MarkGrid(string[] grid, Point p) {
        grid[p.Y] = grid[p.Y].Remove(p.X, 1).Insert(p.X, "O");
    }

    private void PrintGrid(string[] grid) {
        using var writer = new StreamWriter("debug.txt");

        foreach(var line in grid) {
            writer.WriteLine(line);
        }
    }

}
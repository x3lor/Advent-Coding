#pragma warning disable CS8602

using System.Drawing;

public class Solution_21_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var start = new Point(-1,-1);
        const int strechFactor = 7;

        var oldgrid = Input_21_23.input.Split('\n');

        var gridHeight = oldgrid.Length;
        var gridWidth = oldgrid[0].Length;

        for (int y=0; y<gridHeight; y++) {
            var indexOfS = oldgrid[y].IndexOf('S');
            if (indexOfS != -1) {
                start = new Point(indexOfS+gridWidth*(strechFactor/2), y+gridHeight*(strechFactor/2));
                break;
            }
        }

        for (int y=0; y<gridHeight; y++) {
            oldgrid[y] = string.Concat(Enumerable.Repeat(oldgrid[y], strechFactor));;
        }

        var grid = new string[gridHeight*strechFactor];
        for (int y=0; y<gridHeight; y++) {
            for (int sub=0; sub<strechFactor; sub++) {
                grid[y+sub*gridHeight] = oldgrid[y];
            }
        }

        gridHeight = grid.Length;
        gridWidth = grid[0].Length;

        var currentSet = new HashSet<Point> { start };
        var nextSet = new HashSet<Point>();

        const int steps = 65+(131*(strechFactor/2));
        
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

        var half = (strechFactor/2);

        var sumFullG         = GetSteppingPoints(grid, half,1);       
        var sumFullK         = GetSteppingPoints(grid, half,2);       
        var sumLeftTop       = GetSteppingPoints(grid, half+1,strechFactor-1);        
        var sumLeftBottom    = GetSteppingPoints(grid, half+1,0);
        var sumRightTop      = GetSteppingPoints(grid, half-1,strechFactor-1);
        var sumRightBottom   = GetSteppingPoints(grid, half-1,0);
        var sumCornerTop     = GetSteppingPoints(grid, half,0);
        var sumCornerRight   = GetSteppingPoints(grid, strechFactor-1,half);
        var sumCornerBottom  = GetSteppingPoints(grid, half,strechFactor-1);
        var sumCornerLeft    = GetSteppingPoints(grid, 0,half);
        var sum34topLeft     = GetSteppingPoints(grid, half+1,strechFactor-2);
        var sum34topRight    = GetSteppingPoints(grid, 1,half+1);
        var sum34bottomLeft  = GetSteppingPoints(grid, half+1,1);
        var sum34bottomRight = GetSteppingPoints(grid, half-1,1);
        var sumMiddle        = GetSteppingPoints(grid, half,half);

        // Console.WriteLine(GetSteppingPoints(grid, 3,1));
        // Console.WriteLine(GetSteppingPoints(grid, 2,2));
        // Console.WriteLine(GetSteppingPoints(grid, 2,3));
        // Console.WriteLine(GetSteppingPoints(grid, 2,4));
        // Console.WriteLine(GetSteppingPoints(grid, 1,3));
        // Console.WriteLine(GetSteppingPoints(grid, 2,3));
        // Console.WriteLine(GetSteppingPoints(grid, 3,3));
        // Console.WriteLine(GetSteppingPoints(grid, 4,3));
        // Console.WriteLine(GetSteppingPoints(grid, 5,3));
        // Console.WriteLine(GetSteppingPoints(grid, 2,4));
        // Console.WriteLine(GetSteppingPoints(grid, 3,4));
        // Console.WriteLine(GetSteppingPoints(grid, 4,4));
        // Console.WriteLine(GetSteppingPoints(grid, 3,5));


        var factor = (26501365L-65)/131;
       // var factor = (steps-65)/131;
        
        var result = factor*factor*sumFullG+
                     (factor-1)*(factor-1)*sumFullK+
                     (factor-1)*sum34bottomLeft+
                     (factor-1)*sum34bottomRight+
                     (factor-1)*sum34topLeft+
                     (factor-1)*sum34topRight+
                     sumCornerBottom+sumCornerLeft+sumCornerRight+sumCornerTop+
                     factor*sumLeftTop+
                     factor*sumLeftBottom+
                     factor*sumRightTop+
                     factor*sumRightBottom;

        Console.WriteLine($"Done! result: {result}");

        var alternateSum=0;
        for (int y=0; y<gridHeight; y++) {
            for (int x=0; x<gridWidth; x++) {
                if (IsSteppingPoint(grid, new Point(x, y)))
                    alternateSum++;
            }
        }

        Console.WriteLine("Alternate sum: " + alternateSum);
    }

    private static long GetSteppingPoints(string[] grid, int factorX, int factorY) {
        var origin = new Point(131*factorX, 131*factorY);
        var sum = 0;
        for (int y=0; y<131; y++) {
            for (int x=0; x<131; x++) {
                var current = new Point(x+origin.X,y+origin.Y);
                if (IsSteppingPoint(grid, current))
                    sum++;
            }
        }
        return sum;
    } 

    private static bool IsSteppingPoint(string[] grid, Point p) {
         return grid[p.Y][p.X] == 'O';
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
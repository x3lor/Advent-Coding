#pragma warning disable CS8602

public class Solution_17_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var lines = Input_17_23.example.Split('\n');

        var gridHeight = lines.Length;
        var gridWidth = lines[0].Length;

        var grid = new char[gridHeight,gridWidth];
        var distancGrid = new int[gridHeight,gridWidth];

        for (int y=0; y<gridHeight; y++) {
            for (int x=0; x<gridWidth; x++) {
                grid[y, x] = lines[y][x];
                distancGrid[y, x] = int.MaxValue;
            }
        }
        distancGrid[0,0] = 0;

        var pathStack = new Stack<Path>();
        pathStack.Push(new Path());

        while (pathStack.Count > 0) {

            var current = pathStack.Pop();

            if (current.IsLeftPossible()) {
                
            }



        }




        

        Console.WriteLine($"Done! shortest path: " + distancGrid[gridHeight-1, gridWidth-1]);
    }

    private class Path {

        private readonly List<Point> path;

        public Path() {
            path = new List<Point>() { new Point(0,0)};
            Distance = 0;
        }

        private Path(List<Point> path, int distance) {
            this.path = path;
            Distance = distance;
        }
        
        public int Distance { get; set; }

        public Path AppendItem(Point point, int value) {
            var listCopy = path.ToList();
            listCopy.Add(point);
            return new Path(listCopy, Distance+value);
        }

        public bool IsLeftPossible() {
            var current = path.Last();

            if (current.X-1 < 0) return false;
            if (path.Count < 4) return true;

            if (path[^1].X+1 == current.X)
                return false;

            var xFourBack = path[^4].X;
            for (int i=path.Count-4; i<path.Count; i++) {
                if (path[i].X != xFourBack)
                    return true;
            }

            return false;
        }

        public bool IsRightPossible(int gridWidth) {
            var current = path.Last();

            if (current.X+1 >= gridWidth) return false;
            if (path.Count < 4) return true;

            if (path[^1].X-1 == current.X)
                return false;

            var xFourBack = path[^4].X;
            for (int i=path.Count-4; i<path.Count; i++) {
                if (path[i].X != xFourBack)
                    return true;
            }

            return false;
        }

        public bool IsUpPossible() {
            var current = path.Last();

            if (current.Y-1 < 0) return false;
            if (path.Count < 4) return true;

            if (path[^1].Y+1 == current.Y)
                return false;

            var yFourBack = path[^4].Y;
            for (int i=path.Count-4; i<path.Count; i++) {
                if (path[i].Y != yFourBack)
                    return true;
            }

            return false;
        }

        public bool IsDownPossible(int gridHeight) {
            var current = path.Last();

            if (current.Y+1 >= gridHeight) return false;
            if (path.Count < 4) return true;

            if (path[^1].Y-1 == current.Y)
                return false;

            var yFourBack = path[^4].Y;
            for (int i=path.Count-4; i<path.Count; i++) {
                if (path[i].Y != yFourBack)
                    return true;
            }

            return false;
        }
    }

    private class Point {

        public Point(int x, int y) {
            X=x;
            Y=y;
        }

        public int X { get; }
        public int Y { get; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Point) return false;
            var point = obj as Point;
            return point.X == X && point.Y == Y;
        }

        public static bool operator ==(Point obj1, Point obj2)
        {
            if (ReferenceEquals(obj1, obj2)) return true;
            if (obj1 is null) return false;
            if (obj2 is null) return false;
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Point obj1, Point obj2) => !(obj1 == obj2);

        public override int GetHashCode()
        {
            return X.GetHashCode()^Y.GetHashCode();
        }

        public override string ToString()
        {
            return $"({X}|{Y})";
        }
    }
}
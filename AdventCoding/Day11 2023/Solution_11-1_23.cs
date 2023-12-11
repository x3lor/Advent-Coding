public class Solution_11_1_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting...");

        string[] grid = Input_11_23.input.Split('\n');

        var emptyLines = new List<int>();
        var emptyColumns = new List<int>();

        for (int y=0; y<grid.Length; y++) {
            if (grid[y].IndexOf('#') == -1) {
                emptyLines.Add(y);
            }
        }

        for (int x=0; x<grid[0].Length; x++) {
            bool found = false;
            for (int y=0; y<grid.Length; y++) {
                if (grid[y][x] == '#') {
                    found = true;
                    break;
                }
            }
            if (!found) {
                emptyColumns.Add(x);
            }
        }

        var galaxies = new List<Coord>();

        int onTopOfRows = 0;

        for (int y=0; y<grid.Length; y++) {

            if (emptyLines.Contains(y))
                onTopOfRows++;

            int onTopOfColumns = 0;

            for (int x=0; x<grid[0].Length; x++) {

                if (emptyColumns.Contains(x)) {
                    onTopOfColumns++;
                }

                if (grid[y][x] == '#') {
                    galaxies.Add(new Coord(x+onTopOfColumns, y+onTopOfRows));
                }

            }
        }

        var sum = 0;

        for (int i=0; i<galaxies.Count; i++) {
            for (int j=i+1; j<galaxies.Count; j++) {
                sum += getDistance(galaxies[i], galaxies[j]);
            }
        }

        Console.WriteLine($"Done! Sum: {sum}");
    }

    private int getDistance(Coord c1, Coord c2) {
        return Math.Abs(c1.X-c2.X)+Math.Abs(c1.Y-c2.Y);
    }

    private class Coord {
        public Coord(int x, int y) {
            X = x;
            Y = y;
        }

        public int X {get;}
        public int Y {get;}

        public override string ToString()
        {
            return $"({X};{Y})";
        }
    }
}
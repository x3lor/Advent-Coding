using System.Text;

public class Solution_13_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var grids = new List<Grid>();
        var tmpList = new List<string>();

        foreach (var line in Input_13_23.example.Split('\n')) {
            if (string.IsNullOrEmpty(line)) {
                grids.Add(new Grid(tmpList.ToList()));
                tmpList.Clear();
            } else {
                tmpList.Add(line);
            }
        }
        grids.Add(new Grid(tmpList.ToList()));

        var sum = 0;
        foreach(var grid in grids) {
            sum += grid.GetVerticalReflectionLines();
            sum += 100*grid.GetHorizontalReflectionLlines();
        }
        Console.WriteLine($"Done! Sum: {sum}");
    }

    public class Grid {

        private readonly List<string> grid;
        private readonly List<string> grid_transpose;

        public Grid(List<string> grid) {
            this.grid = grid;
            grid_transpose = new List<string>();

            for (int x=0; x<grid[0].Length; x++) {
                var sb = new StringBuilder();
                for (int y=0; y<grid.Count; y++) {
                    sb.Append(grid[y][x]);
                }
                grid_transpose.Add(sb.ToString());
            }
        }

        public int GetHorizontalReflectionLlines() {
            return GetReflectionNumber(grid);
        }

        public int GetVerticalReflectionLines() {
            return GetReflectionNumber(grid_transpose);
        }

        public static int GetReflectionNumber(List<string> grid) {
            for (int i=0; i<grid.Count-1; i++) {

                var foundReflection = true;

                for (var j=0; j<=i; j++) {
                    
                    if (i+j+1 >= grid.Count) {
                        break;
                    }
                 
                    if (grid[i-j] != grid[i+j+1])
                    {
                        foundReflection = false;
                        break;
                    }
                }

                if (foundReflection) {
                    return i+1;
                }
            }

            return 0;
        }
    }
}
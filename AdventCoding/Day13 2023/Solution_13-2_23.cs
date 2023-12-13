using System.Text;

public class Solution_13_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var grids = new List<Grid>();
        var tmpList = new List<string>();

        foreach (var line in Input_13_23.input.Split('\n')) {
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
            var oldNum = GetReflectionNumberForAGrid(grid);
            return GetReflectionNumber(grid, oldNum);
        }

        public int GetVerticalReflectionLines() {
            var oldNum = GetReflectionNumberForAGrid(grid_transpose);
            return GetReflectionNumber(grid_transpose, oldNum);
        }

        private static int GetReflectionNumber(List<string> grid, int oldNum) {

            for (int yChange=0; yChange<grid.Count; yChange++) {
                for (int xChange=0; xChange<grid[0].Length; xChange++) {

                    if (grid[yChange][xChange] == '#')
                        grid[yChange] = grid[yChange].Remove(xChange, 1).Insert(xChange, ".");
                    else 
                        grid[yChange] = grid[yChange].Remove(xChange, 1).Insert(xChange, "#");

                    var result = GetReflectionNumberForAGrid(grid);

                    if (grid[yChange][xChange] == '#')
                        grid[yChange] = grid[yChange].Remove(xChange, 1).Insert(xChange, ".");
                    else 
                        grid[yChange] = grid[yChange].Remove(xChange, 1).Insert(xChange, "#");

                    if (result != 0 && result != oldNum)
                        return result;
                }
            }

            return 0;
        }

        private static int GetReflectionNumberForAGrid(List<string> grid) {
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
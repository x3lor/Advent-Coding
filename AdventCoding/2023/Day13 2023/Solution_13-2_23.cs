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
            sum += grid.GetNumber();
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

        public int GetNumber() {

            var oldNumHorizontal = GetReflectionNumberForAGrid(grid, 0);
            var oldNumVertical   = GetReflectionNumberForAGrid(grid_transpose, 0);
                                                 
            var newHorizontal = GetReflectionNumber(grid, oldNumHorizontal);               
            if (newHorizontal != 0)                     
                return 100*newHorizontal;
                                   
            var newVertical = GetReflectionNumber(grid_transpose, oldNumVertical);                                              
            return newVertical;                                          
        }
       
        private static int GetReflectionNumber(List<string> grid, int oldNum) {

            for (int yChange=0; yChange<grid.Count; yChange++) {
                for (int xChange=0; xChange<grid[0].Length; xChange++) {

                    SwapGridItem(grid, xChange, yChange);
                    var result = GetReflectionNumberForAGrid(grid, oldNum);
                    SwapGridItem(grid, xChange, yChange);

                    if (result != 0) {                                        
                        return result;
                    }                        
                }
            }

            return 0;
        }

        private static void SwapGridItem(List<string> grid, int x, int y) {
             if (grid[y][x] == '#')
                grid[y] = grid[y].Remove(x, 1).Insert(x, ".");
            else 
                grid[y] = grid[y].Remove(x, 1).Insert(x, "#");
        }

        private static int GetReflectionNumberForAGrid(List<string> grid, int oldNr) {

            oldNr--;

            for (int i=0; i<grid.Count-1; i++) {

                if (i==oldNr)
                    continue;

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
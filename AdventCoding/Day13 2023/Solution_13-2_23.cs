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

        private static int GridNr = 0;

        private readonly List<string> grid;
        private readonly List<string> grid_transpose;
        private readonly int gridNr;

        public Grid(List<string> grid) {

            gridNr = GridNr;
            GridNr++;

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
            
            if (oldNumHorizontal != 0) {                                    
                var newHorizontal = GetReflectionNumber(grid, oldNumHorizontal);
                if (newHorizontal != 0) {                    
                    return 100*newHorizontal;
                } else {                   
                    var newVertical = GetReflectionNumber(grid_transpose, 0);
                    if (newVertical != 0) {                        
                        return newVertical;
                    }
                }                
            }
            if (oldNumVertical != 0) {                            
                var newVertical = GetReflectionNumber(grid_transpose, oldNumVertical);
                if (newVertical != 0) {
                    return newVertical;
                } else {                    
                    var newHorizontal = GetReflectionNumber(grid, 0);
                    if (newHorizontal != 0) {
                        return 100*newHorizontal;
                    }
                }
            }            
            return 0;
        }
       
        private static int GetReflectionNumber(List<string> grid, int oldNum) {

            for (int yChange=0; yChange<grid.Count; yChange++) {
                for (int xChange=0; xChange<grid[0].Length; xChange++) {

                    if (grid[yChange][xChange] == '#')
                        grid[yChange] = grid[yChange].Remove(xChange, 1).Insert(xChange, ".");
                    else 
                        grid[yChange] = grid[yChange].Remove(xChange, 1).Insert(xChange, "#");

                    var result = GetReflectionNumberForAGrid(grid, oldNum);

                    if (grid[yChange][xChange] == '#')
                        grid[yChange] = grid[yChange].Remove(xChange, 1).Insert(xChange, ".");
                    else 
                        grid[yChange] = grid[yChange].Remove(xChange, 1).Insert(xChange, "#");

                    if (result != 0 && result != oldNum) {                                        
                        return result;
                    }                        
                }
            }

            return 0;
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
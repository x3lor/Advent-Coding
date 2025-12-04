
public class Solution_4_1_25 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var resultCounter = 0L;

        var grid = Input_4_25.input.Split('\n').ToList();

        var gridWidth = grid[0].Length;
        var gridHeight = grid.Count;

        for(int y=0; y<gridHeight; y++)
        {
            for (int x=0; x<gridWidth; x++)
            {
                if (grid[y][x] == '@')
                {
                    if (Accessible(grid, x, y, gridWidth, gridHeight))
                    {
                        resultCounter++;
                    }
                }
            }
        }

        Console.WriteLine($"done! Sum: {resultCounter}");
    }

    private bool Accessible(List<string> grid, int x, int y, int gridWidth, int gridHeight)
    {
        var resultList = new List<bool>();

        if (x > 0)            if (grid[y  ][x-1] == '@') resultList.Add(true);
        if (x < gridWidth-1)  if (grid[y  ][x+1] == '@') resultList.Add(true);
        if (y > 0)            if (grid[y-1][x  ] == '@') resultList.Add(true);
        if (y < gridHeight-1) if (grid[y+1][x  ] == '@') resultList.Add(true);

        if (x > 0           && y > 0 )             if (grid[y-1][x-1] == '@') resultList.Add(true);
        if (x < gridWidth-1 && y > 0 )             if (grid[y-1][x+1] == '@') resultList.Add(true);
        if (x > 0           && y < gridHeight-1 )  if (grid[y+1][x-1] == '@') resultList.Add(true);
        if (x < gridWidth-1 && y < gridHeight-1 )  if (grid[y+1][x+1] == '@') resultList.Add(true);

        return resultList.Count(b => b) < 4;
    }
}
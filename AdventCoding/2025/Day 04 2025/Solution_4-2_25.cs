public class Solution_4_2_25 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var resultCounter = 0L;

        var grid = Input_4_25.input.Split('\n').ToList();

        var gridWidth = grid[0].Length;
        var gridHeight = grid.Count;

        var newGrid = new char[gridHeight,gridWidth];

        for(int y=0; y<gridHeight; y++)
            {
                for (int x=0; x<gridWidth; x++)
                {
                    newGrid[y,x] = grid[y][x];
                }
            }


        while (true)
        {
            var listForRemoval = new List<Tuple<int, int>>();

            for(int y=0; y<gridHeight; y++)
            {
                for (int x=0; x<gridWidth; x++)
                {
                    if (newGrid[y,x] == '@')
                    {
                        if (Accessible(newGrid, x, y, gridWidth, gridHeight))
                        {
                            listForRemoval.Add(new Tuple<int, int>(y,x));
                        }
                    }
                }
            }

            if (listForRemoval.Count == 0)
                break;

            resultCounter += listForRemoval.Count;

            foreach (var tuple in listForRemoval)
            {
                newGrid[tuple.Item1,tuple.Item2] = '.';
            }
        }

        Console.WriteLine($"done! Sum: {resultCounter}");
    }

    private bool Accessible(char[,] grid, int x, int y, int gridWidth, int gridHeight)
    {
        var resultList = new List<bool>();

        if (x > 0)            if (grid[y  ,x-1] == '@') resultList.Add(true);
        if (x < gridWidth-1)  if (grid[y  ,x+1] == '@') resultList.Add(true);
        if (y > 0)            if (grid[y-1,x  ] == '@') resultList.Add(true);
        if (y < gridHeight-1) if (grid[y+1,x  ] == '@') resultList.Add(true);

        if (x > 0           && y > 0 )             if (grid[y-1,x-1] == '@') resultList.Add(true);
        if (x < gridWidth-1 && y > 0 )             if (grid[y-1,x+1] == '@') resultList.Add(true);
        if (x > 0           && y < gridHeight-1 )  if (grid[y+1,x-1] == '@') resultList.Add(true);
        if (x < gridWidth-1 && y < gridHeight-1 )  if (grid[y+1,x+1] == '@') resultList.Add(true);

        return resultList.Count(b => b) < 4;
    }
}
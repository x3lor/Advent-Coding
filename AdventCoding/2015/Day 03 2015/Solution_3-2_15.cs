public class Solution_3_2_15 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");
        
        var resultList = new HashSet<Tuple<int, int>>();

        var currentSX = 0;
        var currentSY = 0;

        var currentRX = 0;
        var currentRY = 0;

        var santaTurn = true;

        resultList.Add(new Tuple<int, int>(currentSX, currentSY));

        foreach(var c in Input_3_15.input)
        {
            if (santaTurn)
            {
                switch(c)
                {
                    case '^': { currentSY--; break; }
                    case 'v': { currentSY++; break; }
                    case '>': { currentSX++; break; }
                    case '<': { currentSX--; break; }
                }
                resultList.Add(new Tuple<int, int>(currentSX, currentSY));
            } else
            {
                switch(c)
                {
                    case '^': { currentRY--; break; }
                    case 'v': { currentRY++; break; }
                    case '>': { currentRX++; break; }
                    case '<': { currentRX--; break; }
                }
                resultList.Add(new Tuple<int, int>(currentRX, currentRY));
            }

            santaTurn = !santaTurn;        
        }

        Console.WriteLine($"done! Sum: {resultList.Count}");
    }
}
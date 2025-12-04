using System.Runtime.Intrinsics.Arm;

public class Solution_3_1_15 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");
        
        var resultList = new HashSet<Tuple<int, int>>();

        var currentX = 0;
        var currentY = 0;

        resultList.Add(new Tuple<int, int>(currentX, currentY));

        foreach(var c in Input_3_15.input)
        {
            switch(c)
            {
                case '^': { currentY--; break; }
                case 'v': { currentY++; break; }
                case '>': { currentX++; break; }
                case '<': { currentX--; break; }
            }

            resultList.Add(new Tuple<int, int>(currentX, currentY));
        }

        Console.WriteLine($"done! Sum: {resultList.Count}");
    }
}
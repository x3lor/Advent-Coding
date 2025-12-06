using System.Runtime.Intrinsics.Arm;

public class Solution_6_1_15 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");            

        var grid = new bool[1000,1000];

        foreach(var line in Input_6_15.input.Split('\n'))
        {
            // turn off 539,243 through 559,965
            // toggle 720,196 through 897,994

            var parts = line.Split(' ');
            var toggle = parts[0] == "toggle";            
            var on = false;
            
            if (!toggle)
                on = parts[1] == "on";

            var numbersFrom = toggle ? parts[1] : parts[2];
            var numbersTo = toggle ? parts[3] : parts[4];

            var fromParts = numbersFrom.Split(',');
            var toParts = numbersTo.Split(',');

            var fromX = int.Parse(fromParts[0]);
            var fromY = int.Parse(fromParts[1]);

            var ToX = int.Parse(toParts[0]);
            var ToY = int.Parse(toParts[1]);

            for (int x=fromX; x<=ToX; x++)
            {
                for (int y=fromY; y<=ToY; y++)
                {
                    if (toggle)
                    {
                        grid[x,y] = !grid[x,y];
                    } else
                    {
                        grid[x,y] = on;
                    }
                }
            }
        }

        var resultCounter = 0;

        for (int x=0; x<=999; x++)
        {
            for (int y=0; y<=999; y++)
            {
                if (grid[x,y])
                    resultCounter++;                    
            }
        }

        Console.WriteLine($"done! Sum: {resultCounter}");
    }
}
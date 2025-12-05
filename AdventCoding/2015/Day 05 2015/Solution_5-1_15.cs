using System.Runtime.Intrinsics.Arm;

public class Solution_5_1_15 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");
        
        var resultCounter = 0;

        foreach(var line in Input_5_15.input.Split('\n'))
        {
            int countVocals = line.Count("aeiou".Contains);
            if (countVocals < 3) continue;

            bool doubleFound = false;
            for (int i=0; i<line.Length-1; i++)
            {
                if (line[i] == line[i + 1])
                {
                    doubleFound = true;
                    break;
                }
            }

            if (!doubleFound)
                continue;

            if (line.Contains("ab")) continue;
            if (line.Contains("cd")) continue;
            if (line.Contains("pq")) continue;
            if (line.Contains("xy")) continue;

            resultCounter++;
        }

        Console.WriteLine($"done! Sum: {resultCounter}");
    }
}
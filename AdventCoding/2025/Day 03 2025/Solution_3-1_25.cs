public class Solution_3_1_25 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var resultCounter = 0L;

        foreach (var line in Input_3_25.input.Split('\n'))
        {
            resultCounter += GetMax(line);
        }

        Console.WriteLine($"done! Sum: {resultCounter}");
    }
    
    private int GetMax(string s)
    {
        var max = Max(s, 9);
        
        var firstIndexOfMax = s.IndexOf(max.ToString());

        if (firstIndexOfMax == s.Length-1)
        {
            var nextMax = Max(s, max-1);
            return nextMax * 10 + max;
        } 

        return max * 10 + Max(s.Substring(firstIndexOfMax+1),9);
    }

    private int Max(string s, int start)
    {
        for (int i=start; i>0; i--)
        {
            var index = s.IndexOf(i.ToString());

            if (index != -1)
                return i;
        }

        return 0;
    }
}
public class Solution_3_2_25 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var resultCounter = 0L;

        foreach (var line in Input_3_25.input.Split('\n'))
        {
            resultCounter += GetMax(line, 12);
        }

        Console.WriteLine($"done! Sum: {resultCounter}");
    }
    
    private long GetMax(string s, int numberPosition)
    {
        var max = MaxNumber(s.Substring(0, s.Length-numberPosition+1));    
        var firstIndexOfMax = s.IndexOf(max.ToString());

        if (numberPosition == 1)
            return (long)max;
        else
            return (long)max * (long)Math.Pow(10, numberPosition-1) + GetMax(s.Substring(firstIndexOfMax+1), numberPosition-1);
    }

    private int MaxNumber(string s)
    {
        for (int i=9; i>0; i--)
        {
            var index = s.IndexOf(i.ToString());

            if (index != -1)
                return i;
        }

        return 0;
    }
}
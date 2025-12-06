public class Solution_6_2_15 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");
        
        var resultCounter = 0;

        foreach(var line in Input_5_15.input.Split('\n'))
        {
            if (!TestRepeatingPairs(line) || !TestRepeatWithMiddle(line))
                continue;

            resultCounter++;
        }

        Console.WriteLine($"done! Sum: {resultCounter}");
    }

    private bool TestRepeatingPairs(string s)
    {        
        for (int i=0; i<s.Length-3; i++)
        {
            if (s.Substring(i+2).Contains(s.Substring(i, 2)))
                return true;
        }

        return false;
    }

    private bool TestRepeatWithMiddle(string s)
    {
        for (int i=0; i<s.Length-2; i++)
        {
            if (s[i] == s[i+2])
                return true;
        }

        return false;
    }
}
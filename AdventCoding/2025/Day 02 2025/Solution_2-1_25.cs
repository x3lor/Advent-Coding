public class Solution_2_1_25 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var resultCounter = 0L;

        foreach (var line in Input_2_25.input.Split(','))
        {

            var indexOfDash = line.IndexOf('-');
            var min = long.Parse(line.Substring(0, indexOfDash));
            var max = long.Parse(line.Substring(indexOfDash + 1));

            for (long val=min; val <= max; val++)
            {
                if (Check(val))
                {
                    resultCounter += val;
                }
            }
        }

        Console.WriteLine($"done! Sum: {resultCounter}");
    }
    
    private bool Check(long val)
    {
        var s = val.ToString();

        if (s.Length % 2 != 0)
            return false;

        return s.Substring(0, s.Length/2) == s.Substring(s.Length/2) ;
    }
}
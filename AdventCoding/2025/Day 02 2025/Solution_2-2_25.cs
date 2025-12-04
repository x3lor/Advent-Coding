public class Solution_2_2_25 : ISolution
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
        var length = s.Length;

        for (int i = 1; i < length; i++)
        {
            if (length % i == 0)
            {
                if (Check(s, i))
                    return true;
            }
        }
        return false;
    }
    
    private bool Check(string s, int i)
    {
        var first = s.Substring(0, i);

        for(int pointer = i; pointer<s.Length; pointer+=i)
        {
            var tocheck = s.Substring(pointer, i);
            if (first != tocheck)
                return false;
        }

        return true;
    }
}
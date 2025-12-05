
public class Solution_5_1_25 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var ranges = Input_5_25.inputRanges.Split('\n').Select(line => new Range(line)).ToList();
        var result = Input_5_25.inputIDs.Split('\n').Count(line => ranges.Any(r => r.Test(long.Parse(line))));
       
        Console.WriteLine($"done! Sum: {result}");
    }

    private class Range
    {
        private readonly long from;
        private readonly long to;

        public Range(string init)
        {
            var parts = init.Split('-');
            from = long.Parse(parts[0]);
            to   = long.Parse(parts[1]);
        }

        public bool Test(long i)
        {
            return i>=from && i<=to;
        }
    }
}
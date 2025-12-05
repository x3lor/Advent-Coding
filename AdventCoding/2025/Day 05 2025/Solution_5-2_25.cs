using System.Numerics;

public class Solution_5_2_25 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var ranges = Input_5_25.inputRanges.Split('\n').Select(line => new Range(line)).ToList();
        
        var finalRanges = new List<Range>();

        foreach(var range in ranges)
        {
            var currentRange = range;

            while (finalRanges.Any(r => r.CanMerge(currentRange)))
            {
                var first = finalRanges.First(r => r.CanMerge(currentRange));
                finalRanges.Remove(first);
                var newRange = first.Merge(currentRange);
                currentRange = newRange; 
            }

            finalRanges.Add(currentRange);
        }

        var result = new BigInteger(0);

        foreach(var r in finalRanges)
        {
            result += r.NumberRange();
        }

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

        public Range(long from, long to)
        {
            this.from = from;
            this.to = to;
        }

        public BigInteger NumberRange()
        {
            return new BigInteger(to)-new BigInteger(from)+1;
        }

        public bool NumberwithinRage(long i)
        {
            return i>=from && i<=to;
        }

        public bool CanMerge(Range otherRange)
        {
            return NumberwithinRage(otherRange.from) || 
                   NumberwithinRage(otherRange.to) || 
                   otherRange.NumberwithinRage(from) || 
                   otherRange.NumberwithinRage(to);
        }

        public Range Merge(Range otherRange)
        {
            var values = new List<long>() {from, to, otherRange.from, otherRange.to};
            return new Range(values.Min(), values.Max());
        }
    }

    
}
public class Solution_4_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = 0;

        foreach(var line in Input_4.input.Split('\n')) {
            
            // Example: 12-45,46-95
            var parts = line.Split(',');

            var rangeLeft  = new NumberRange(parts[0]);
            var rangeRight = new NumberRange(parts[1]);
          
            if (rangeLeft.DoesOverlap(rangeRight) || rangeRight.DoesOverlap(rangeLeft)) {
                sum++;
            }   
        }

        Console.WriteLine($"done! Sum: {sum}");
    }    

    private class NumberRange {
        private int start;
        private int end;

        public NumberRange(string s) {
            var parts = s.Split('-');
            start = int.Parse(parts[0]);
            end   = int.Parse(parts[1]);
        }

        public bool DoesOverlap (NumberRange r) {
            return (r.start >= start && r.start <= end) || (r.end >= start && r.end <= end);
        }
    }
}
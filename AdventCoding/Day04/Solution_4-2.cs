public class Solution_4_2 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting");

        // 12-45,46-95
        var input = Input_4.input.Split('\n');

        var sum = 0;

        for(int i=0; i<input.Length; i++) {
            
            var currentLine = input[i];
            var parts = currentLine.Split(',');

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
            end = int.Parse(parts[1]);
        }

        public bool DoesOverlap (NumberRange r) {
            return (r.start >= start && r.start <= end) || (r.end >= start && r.end <= end);
        }
    }
}
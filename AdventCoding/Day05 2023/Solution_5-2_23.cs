using System.Text;

public class Solution_5_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting");

        var lines = Input_5_23.input.Split('\n').ToList();

        var seeds = new Stack<long>(lines[0].Substring(7).Split(' ').Select(num => long.Parse(num)).Reverse());
        var functions = new List<Function>();

        var linepointer = 3;

        while (linepointer < lines.Count) {
            var start = linepointer;

            while (linepointer < lines.Count && !string.IsNullOrWhiteSpace(lines[linepointer])) {
                linepointer++;
            }

            functions.Add(new Function(lines.GetRange(start, linepointer-start)));
            linepointer += 2;
        }

        var min = long.MaxValue;

        var total = seeds.Count;
        var counter = 0;

        while (seeds.Count > 0) {
            var from = seeds.Pop();
            var to = seeds.Pop();
            counter++;

            Console.WriteLine($"Doing ...({counter} of {total/2})");

            for (long i=from; i<from+to; i++) {
                var value = i;
                    foreach(var func in functions) {
                        value = func.Map(value);
                    }

            if (value < min)
                min = value;
            }
        }

        Console.WriteLine($"Done: {min}");
        
    }   

    public class Function {

        private List<SubFunction> subFunctions;

        public Function(List<string> init) {
            subFunctions = init.Select(line => new SubFunction(line)).ToList();
        }

        public long Map(long input) {

            foreach(var subfunc in subFunctions) {
                if (subfunc.IsInputApplicable(input))
                    return subfunc.Map(input);
            }

            return input;
        }
    } 

    public class SubFunction {

        public SubFunction(string init) {
            var parts = init.Split(' ');
            DestinationStart = long.Parse(parts[0]);
            SourceStart      = long.Parse(parts[1]);
            Range            = long.Parse(parts[2]);
        }

        public long DestinationStart { get; }
        public long SourceStart      { get; }
        public long Range            { get; }

        public bool IsInputApplicable(long input) {
            return input >= SourceStart && input < SourceStart+Range;
        }

        public long Map(long input) {
            return DestinationStart + (input-SourceStart);
        }
    }

}
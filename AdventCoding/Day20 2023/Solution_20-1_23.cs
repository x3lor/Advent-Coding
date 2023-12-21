public class Solution_20_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        
        Console.WriteLine($"Done! Sum:");
    }

    public enum Signal {
        Low, High, Invalid
    }

    

    public interface IModule {
        string Name { get; }
        void Input(List<Signal> input);
        Signal Output();
    }

    // %
    public class FlipFlop : IModule
    {
        private bool on;
        private Signal nextOutput;

        public FlipFlop(string name) {
            on = false;
            nextOutput = Signal.Invalid;
        }

        public string Name => throw new NotImplementedException();

        public void Input(List<Signal> input)
        {
            if (input.Count > 1)
                throw new NotImplementedException("flipflops should only get 1 input");
            if (input.Count == 0)
                nextOutput = Signal.Invalid;

            var inputS = input[0];
            if (inputS == Signal.High)
                nextOutput = Signal.Invalid;
            else
                nextOutput = on ? Signal.High : Signal.Low;

        }

        public Signal Output()
        {
            return nextOutput;
        }
    }

    public class Conjunction : IModule
    {
        private List<Signal> lastInput;

        public Conjunction(string name) {
            Name = name;
            lastInput = new List<Signal>() { Signal.Low };
        }

        public string Name { get; }

        public void Input(List<Signal> input)
        {
            lastInput = input;
        }

        public Signal Output()
        {
            return Signal.Low;
        }
    }

}
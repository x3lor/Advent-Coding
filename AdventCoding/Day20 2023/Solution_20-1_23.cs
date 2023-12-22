#pragma warning disable CS8618
#pragma warning disable CS8602

public class Solution_20_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        /* 
        @"broadcaster -> a, b, c
        %a -> b
        %b -> c
        %c -> inv
        &inv -> a";
        */

        var modules = new List<IModule>();

        foreach (var line in Input_20_23.input.Split('\n')) {
            var indexofArrow = line.IndexOf('-');

            var name = line[..(indexofArrow-1)];
            var receivers = line[(indexofArrow+2)..].Split(',').Select(s => s.Trim()).ToList();
            if (name == "broadcaster") {
                modules.Add(new BroadCast(receivers));
            } else if (name.StartsWith('%')) {
                modules.Add(new FlipFlop(name[1..], receivers));
            } else if (name.StartsWith('&')) {
                modules.Add(new Conjunction(name[1..], receivers));
            } else {
                throw new NotImplementedException("blubb");
            }
        }

        foreach (var conjunction in modules.Where(m => m is Conjunction)) {
            var inputs = modules.Where(m => m.Receivers.Contains(conjunction.Name)).Select(m => m.Name).ToList();
            (conjunction as Conjunction).SetInputs(inputs);
        }

        var modulesDict = modules.ToDictionary(m => m.Name, m=>m);

        var sumHigh = 0L;
        var sumLow = 0L;

        const int pushes = 1000;

        for (int i=0; i<pushes; i++) {
            
            sumLow++;
            List<Signal> initialSignals = modulesDict["broadcaster"].Compute(new("broadcaster", string.Empty, false));
            sumHigh += initialSignals.Count(s => s.High);
            sumLow  += initialSignals.Count(s => !s.High);

            var signalQueue = new Queue<Signal>(initialSignals);

            while (signalQueue.Count > 0) {

                var nextList = new List<Signal>();

                while (signalQueue.Count > 0) {

                    var next = signalQueue.Dequeue();

                    if (!modulesDict.ContainsKey(next.Receiver)) {
                        continue;
                    }
                    var receiver = modulesDict[next.Receiver];
                    var sigs = receiver.Compute(next);
                    nextList.AddRange(sigs);
                }

                sumHigh += nextList.Count(s => s.High);
                sumLow += nextList.Count(s => !s.High);

                foreach(var s in nextList) {
                    signalQueue.Enqueue(s);
                }
            }

        }

        var result = sumHigh*sumLow;
        Console.WriteLine($"Done! result: {result}");
    }

    public class Signal {
        
        public Signal(string receiver, string sender, bool signal) {
            Receiver = receiver;
            Sender = sender;
            High = signal;
        }

        public string Sender { get; }
        public string Receiver { get; }
        public bool High { get; set; }

        public override string ToString()
        {
            var sig = High ? "High" : "Low";
            return $"(S:{Sender}, R:{Receiver}, Signal:{sig})";
        }
    }

    public interface IModule {
        string Name { get; }
        List<string> Receivers { get; }
        List<Signal> Compute(Signal signal);
    }

    public class FlipFlop : IModule
    {
        private bool on;
        
        public FlipFlop(string name, List<string> receivers) {
            on = false;
            Name = name;
            Receivers = receivers;
        }

        public string Name { get; }
        public List<string> Receivers { get; }

        public List<Signal> Compute(Signal signal)
        {
            if (signal.High)
                return new List<Signal>();
            else {
                on = !on;
                var output = Receivers.Select(r => new Signal(r, Name, on)).ToList();
                return output;
            }
        }

        public override string ToString() => Name + " (FF)";
    }

    public class Conjunction : IModule
    {
        private List<Signal> lastInput;

        public Conjunction(string name, List<string> receivers) {
            Name = name;
            Receivers = receivers;
        }

        public void SetInputs(List<string> inputs) {
            lastInput = inputs.Select(i => new Signal(Name, i, false)).ToList();
        }

        public List<string> Receivers { get; }
        public string Name { get; }

        public List<Signal> Compute(Signal signal)
        {   
            var sig = lastInput.First(i => i.Sender==signal.Sender);
            sig.High = signal.High;

            var outSig = !lastInput.All(s => s.High);
            var output = Receivers.Select(r => new Signal(r, Name, outSig)).ToList();
            return output;
        }

        public override string ToString() => Name + " (Con)";
    }

    public class BroadCast : IModule
    {
        public BroadCast(List<string> receivers) {
            Receivers = receivers;
            Name = "broadcaster";
        }

        public List<string> Receivers { get; }
        public string Name { get; }

        public List<Signal> Compute(Signal signal)
        {
            var output = Receivers.Select(r => new Signal(r, Name, signal.High)).ToList();
            return output;
        }

        public override string ToString() => Name;
    }

}
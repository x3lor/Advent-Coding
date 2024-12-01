#pragma warning disable CS8618
#pragma warning disable CS8602

public class Solution_20_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var result = DoShit();
        
        Console.WriteLine($"Done! result: {result}");
    }

    private static long DoShit() {
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
        var pushes = 0L;

        while (true) {
                
            var initialSignals = modulesDict["broadcaster"].Compute(new("broadcaster", string.Empty, false));
            pushes++;
            if (pushes % 100 == 0)
                Console.Write("\r Pushes: " + pushes);

            var signalQueue = new Queue<Signal>(initialSignals);

            while (signalQueue.Count > 0) {

                var nextList = new List<Signal>();

                while (signalQueue.Count > 0) {

                    var next = signalQueue.Dequeue();

                    if (next.Receiver == "rx") {
                        if (next.High ==  false) {
                            return pushes;
                        }
                    }

                    if (!modulesDict.ContainsKey(next.Receiver)) {
                        continue;
                    }
                    var receiver = modulesDict[next.Receiver];
                    var sigs = receiver.Compute(next);
                    nextList.AddRange(sigs);
                }            

                foreach(var s in nextList) {
                    signalQueue.Enqueue(s);
                }
            }

        }

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
        IEnumerable<Signal> Compute(Signal signal);
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

        public IEnumerable<Signal> Compute(Signal signal)
        {
            if (signal.High)
                return new List<Signal>();
            else {
                on = !on;
                var output = Receivers.Select(r => new Signal(r, Name, on));
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

        public IEnumerable<Signal> Compute(Signal signal)
        {   
            var sig = lastInput.First(i => i.Sender==signal.Sender);
            sig.High = signal.High;

            var outSig = !lastInput.All(s => s.High);
            var output = Receivers.Select(r => new Signal(r, Name, outSig));
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

        public IEnumerable<Signal> Compute(Signal signal)
        {
            var output = Receivers.Select(r => new Signal(r, Name, signal.High));
            return output;
        }

        public override string ToString() => Name;
    }

}
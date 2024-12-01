public class Solution_8_2_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting...");

        var instructions = Input_8_23.input_instructions;
        var nodesDict = Input_8_23.input_nodes
                                  .Split('\n')
                                  .ToDictionary(node => node.Substring(0, 3), 
                                                node => node);       

        var nodes = nodesDict.Keys.Where(key => key.EndsWith('A'));
        var stepsList = new List<long>();

        foreach (var node in nodes) {

            var steps = 0;
            string currentPosition = nodesDict[node];
        
            while (currentPosition.Substring(2,1) != "Z") {

                var nextInstruction = instructions[steps%instructions.Length];

                currentPosition = nextInstruction == 'L' 
                                    ? nodesDict[currentPosition.Substring(7,3)] 
                                    : nodesDict[currentPosition.Substring(12,3)];

                steps++;
            }
            stepsList.Add(steps);
        }

        var result = LCM(stepsList);
        Console.WriteLine($"Done: {result}");
    }

    static long GCD(long n1, long n2) => (n2 == 0) ? n1 : GCD(n2, n1 % n2);

    public static long LCM(List<long> numbers)
    {
        return numbers.Aggregate((S, val) => S * val / GCD(S, val));
    }
}
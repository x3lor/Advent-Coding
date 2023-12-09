public class Solution_8_1_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting...");

        var instructions = Input_8_23.input_instructions;
        var nodesDict = Input_8_23.input_nodes
                                  .Split('\n')
                                  .ToDictionary(node => node.Substring(0, 3), 
                                                node => node);
       
        var steps = 0;
        string currentPosition = nodesDict["AAA"];

        while (currentPosition.Substring(0,3) != "ZZZ") {

            var nextInstruction = instructions[steps%instructions.Length];

            currentPosition = nextInstruction == 'L' 
                                ? nodesDict[currentPosition.Substring(7,3)] 
                                : nodesDict[currentPosition.Substring(12,3)];

            steps++;
        }

        Console.WriteLine($"Done: {steps}");
    }
}
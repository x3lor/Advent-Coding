using System.Diagnostics.CodeAnalysis;

public class Solution_8_1_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting...");

        var instructions = Input_8_23.input_instructions;
        
        // BXN = (SJH, XFB)
        var nodesDict = Input_8_23.input_nodes
                                  .Split('\n')
                                  .ToDictionary(node => node.Substring(0, 3), 
                                                node => node);
       
        var steps = 0;
        string currentPosition = nodesDict["AAA"];
        var reached = false;

        while (!reached) {

            var nextInstruction = instructions[steps%instructions.Length];

            if (nextInstruction == 'L') {
                currentPosition = nodesDict[currentPosition.Substring(7,3)];
            } else {
                currentPosition = nodesDict[currentPosition.Substring(12,3)];
            }

            steps++;

            if (currentPosition.Substring(0,3) == "ZZZ") {
                reached = true;
            }
        }

        Console.WriteLine($"Done: {steps}");
    }
}
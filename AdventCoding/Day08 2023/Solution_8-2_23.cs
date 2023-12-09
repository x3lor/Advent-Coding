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
       

        var nodes = new List<string> { "AAA", "GGA", "DXA", "LTA", "BJA", "XVA" };
        var stepsFirstList = new List<int>();
        var stepsCyclusList = new List<int>();

        foreach (var node in nodes) {

            var steps = 0;
            string currentPosition = nodesDict[node];
            var reached = false;
            var reached_two = false;

            Console.WriteLine($"\n\nNode: {currentPosition}");

            while (!reached_two) {

                var nextInstruction = instructions[steps%instructions.Length];

                if (nextInstruction == 'L') {
                    currentPosition = nodesDict[currentPosition.Substring(7,3)];
                } else {
                    currentPosition = nodesDict[currentPosition.Substring(12,3)];
                }

                steps++;

                if (currentPosition.Substring(2,1) == "Z") {

                    if (reached == false) {
                        reached = true;
                        stepsFirstList.Add(steps);
                        Console.WriteLine($"FirstReach: {steps} on {currentPosition}");
                    } else {
                        reached_two = true;
                        stepsCyclusList.Add(steps - stepsFirstList.Last());
                        Console.WriteLine($"Zyklus: {steps - stepsFirstList.Last()} on {currentPosition}");
                    }                    
                }
            }
        }
    }
}
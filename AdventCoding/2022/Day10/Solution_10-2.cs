public class Solution_10_2 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting ... ");

        var registerX = 1;
        var cycle = 1;

        var currentString = 0;
        var stringList = new List<string> { "","","","","","" };

        foreach(var line in Input_10.input.Split('\n')) {

            DrawNextPixel(cycle, registerX, stringList, currentString);

            if (cycle%40==0) {
                currentString++;
            }

            cycle++;
            
            var parts = line.Split(' ');
            if (parts[0] == "noop") {
                continue;
            }
        
            DrawNextPixel(cycle, registerX, stringList, currentString);

            if (cycle%40==0) {
                currentString++;
            }

            registerX += int.Parse(parts[1]);
            cycle++;
        }

        foreach(string s in stringList)
            Console.WriteLine(s);
    }    

    private void DrawNextPixel(int cycle, int registerX, List<string> stringList, int currentString) {
        
        var currentDrawPos = (cycle % 40)-1;
        var currentSpikePos = registerX;

        if (currentSpikePos == currentDrawPos     || 
            currentSpikePos == currentDrawPos + 1 ||
            currentSpikePos == currentDrawPos - 1) {

            stringList[currentString] += "#";
        } else {
            stringList[currentString] += " ";
        }
    }
}
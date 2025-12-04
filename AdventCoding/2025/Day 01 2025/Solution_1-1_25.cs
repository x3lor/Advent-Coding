public class Solution_1_1_25 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var currentNr = 50;
        var resultCounter = 0;

        

        foreach(var line in Input_1_25.input.Split('\n')) {

            var rotation = int.Parse(line.Substring(1));

            currentNr = line.StartsWith('L') 
                            ? (currentNr - rotation) % 100 
                            : (currentNr + rotation) % 100;

            if (currentNr == 0)
                resultCounter++;
        }

        Console.WriteLine($"done! Sum: {resultCounter}");
    }
}
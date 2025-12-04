public class Solution_1_2_25 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var currentNr = 50;
        var resultCounter = 0;

        foreach (var line in Input_1_25.input.Split('\n'))
        {

            var rotation = int.Parse(line.Substring(1));

            var before = currentNr;
            var after = line.StartsWith('L') ? currentNr - rotation : currentNr + rotation;

            var upgrade = 0;

            if (after == 0)
            {
                upgrade = 1;
            }
            else if (line.StartsWith('R'))
            {
                if (after > 99)
                {
                    upgrade = after / 100;
                }
            }
            else
            {
                if (after < 0)
                {
                    upgrade = ((-after) / 100) + 1;

                    if (before == 0)
                        upgrade--;
                }
            }

            resultCounter += upgrade;
            currentNr = ((after % 100) + 100) % 100;            
        }

        Console.WriteLine($"done! Sum: {resultCounter}");
    }
}
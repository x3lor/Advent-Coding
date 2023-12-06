public class Solution_1_1 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var max = 0;
        var sum = 0;

        foreach(var line in Input_1.input.Split('\n')) {
            
            if (string.IsNullOrWhiteSpace(line)) {
                if (sum > max) {
                    max = sum;
                }
                sum = 0;
            } else {
                sum += int.Parse(line);
            }
        }

        Console.WriteLine($"done! Max: {max}");
    }
}
public class Solution_1_1_24 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");
        var sum = 0;

        foreach(var line in Input_1_24.input.Split('\n')) {
            
            if (string.IsNullOrWhiteSpace(line))
                continue;

        
        }

        Console.WriteLine($"done! Sum: {sum}");
    }

    public bool isNumber(string s) {
        return s[0] >= '0' && s[0] <= '9';
    }
}
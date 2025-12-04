public class Solution_1_1_15 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");
        var result = Input_1_15.input.Count(c => c == '(') - Input_1_15.input.Count(c => c == ')');
        Console.WriteLine($"done! Sum: {result}");
    }
}
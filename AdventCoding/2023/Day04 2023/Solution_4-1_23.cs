public class Solution_4_1_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = Input_4_23.input
                            .Split('\n')
                            .Select(line => line.Substring(10, 29)
                                                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                .Select(part => int.Parse(part))
                                                .Intersect(line.Substring(42)
                                                               .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                               .Select(part => int.Parse(part)))
                                                .Count())
                            .Select(match => (int)Math.Pow(2, match-1))
                            .Sum();

        Console.WriteLine($"done! Sum: {sum}");
    }
}
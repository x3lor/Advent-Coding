public class Solution_4_1_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = 0;

        foreach(var line in Input_4_23.input.Split('\n')) {

                var firstNumbers = line.Substring(10, 29)
                                       .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(part => int.Parse(part))
                                       .ToList();

                var secondNumbers = line.Substring(42)
                                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                        .Select(part => int.Parse(part))
                                        .ToList();   

                var matches = firstNumbers.Intersect(secondNumbers).Count();   
            
                sum += (int)Math.Pow(2, matches-1);
        }

        Console.WriteLine($"done! Sum: {sum}");
    }
}
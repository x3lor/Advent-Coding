public class Solution_1_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var list = new List<int>();
        var sum = 0;

        foreach(var line in Input_1.input.Split('\n')) {
            
            if (string.IsNullOrWhiteSpace(line)) {
                list.Add(sum);
                sum = 0;
            } else {
                sum += int.Parse(line);
            }
        }

        var result = list.OrderByDescending(i => i)
                         .Take(3)
                         .Sum();

        Console.WriteLine($"done! Top 3 sum: {result}");
    }
}
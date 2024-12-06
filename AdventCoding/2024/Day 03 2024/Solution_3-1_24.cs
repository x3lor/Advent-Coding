using System.Text.RegularExpressions;

public class Solution_3_1_24 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        long sum = Regex.Matches(string.Join(' ', Input_3_24.input.Split('\n')), @"mul\((\d+),(\d+)\)")
                        .Select(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value))
                        .Sum();

        Console.WriteLine($"done! Sum: {sum}");
    }
}


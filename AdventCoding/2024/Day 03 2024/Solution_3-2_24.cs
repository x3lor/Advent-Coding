using System.Text.RegularExpressions;

public class Solution_3_2_24 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        long sum = 0;
        string input = string.Join(' ', Input_3_24.input.Split('\n'));

        var do_parts = input.Split("do()", StringSplitOptions.RemoveEmptyEntries).ToList();
        for (int i=0; i<do_parts.Count; i++) {
            var firstDont = do_parts[i].IndexOf("don't()");
            if (firstDont == -1) {
                continue;
            }
            do_parts[i] = do_parts[i].Substring(0, firstDont);
        }
        input = string.Join(' ', do_parts);

        foreach (Match match in Regex.Matches(input, @"mul\((\d+),(\d+)\)"))
        {
            sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        Console.WriteLine($"done! Sum: {sum}");
    }

   
}
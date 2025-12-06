
using System.Numerics;

public class Solution_6_1_25 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_6_25.input.Split('\n');
        var firstNumerLine  = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToList();
        var secondNumerLine = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToList();
        var thirdNumerLine  = input[2].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToList();
        var fourthNumerLine = input[3].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToList();
        var operators       = input[4].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
       
        var result = new BigInteger(0);

        for (int i=0; i<firstNumerLine.Count; i++)
        {
            if (operators[i] == "+")
            {
                result += firstNumerLine[i]+secondNumerLine[i]+thirdNumerLine[i]+fourthNumerLine[i];
            } 
            else
            {
                result += firstNumerLine[i]*secondNumerLine[i]*thirdNumerLine[i]*fourthNumerLine[i];
            }
        }

        Console.WriteLine($"done! Sum: {result}");
    }    
}
public class Solution_1_1_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");
        var sum = 0;

        foreach(var line in Input_1_23.input.Split('\n')) {
            
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var firstDigitPtr = 0;
            while (!isNumber(line.Substring(firstDigitPtr, 1)))
                firstDigitPtr++;

            var lastDigitPtr = line.Length-1;
            while (!isNumber(line.Substring(lastDigitPtr, 1)))
                lastDigitPtr--;

            var first  = int.Parse(line.Substring(firstDigitPtr, 1));
            var second = int.Parse(line.Substring(lastDigitPtr, 1));

            var num = first * 10 + second;
            sum += num;       
        }

        Console.WriteLine($"done! Sum: {sum}");
    }

    public bool isNumber(string s) {
        return s[0] >= '0' && s[0] <= '9';
    }
}
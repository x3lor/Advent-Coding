public class Solution_1_2_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = 0;

        foreach(var line in Input_1_23.input.Split('\n')) {
            
            var digitList = divide(line);
            var first  = digitList.First();
            var second = digitList.Last();
            var num = first * 10 + second;
            sum += num;                   
        }

        Console.WriteLine($"done! Sum: {sum}");
    }

    public static List<int> divide(string s) {

        var result = new List<int>();
        
        for (int stringPtr=0; stringPtr < s.Length; stringPtr++) {

            var currentDigit = s.Substring(stringPtr, 1);
            if (isNumber(currentDigit)) {
                result.Add(int.Parse(currentDigit));
                continue;
            }

            var restOfString = s.Length-stringPtr;

            if (restOfString >= 3 && s.Substring(stringPtr, 3) == "one"  ) { result.Add(1); continue; }
            if (restOfString >= 3 && s.Substring(stringPtr, 3) == "two"  ) { result.Add(2); continue; }
            if (restOfString >= 5 && s.Substring(stringPtr, 5) == "three") { result.Add(3); continue; }
            if (restOfString >= 4 && s.Substring(stringPtr, 4) == "four" ) { result.Add(4); continue; }
            if (restOfString >= 4 && s.Substring(stringPtr, 4) == "five" ) { result.Add(5); continue; }
            if (restOfString >= 3 && s.Substring(stringPtr, 3) == "six"  ) { result.Add(6); continue; }
            if (restOfString >= 5 && s.Substring(stringPtr, 5) == "seven") { result.Add(7); continue; }
            if (restOfString >= 5 && s.Substring(stringPtr, 5) == "eight") { result.Add(8); continue; }
            if (restOfString >= 4 && s.Substring(stringPtr, 4) == "nine" ) { result.Add(9); continue; }
        }
        
        return result;
    }

    public static bool isNumber(string s) {
        return s[0] >= '0' && s[0] <= '9';
    }
}
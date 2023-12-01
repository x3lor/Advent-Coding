public class Solution_1_2_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = 0;

        foreach(var line in Input_1_23.input.Split('\n')) {
            
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var digitList = divide(line);
            var first  = digitList.First();
            var second = digitList.Last();
            var num = first * 10 + second;
            sum += num;       

            //Console.WriteLine($"{line}; first: {first}; last: {second}");
        }

        Console.WriteLine($"done! Sum: {sum}");
    }

    public List<int> divide(string s) {

        var result = new List<int>();
        var stringPtr = 0;

        while (stringPtr < s.Length) {

            if (isNumber(s.Substring(stringPtr, 1))) {
                result.Add(int.Parse(s.Substring(stringPtr, 1)));
                stringPtr++;
                continue;
            }

            if (s.Length-stringPtr >= 3) { if (s.Substring(stringPtr, 3) == "one"  ) { result.Add(1); stringPtr += 1; continue; }}
            if (s.Length-stringPtr >= 3) { if (s.Substring(stringPtr, 3) == "two"  ) { result.Add(2); stringPtr += 1; continue; }}
            if (s.Length-stringPtr >= 5) { if (s.Substring(stringPtr, 5) == "three") { result.Add(3); stringPtr += 1; continue; }}
            if (s.Length-stringPtr >= 4) { if (s.Substring(stringPtr, 4) == "four" ) { result.Add(4); stringPtr += 1; continue; }}
            if (s.Length-stringPtr >= 4) { if (s.Substring(stringPtr, 4) == "five" ) { result.Add(5); stringPtr += 1; continue; }}
            if (s.Length-stringPtr >= 3) { if (s.Substring(stringPtr, 3) == "six"  ) { result.Add(6); stringPtr += 1; continue; }}
            if (s.Length-stringPtr >= 5) { if (s.Substring(stringPtr, 5) == "seven") { result.Add(7); stringPtr += 1; continue; }}
            if (s.Length-stringPtr >= 5) { if (s.Substring(stringPtr, 5) == "eight") { result.Add(8); stringPtr += 1; continue; }}
            if (s.Length-stringPtr >= 4) { if (s.Substring(stringPtr, 4) == "nine" ) { result.Add(9); stringPtr += 1; continue; }}

            stringPtr++;
        }
        
        return result;
    }

    public bool isNumber(string s) {
        return s[0] >= '0' && s[0] <= '9';
    }
}
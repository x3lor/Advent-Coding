public class Solution_3_1 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting");

        var input = Input_3.input.Split('\n');

        var sum = 0;

        for(int i=0; i<input.Length; i++) {
            
          var currentline = input[i];
          var (s1, s2) = SplitStringInHalf(currentline);
          var c = GetFirstCommonCharInTwoStrings(s1, s2);
          var number = CharToNumber(c);

          sum += number;
        }

        Console.WriteLine($"done! Sum: {sum}");
    }

    private (string, string) SplitStringInHalf(string s) {
        var halfLength = s.Length/2;
        return (s.Substring(0, halfLength), s.Substring(halfLength, halfLength));
    }

    private char GetFirstCommonCharInTwoStrings(string s1, string s2) {

        foreach (char c in s1) {
            if (s2.Contains(c))
                return c;
        }

        throw new Exception("sollte nicht passieren");
    }

    private int CharToNumber(char c) {

        if (c >= 'A' && c <= 'Z')
            return ((int)c)-38;
        else
            return ((int)c)-96;
    }
}
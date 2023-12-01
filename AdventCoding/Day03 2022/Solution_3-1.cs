public class Solution_3_1 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = 0;

        foreach(var line in Input_3.input.Split('\n')) {
                      
            var halfLength = line.Length/2;
            var c = GetFirstCommonCharInTwoStrings(line.Substring(0,          halfLength), 
                                                   line.Substring(halfLength, halfLength));
            sum += CharToNumber(c);
        }

        Console.WriteLine($"done! Sum: {sum}");
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
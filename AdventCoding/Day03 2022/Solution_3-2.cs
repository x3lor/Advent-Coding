public class Solution_3_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_3.input.Split('\n');
        var sum = 0;

        for(int i=0; i<input.Length; i += 3) {
                
            var c = GetFirstCommonCharInThreeStrings(input[i], input[i+1], input[i+2]);
            sum += CharToNumber(c);
        }

        Console.WriteLine($"done! Sum: {sum}");
    }    

    private char GetFirstCommonCharInThreeStrings(string s1, string s2, string s3) {

        foreach (char c in s1) {
            if (s2.Contains(c) & s3.Contains(c))
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
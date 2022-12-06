public class Solution_6_1 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting");

        var input = Input_6.input;

        for(int i=0; i<input.Length; i++) {
            
            var currentsegment = input.Substring(i, 4);
            if (OnlyUniqueCharaters (currentsegment))
            {
                Console.WriteLine($"Found at: i={i} .. result should be: {i+4}");
                break;
            }
        }

        Console.WriteLine($"end");
    }    

    private static bool OnlyUniqueCharaters(string s) {
        var charArray = s.ToCharArray().ToHashSet<char>();
        return charArray.Count == 4;
    }
}
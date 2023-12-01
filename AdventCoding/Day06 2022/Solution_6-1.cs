public class Solution_6_1 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_6.input;
        for(int i=0; i<input.Length-4; i++) {
            
            var currentsegment = input.Substring(i, 4);
            if (OnlyUniqueCharaters (currentsegment))
            {
                Console.WriteLine($"Done! result: {i+4}");
                break;
            }
        }
    }    

    private static bool OnlyUniqueCharaters(string s) {
        return s.Distinct().Count() == 4;
    }
}
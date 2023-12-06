public class Solution_6_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_6.input;
        for(int i=0; i<input.Length-14; i++) {
            
            var currentsegment = input.Substring(i, 14);
            if (OnlyUniqueCharaters (currentsegment))
            {
                Console.WriteLine($"Done! result: {i+14}");
                break;
            }
        }
    }    

    private static bool OnlyUniqueCharaters(string s) {
        return s.Distinct().Count() == 14;
    }
}
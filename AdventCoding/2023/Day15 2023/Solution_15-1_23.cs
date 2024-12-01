public class Solution_15_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");
        var sum=0;
        foreach (var part in Input_15_23.input.Split(',')) {
            var hash = 0;
            foreach(var c in part) {
                hash += c;
                hash *= 17;
                hash %= 256;
            }
            sum += hash;
        }
        Console.WriteLine($"Done! Sum: {sum}");
    }
}
public class Solution_1_2_24 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");
        
        var list1 = new List<int>();
        var list2 = new List<int>();

        foreach(var line in Input_1_24.input.Split('\n')) {
            
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            list1.Add(int.Parse(parts[0]));
            list2.Add(int.Parse(parts[1]));
        }

        long sum = 0;

        foreach(var num in list1) {

            sum += num * list2.Count(x => x == num);
        }

        Console.WriteLine($"done! Sum: {sum}");
    }
}
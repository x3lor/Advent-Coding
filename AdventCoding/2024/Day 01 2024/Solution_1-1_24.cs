public class Solution_1_1_24 : ISolution
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

        list1.Sort();
        list2.Sort();

        var sum = 0;

        for (int i=0; i<list1.Count; i++) {
            sum += Math.Abs(list1[i]-list2[i]);
        }

        Console.WriteLine($"done! Sum: {sum}");
    }
}
public class Solution_1_2 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting");

        var input = Input_1_1.input.Split('\n');

        var list = new List<int>(input.Length/4);
        var sum = 0;

        for(int i=0; i<input.Length; i++) {
            
            var currentline = input[i];
            
            if (string.IsNullOrWhiteSpace(currentline)) {
                list.Add(sum);
                sum = 0;
            } else {
                var inputAsNumber = int.Parse(currentline);
                sum += inputAsNumber;
            }
        }

        list.Sort();
        list.Reverse();

        var result = list.Take(3).Sum();

        Console.WriteLine($"done! Top 3 sum: {result}");
    }
}
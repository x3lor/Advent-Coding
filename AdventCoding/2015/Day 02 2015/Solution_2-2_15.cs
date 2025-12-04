public class Solution_2_2_15 : ISolution
{
     public void run()
    {
        Console.Write("Starting ... ");
        
        var result = 0L;

        foreach(var line in Input_2_15.input.Split('\n'))
        {
            var parts = line.Split('x');

            var s1 = int.Parse(parts[0]);
            var s2 = int.Parse(parts[1]);
            var s3 = int.Parse(parts[2]);

            var list = new List<int>(){s1,s2,s3};
            list.Sort();


            result += list[0]*2 + list[1]*2 + s1*s2*s3;
        }

        Console.WriteLine($"done! Sum: {result}");
    }
}
public class Solution_2_1_24 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = 0;
        
        foreach(var line in Input_2_24.input.Split('\n')) {
            
            var list = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                           .Select(int.Parse)
                           .ToList();

            var trueASC  = list.Zip(list.Skip(1), (current, next) => next - current <= 3 && next - current > 0).All(b => b);
            var trueDESC = list.Zip(list.Skip(1), (current, next) => current - next <= 3 && current - next > 0).All(b => b);
            
            if (trueASC || trueDESC)
                sum++;
        }

        Console.WriteLine($"done! Sum: {sum}");
    }
}
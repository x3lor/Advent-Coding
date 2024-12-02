public class Solution_2_2_24 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = 0;
        
        foreach(var line in Input_2_24.input.Split('\n')) {
            
            var list = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                           .Select(int.Parse)
                           .ToList();

            if (IsListSafe(list)) {
                sum++;
                continue;
            }

            for (int i=0; i<list.Count; i++) {
                var reducedList = list.ToList();
                reducedList.RemoveAt(i);
                if (IsListSafe(reducedList)) {
                    sum++;
                    break;
                }
            }
        }

        Console.WriteLine($"done! Sum: {sum}");
    }

    private bool IsListSafe(List<int> list) {
            var trueASC  = list.Zip(list.Skip(1), (current, next) => next - current <= 3 && next - current > 0).All(b => b);
            var trueDESC = list.Zip(list.Skip(1), (current, next) => current - next <= 3 && current - next > 0).All(b => b);
            
            return trueASC || trueDESC;
    }
}
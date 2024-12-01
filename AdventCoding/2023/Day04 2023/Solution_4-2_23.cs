public class Solution_4_2_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var matchList = Input_4_23.input
                                  .Split('\n')
                                  .Select(line => line.Substring(10, 29)
                                                      .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                      .Select(part => int.Parse(part))
                                                      .Intersect(line.Substring(42)
                                                                     .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                                     .Select(part => int.Parse(part)))
                                                      .Count())
                                  .ToList();

        var stack = new Stack<int>();
        for (int i=matchList.Count-1; i>=0; i--) {
            stack.Push(i);
        }

        var sum = 0;

        while (stack.Count > 0) {
            var gameNumber = stack.Pop();
            var matches = matchList[gameNumber];
            sum++;

            for (int i=gameNumber+1; i<gameNumber+1+matches; i++) {
                stack.Push(i);
            }
        }        

        Console.WriteLine($"done! Sum: {sum}");
    }
}
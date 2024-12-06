public class Solution_4_2_24 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_4_24.input.Split('\n').ToList();

        var sum = 0;

        for (int y=1; y<input.Count-1; y++) {
            for (int x=1; x<input[0].Length-1; x++) {
                if (input[y][x] == 'A')
                    if (TestA(input,x,y))
                        sum++;
            }
        }

        Console.WriteLine($"done! Sum: {sum}");
    }

    public bool TestA (List<string> input, int x, int y) {

        var fromLeftTop = (input[y-1][x-1] == 'M' && input[y+1][x+1] == 'S') ||
                          (input[y-1][x-1] == 'S' && input[y+1][x+1] == 'M');

        var fromRightTop = (input[y-1][x+1] == 'M' && input[y+1][x-1] == 'S') ||
                          (input[y-1][x+1] == 'S' && input[y+1][x-1] == 'M');

        return fromLeftTop && fromRightTop;
    }

}
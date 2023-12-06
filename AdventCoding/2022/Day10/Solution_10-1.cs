public class Solution_10_1 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = 0;
        var registerX = 1;
        var cycle = 1;

        foreach(var line in Input_10.input.Split('\n')) {
            
            if ((cycle-20)%40==0) {
                sum += registerX * (cycle);
            }
            
            cycle++;

            var parts = line.Split(' ');
            if (parts[0] == "noop") {
                continue;
            }
            
            if ((cycle-20)%40==0) {
                sum += registerX * (cycle);
            }

            registerX += int.Parse(parts[1]);
            cycle++;
        }

        Console.WriteLine($"done! Sum: {sum}");
    }    
}
public class Solution_1_1 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting");

        var input = Input_1.input.Split('\n');

        var max = 0;
        var sum = 0;

        for(int i=0; i<input.Length; i++) {
            
            var currentline = input[i];
            
            if (string.IsNullOrWhiteSpace(currentline)) {
                if (sum > max) {
                    max = sum;
                }
                sum = 0;
            } else {
                var inputAsNumber = int.Parse(currentline);
                sum += inputAsNumber;
            }
        }

        Console.WriteLine($"done! Max: {max}");
    }
}
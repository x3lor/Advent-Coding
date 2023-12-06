public class Solution_2_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = 0;
        foreach(var line in Input_2.input.Split('\n')) {
            
            switch (line) {
                case "A X": sum += 3; break; // -> Need Loss (0) + Scissors (3)
                case "A Y": sum += 4; break; // -> Need Draw (3) + Rock (1)
                case "A Z": sum += 8; break; // -> Need Win  (6) + Paper (2)
                case "B X": sum += 1; break; // -> Need Loss (0) + Rock (1)
                case "B Y": sum += 5; break; // -> Need Draw (3) + Paper (2)
                case "B Z": sum += 9; break; // -> Need Win  (6) + Scissors (3)
                case "C X": sum += 2; break; // -> Need Loss (0) + Paper (2)
                case "C Y": sum += 6; break; // -> Need Draw (3) + Scissors (3)
                case "C Z": sum += 7; break; // -> Need Win  (6) + Rock (1)
            }
        }

        Console.WriteLine($"done! Sum: {sum}");
    }
}
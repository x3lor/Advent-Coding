public class Solution_2_1 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = 0;
        foreach(var line in Input_2.input.Split('\n')) {
            
           switch (line) {
                case "A X": sum += 4; break; // DRAW(3) + Rock (1)
                case "A Y": sum += 8; break; // WIN (6) + Paper (2)
                case "A Z": sum += 3; break; // LOSS(0) + Scissors (3)
                case "B X": sum += 1; break; // LOSS(0) + Rock (1)
                case "B Y": sum += 5; break; // DRAW(3) + Paper (2)
                case "B Z": sum += 9; break; // WIN (6) + Scissors (3)
                case "C X": sum += 7; break; // WIN (6) + Rock (1)
                case "C Y": sum += 2; break; // LOSS(0) + Paper (2)
                case "C Z": sum += 6; break; // DRAW(3) + Scissors (3)
            }
        }

        Console.WriteLine($"done! Sum: {sum}");
    }
}
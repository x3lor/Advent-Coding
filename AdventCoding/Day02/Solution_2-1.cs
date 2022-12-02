public class Solution_2_1 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting");

        var input = Input_2.input.Split('\n');

        var sum = 0;

        for(int i=0; i<input.Length; i++) {
            
            var currentline = input[i];

            // +1 Rock
            // +2 Paper
            // +3 Scissors

            // +0 Loss
            // +3 Draw
            // +6 Win
            
            switch (currentline) {
                case "A X": sum += 4; break; // DRAW + Rock
                case "A Y": sum += 8; break; // WIN  + Paper
                case "A Z": sum += 3; break; // LOSS + Scissors
                case "B X": sum += 1; break; // LOSS + Rock
                case "B Y": sum += 5; break; // DRAW + Paper
                case "B Z": sum += 9; break; // WIN  + Scissors
                case "C X": sum += 7; break; // WIN  + Rock
                case "C Y": sum += 2; break; // LOSS + Paper
                case "C Z": sum += 6; break; // DRAW + Scissors
            }
        }

        Console.WriteLine($"done! Sum: {sum}");
    }
}
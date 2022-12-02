public class Solution_2_2 : ISolution
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

            // X -> need LOSS
            // Y -> need DRAW
            // Z -> need WIN

            switch (currentline) {
                case "A X": sum += 3; break; // Rock + Loss     -> Loss + Scissors 
                case "A Y": sum += 4; break; // Rock + Draw     -> Draw + Rock
                case "A Z": sum += 8; break; // Rock + Win      -> Win  + Paper
                case "B X": sum += 1; break; // Paper + Loss    -> Loss + Rock
                case "B Y": sum += 5; break; // Paper + Draw    -> Draw + Paper
                case "B Z": sum += 9; break; // Paper + Win     -> Win  + Scissors
                case "C X": sum += 2; break; // Scissors + Loss -> Loss + Paper
                case "C Y": sum += 6; break; // Scissors + Draw -> Draw + Scissors
                case "C Z": sum += 7; break; // Scissors + Win  -> Win  + Rock
            }
        }

        Console.WriteLine($"done! Sum: {sum}");
    }
}
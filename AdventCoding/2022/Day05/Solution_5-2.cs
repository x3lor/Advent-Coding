using System.Text;

public class Solution_5_2 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting");
        var stacks = new Stacks(Input_5.start);

        foreach(var move in Input_5.moves.Split('\n')) {
        
            // Example: move 12 from 3 to 5
            var parts = move.Split(' ');

            var quantity = int.Parse(parts[1]);
            var from     = int.Parse(parts[3]) - 1;
            var to       = int.Parse(parts[5]) - 1;        
    
            stacks.ApplyMove(quantity, from, to);
        }

        //stacks.PrintState();
        stacks.PrintTopOfStacks();       
    }    

    private class Stacks {
        private List<string> stacks;

        public Stacks(string initial) {
            stacks = initial.Split('\n').ToList();
        }

        public void ApplyMove(int quantity, int from, int to) {
            var toMove = stacks[from].Substring(stacks[from].Length-quantity, quantity);
            stacks[from] = stacks[from].Substring(0, stacks[from].Length-quantity);
            stacks[to] = stacks[to]+toMove;
        }

        public void PrintState() {
            Console.WriteLine("CurrentState:");
            foreach (var s in stacks) {
                if (string.IsNullOrWhiteSpace(s))
                    Console.WriteLine("--empty--");
                else
                    Console.WriteLine(s);
            }
            Console.WriteLine();
        }

        private static string LastCharacterOf(string s) {
            return s.Substring(s.Length-1, 1);
        }

        public void PrintTopOfStacks() {
            Console.WriteLine($"Top of Stacks: {string.Join("", stacks.Select(s => LastCharacterOf(s)))}");
        } 
    }
}
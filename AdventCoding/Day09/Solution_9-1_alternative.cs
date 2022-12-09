public class Solution_9_1_alternative : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var allTailPositions = new List<Position>();

        var head = new Position();
        var tail = new Position();

        foreach(var line in Input_9.input.Split('\n')) {
            
            var parts = line.Split(' ');
            var moves = int.Parse(parts[1]);

            for (int i=0; i<moves; i++) {

                switch(parts[0]) {
                    case "L": { head.X--; break; }
                    case "U": { head.Y--; break; }
                    case "D": { head.Y++; break; }
                    case "R": { head.X++; break; }
                }

                if (Math.Abs(head.X-tail.X) < 2 && Math.Abs(head.Y-tail.Y) < 2) {
                    continue;
                }

                tail.X = tail.X + (((head.X-tail.X)==0) ? 0 : (head.X-tail.X)/Math.Abs((head.X-tail.X)));
                tail.Y = tail.Y + (((head.Y-tail.Y)==0) ? 0 : (head.Y-tail.Y)/Math.Abs((head.Y-tail.Y)));
                
                if (!allTailPositions.Any(p => p.X == tail.X && p.Y == tail.Y))
                    allTailPositions.Add(tail.Copy());
            }
        }

        Console.WriteLine($"done! Distinct Positions: {allTailPositions.Count}");
    }

    public class Position {

        public Position Copy() {
            return new Position {X=X, Y=Y};
        }

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0; 
    }  
}
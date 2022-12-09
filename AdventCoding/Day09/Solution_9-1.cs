public class Solution_9_1 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var allTailPositions = new List<Position>();

        var pHead = new Position();
        var pTail = new Position();

        foreach(var line in Input_9.input.Split('\n')) {
            
            var parts = line.Split(' ');
            var moves = int.Parse(parts[1]);

            for (int i=0; i<moves; i++) {

                switch(parts[0]) {
                    case "L": { pHead.X--; break; }
                    case "U": { pHead.Y--; break; }
                    case "D": { pHead.Y++; break; }
                    case "R": { pHead.X++; break; }
                }

                AdjustTail(pHead, pTail);

                if (!allTailPositions.Any(p => p.X == pTail.X && p.Y == pTail.Y))
                    allTailPositions.Add(pTail.Copy());
            }
        }

        Console.WriteLine($"done! Distinct Positions: {allTailPositions.Count}");
    }

    private void AdjustTail(Position head, Position tail) {

        if (Math.Abs(head.X-tail.X) < 2 && Math.Abs(head.Y-tail.Y) < 2) {
            return;
        }

        if (AreHeadAndTailInTheSameColumnOrRow(head, tail)) {
           if (head.X < tail.X) { tail.X--; return; }
           if (head.X > tail.X) { tail.X++; return; }
           if (head.Y < tail.Y) { tail.Y--; return; }
           if (head.Y > tail.Y) { tail.Y++; return; }
        } else {
            if (head.X < tail.X ) { tail.X--; } else { tail.X++; }
            if (head.Y < tail.Y ) { tail.Y--; } else { tail.Y++; }
        }
    }

    private bool AreHeadAndTailInTheSameColumnOrRow(Position head, Position tail) {
        return (head.X == tail.X) || (head.Y == tail.Y);
    }

    public class Position {

        public Position Copy() {
            return new Position {X=X, Y=Y};
        }

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0; 
    }    
}
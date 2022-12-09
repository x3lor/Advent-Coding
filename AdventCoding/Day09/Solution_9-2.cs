public class Solution_9_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var allTailPositions = new List<Position>();

        var rope = new List<Position>();
        for (var j=0; j<10; j++) {
            rope.Add(new Position());
        }

        foreach(var line in Input_9.input.Split('\n')) {
            
            var parts = line.Split(' ');
            var moves = int.Parse(parts[1]);

            for (int i=0; i<moves; i++) {

                switch(parts[0]) {
                    case "L": { rope[0].X--; break; }
                    case "U": { rope[0].Y--; break; }
                    case "D": { rope[0].Y++; break; }
                    case "R": { rope[0].X++; break; }
                }

                for (var j=0; j<9; j++) {
                    AdjustTail(rope[j], rope[j+1]);
                }

                if (!allTailPositions.Any(p => p.X == rope[9].X && p.Y == rope[9].Y))
                    allTailPositions.Add(rope[9].Copy());
            }
        }

        Console.WriteLine($"done! Distinct Positions: {allTailPositions.Count}");
    }

    private void AdjustTail(Position head, Position tail) {

        if (Math.Abs(head.X-tail.X) < 2 && Math.Abs(head.Y-tail.Y) < 2) {
            return;
        }

        if (AreHeadAndTailInTheSameColumn(head, tail)) {
           if (head.X < tail.X) { tail.X--; return; }
           if (head.X > tail.X) { tail.X++; return; }
           if (head.Y < tail.Y) { tail.Y--; return; }
           if (head.Y > tail.Y) { tail.Y++; return; }
        } else {
            if (head.X < tail.X && head.Y < tail.Y) { tail.X--; tail.Y--; return; }
            if (head.X > tail.X && head.Y < tail.Y) { tail.X++; tail.Y--; return; }
            if (head.X < tail.X && head.Y > tail.Y) { tail.X--; tail.Y++; return; }
            if (head.X > tail.X && head.Y > tail.Y) { tail.X++; tail.Y++; return; }
        }
    }

    private bool AreHeadAndTailInTheSameColumn(Position head, Position tail) {
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
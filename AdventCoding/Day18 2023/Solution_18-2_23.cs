public class Solution_18_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var instructions = Input_18_23.example
                                      .Split('\n')
                                      .Select(line => new Instruction(line))
                                      .ToList();

        var points = new List<Point>();

        var currentY = 0;
        var currentX = 0;

        foreach (var instruction in instructions) {

            points.Add(new Point {X=currentX, Y=currentY});

            switch (instruction.Direction) {
                case Direction.Up:    currentY -= instruction.Distance; break;
                case Direction.Down:  currentY += instruction.Distance; break;
                case Direction.Left:  currentX -= instruction.Distance; break;
                case Direction.Right: currentX += instruction.Distance; break;
            }
        }

        points.Add(new Point {X=currentX, Y=currentY});


        var edges = new List<Edge>();
        for (int i=0; i<points.Count-1; i++) {
            edges.Add(new Edge(points[i], points[i+1]));
        }
        
        var ymin = points.OrderBy(p => p.Y).First().Y;
        var yMax = points.OrderBy(p => p.Y).Last().Y;

        for (long y=ymin; y<=yMax; y++) {

            // calculate how much to move farward (because it's all identical)
            // and add the sum accordingly

        }
        

        // var sum2 = 0L;

        // for (int y=0; y<gridHeight; y++) {

        //     var blockList = GetBlocks(grid[y], grid, y);
        //     blockList.Reverse();
        //     var blockStack = new Stack<Block>(blockList);

        //     while (blockStack.Count > 0) {

        //         var item = blockStack.Pop();

        //         if (item.Changer == false) {
        //             sum2 += item.Length;
        //             continue;
        //         }

        //         var nextItem = blockStack.Pop();                

        //         while (nextItem.Changer == false) {                     
        //              nextItem = blockStack.Pop();
        //         }

                

        //         sum2 += nextItem.Start+nextItem.Length-item.Start;
        //     }
        // } 
       
        Console.WriteLine($"Done! Sum: ");
    }

    
    public enum Direction {
        Up, Down, Right, Left
    }

    public class Block {
        public int Start { get; set; }
        public int Length { get; set; }
        public bool Changer { get; set; }
    }

    public List<Block> GetBlocks(string line, string[] grid, int lineNr) {
        
        var resultList = new List<Block>();

        var currentStart = -1;
        var onEdge = false;

        for (int i=0; i<line.Length; i++) {

            if (onEdge == false && line[i] == '#') {
                currentStart = i;
                onEdge = true;
                continue;
            }

            if (onEdge == true && line[i] == '#') {
                continue;
            }

            if (line[i]=='.' && onEdge == true) {
                resultList.Add(new Block() {Start = currentStart, Length = i-currentStart});
                onEdge = false;
            }

            if (line[i]=='.' && onEdge == false) {
                continue;
            }
        }

        foreach (var block in resultList) {
            if (block.Length == 1) {
                block.Changer = true;
            } else {
                var leftUp  = grid[lineNr-1][block.Start] == '#';
                var rightUp = grid[lineNr-1][block.Start+block.Length-1] == '#';
                block.Changer = leftUp != rightUp;
            }
        }

        return resultList;
    }

    public class Point {
        public long X { get; set; }
        public long Y { get; set; }

        public override string ToString()
        {
            return $"({X}|{Y})";
        }
    }

    public class Edge {

        public Edge(Point from, Point to) {
            From = from;
            To = to;

            Horizontal = from.Y == to.Y;
        }

        public bool Horizontal { get; }

        public Point From { get; }
        public Point To { get; }
    }

    public class Instruction {

        public Instruction(string input) {
            var parts = input.Split(' ');

            var colorCode = parts[2][2..^1];
            var hexDistance = colorCode[..^1];
            Direction = colorCode[^1] switch {
                '0' => Direction.Right,
                '1' => Direction.Down,
                '2' => Direction.Left,
                _ => Direction.Up
            };

            Distance = Convert.ToInt32(hexDistance, 16);

            // Direction = parts[0] switch {
            //     "U" => Direction.Up,
            //     "R" => Direction.Right,
            //     "L" => Direction.Left,
            //     _ => Direction.Down
            // };

            // Distance = int.Parse(parts[1]);
        }

        public Direction Direction { get; }
        public int Distance { get; }

        public override string ToString()
        {
            return $"[{Direction}|->{Distance}]";
        }
    }
}
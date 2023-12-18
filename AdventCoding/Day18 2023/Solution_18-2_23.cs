using System.Numerics;

public class Solution_18_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///                                 Read instructions                                       ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        var instructions = Input_18_23.input
                                      .Split('\n')
                                      .Select(line => new Instruction(line))
                                      .ToList();

        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///                            Create all Points in space                                   ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

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

        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///                          Create Edges between those points                              ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        var edges = new List<Edge>();
        for (int i=0; i<points.Count-1; i++) {
            edges.Add(new Edge(points[i], points[i+1]));
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///         Determine for Horzontal Edges where they change "inner-outer" or not            ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        for(int i=0; i<edges.Count; i++) {

            var edge = edges[i];

            if (edge.Vertical)
                continue;

            var nextLeftPoint = points[(i-1+edges.Count)%points.Count];
            var nextRightPoint = points[(i+2)%points.Count];

            if (edge.From.Y == nextLeftPoint.Y || edge.To.Y == nextRightPoint.Y)
                throw new Exception("should not happen");

            var leftGoesUp  = edge.From.Y < nextLeftPoint.Y;
            var rightGoesUp = edge.To.Y   < nextRightPoint.Y;

            edge.ChangingInnerOuter = leftGoesUp != rightGoesUp;
        }
        
        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///             Now start line by line                                                      ///
        ///             if at the current line is any horizontal                                    ///
        ///                 -> just compute the inner parts of this line                            ///
        ///             if there is only vertival                                                   ///
        ///                 -> compute once an look for how many lines it's NOT changing            ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        var ymin = points.OrderBy(p => p.Y).First().Y;
        var yMax = points.OrderBy(p => p.Y).Last().Y;

        var overAllSum = new BigInteger(0);

        for (long y=ymin; y<=yMax; y++) {

            var edgesOnThisLine = edges.Where(e => (e.From.Y < y && e.To.Y > y) || (e.From.Y > y && e.To.Y < y) || (e.From.Y == y && e.To.Y == y))
                                       .OrderBy(e => e.From.X)
                                       .ToList();

            var sum = 0L;
            var blockList = GetBlocks(y,edgesOnThisLine);
            blockList.Reverse();
            var blockStack = new Stack<Block>(blockList);

            while (blockStack.Count > 0) {

                var item = blockStack.Pop();

                if (item.Changer == false) {
                    sum += item.Length;
                    continue;
                }

                var nextItem = blockStack.Pop();                

                while (nextItem.Changer == false) {                     
                    nextItem = blockStack.Pop();
                }                    

                sum += nextItem.Start+nextItem.Length-item.Start;
            }

            if (edgesOnThisLine.Any(e => e.Horizontal)) {            
                overAllSum += sum;
            } else {
                var yWhereNextPointIs = points.OrderBy(p => p.Y).Where(p => p.Y >= y).First().Y-1;            
                overAllSum += sum * new BigInteger(yWhereNextPointIs-y+1);
                y += yWhereNextPointIs-y;
            }                                           
        }
        
        Console.WriteLine($"Done! Sum: {overAllSum}");
    }

    
    public enum Direction {
        Up, Down, Right, Left
    }

    public class Block {
        public long Start { get; set; }
        public long Length { get; set; }
        public bool Changer { get; set; }
    }

    public List<Block> GetBlocks(long line, List<Edge> edges) {
        
        var resultList = new List<Block>();

        foreach(var edge in edges) {

            if (edge.Horizontal) {
                resultList.Add(new Block() {
                    Start = Math.Min(edge.From.X, edge.To.X),
                    Length = Math.Abs(edge.From.X - edge.To.X)+1,
                    Changer = edge.ChangingInnerOuter
                });
            } else {
                resultList.Add(new Block() {
                    Start = edge.From.X,
                    Length = 1,
                    Changer = true
                });
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
        public bool Vertical => !Horizontal;
        public bool ChangingInnerOuter { get; set; }

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
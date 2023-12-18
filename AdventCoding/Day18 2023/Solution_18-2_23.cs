using System.Numerics;

public class Solution_18_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");
        var startTime = DateTime.Now;

        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///                                 Read instructions                                       ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        var instructions = Input_18_23.input
                                      .Split('\n')
                                      .Select(line => new Instruction(line));                                      

        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///                            Create all Points in space                                   ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        var points = new List<Point>();
        var currentY = 0;
        var currentX = 0;
        foreach (var instruction in instructions) {
            points.Add(new Point(currentX, currentY));
            switch (instruction.Direction) {
                case Direction.Up:    currentY -= instruction.Distance; break;
                case Direction.Down:  currentY += instruction.Distance; break;
                case Direction.Left:  currentX -= instruction.Distance; break;
                case Direction.Right: currentX += instruction.Distance; break;
            }
        }
        points.Add(new Point(currentX, currentY));

        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///                          Create Edges between those points                              ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        var edges = new List<Edge>();
        for (int i=0; i<points.Count-1; i++) {
            edges.Add(new Edge(points[i], points[i+1]));
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///         Determine for Horzontal Edges wether they change "inner-outer" or not           ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        for(int i=0; i<edges.Count; i++) {

            var edge = edges[i];

            if (edge.Vertical)
                continue;

            var nextLeftPoint = points[(i-1+edges.Count)%points.Count];
            var nextRightPoint = points[(i+2)%points.Count];

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

        var overAllSum = new BigInteger(0);

        var ymin = points.OrderBy(p => p.Y).First().Y;
        var yMax = points.OrderBy(p => p.Y).Last().Y;

        for (long y=ymin; y<=yMax; y++) {

            var edgesOnThisLine = edges.Where(e => (e.From.Y <  y && e.To.Y >  y) || 
                                                   (e.From.Y >  y && e.To.Y <  y) || 
                                                   (e.From.Y == y && e.To.Y == y))
                                       .OrderByDescending(e => e.From.X)
                                       .ToList();

            var sum = 0L;
            var blockStack = GetBlocks(edgesOnThisLine);
            while (blockStack.Count > 0) {
                var item = blockStack.Pop();

                if (item.Changer == false) {
                    sum += item.Length;
                } else {
                    var nextItem = blockStack.Pop();                
                    while (nextItem.Changer == false) {                     
                        nextItem = blockStack.Pop();
                    }                    
                    sum += nextItem.Start+nextItem.Length-item.Start;
                }
            }

            if (edgesOnThisLine.Any(e => e.Horizontal)) {            
                overAllSum += sum;
            } else {
                var yWhereNextPointIs = points.OrderBy(p => p.Y).Where(p => p.Y >= y).First().Y;            
                overAllSum += sum * new BigInteger(yWhereNextPointIs-y);
                y += yWhereNextPointIs-y;
            }                                           
        }

        var endTime = DateTime.Now;
        Console.WriteLine($"Done (within {(endTime-startTime).TotalMilliseconds})! Sum: {overAllSum}");
    }

    
    public enum Direction {
        Up, Down, Right, Left
    }

    public class Block {
        public Block(long start, long length, bool changer) {
            Start = start;
            Length = length;
            Changer = changer;
        }

        public long Start { get; }
        public long Length { get; }
        public bool Changer { get; }
    }

    public static Stack<Block> GetBlocks(List<Edge> edges) {
        
        var resultList = new Stack<Block>();
        foreach(var edge in edges) {

            if (edge.Horizontal) {
                resultList.Push(new Block(Math.Min(edge.From.X, edge.To.X),
                                         Math.Abs(edge.From.X - edge.To.X)+1,
                                         edge.ChangingInnerOuter));
            } else {
                resultList.Push(new Block(edge.From.X, 1, true));
            }
        }
        return resultList;
    }

    public class Point {

        public Point(long x, long y) {
            X=x;
            Y=y;
        }

        public long X { get; }
        public long Y { get; }
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
            Direction = colorCode[^1] switch
            {
                '0' => Direction.Right,
                '1' => Direction.Down,
                '2' => Direction.Left,
                '3' => Direction.Up,
                _ => throw new NotImplementedException()
            };

            Distance = Convert.ToInt32(hexDistance, 16);
        }

        public Direction Direction { get; }
        public int Distance { get; }
    }
}
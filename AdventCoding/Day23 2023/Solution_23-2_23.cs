#pragma warning disable CS8602

using System.Drawing;

public class Solution_23_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        char[][] maze = Input_23_23.input
                                   .Split('\n')
                                   .Select(line => line.ToCharArray())
                                   .ToArray();

        int mazeHeight = maze.Length;
        int mazeWidth = maze[0].Length;

        var graph = new List<Node>();

        for (int y=0; y<mazeHeight; y++) {
            for (int x=0; x<mazeWidth; x++) {
                var point = new Point(x,y);
                if (CanStepOn(maze, point)) {
                    var nextMoves = GetNextPossibleMoves(maze, point);
                    if (nextMoves.Count == 1) {
                        if (point == new Point(1,0)) {
                            graph.Add(new Node(point, NodeType.Start));
                        } else if (point == new Point(mazeWidth-2, mazeHeight-1)) {
                            graph.Add(new Node(point, NodeType.Target));
                        } 
                    } else if (nextMoves.Count > 2) {
                        graph.Add(new Node(point, NodeType.Crossing));
                    }
                }
            }
        }

        foreach(var node in graph) {
            AddNodeConnectionsAndDistances(maze, node, graph);
        }

        var pathStack = new Stack<List<Node>>();
        var start = graph.First(n => n.Type == NodeType.Start);
        pathStack.Push(new List<Node>{start, start.Connections[0]});

        int maxDistance = 0;
        var maxPath = new List<Node>();

        while (pathStack.Count > 0) {
            
            var currentPath = pathStack.Pop();
            var lastNode = currentPath.Last();

            if (lastNode.Type == NodeType.Target) {
                var dist = GetDistance(currentPath);
                if (dist > maxDistance) {
                    maxDistance = dist;
                    maxPath = currentPath;
                }
            } else {
                foreach (var connection in lastNode.Connections) {
                    if (currentPath.Contains(connection))
                        continue;
                    else
                        pathStack.Push(currentPath.ToList().Append(connection).ToList());
                }
            }
        }

        Console.WriteLine($"Done! {maxDistance-1}");
    }

    private static int GetDistance(List<Node> path) {

        var sum = 0;
        for (int i=0; i<path.Count-1; i++) {
            var nodeFrom = path[i];
            var nodeTo = path[i+1];
            sum += nodeFrom.DistanceInfos.First(d => d.From==nodeFrom && d.To==nodeTo).DistanceBetween;
        }
        return sum+path.Count;
    }

    private static void AddNodeConnectionsAndDistances(char[][] maze, Node n, List<Node> graph) {
        
        var nextMoves = GetNextPossibleMoves(maze, n.Point);

        foreach(var p in nextMoves) {

            var last = n.Point;
            var current = p;
            var nodesBetween = 0;
            while (!graph.Any(n => n.Point == current)) {
                var next = GetNextMove(maze, current, last);
                last = current;
                current = next;
                nodesBetween++;
            }
            var foundNode = graph.First(n => n.Point == current);
            n.Connections.Add(foundNode);
            n.DistanceInfos.Add(new DistanceInfo(n, foundNode, nodesBetween));
        }
    }

    private static List<Point> GetNextPossibleMoves(char[][] maze, Point point) {

        return new List<Point>() {
            new(point.X,   point.Y+1),
            new(point.X,   point.Y-1),
            new(point.X-1, point.Y),
            new(point.X+1, point.Y)
        }.Where(p => CanStepOn(maze, p)).ToList();
    }

    private static Point GetNextMove(char[][] maze, Point point, Point without) {

        return new List<Point>() {
            new(point.X,   point.Y+1),
            new(point.X,   point.Y-1),
            new(point.X-1, point.Y),
            new(point.X+1, point.Y)
        }.First(p => CanStepOn(maze, p) && p != without);
    }

    private static bool CanStepOn(char[][] maze, Point p) {
        
        int mazeHeight = maze.Length;
        int mazeWidth = maze[0].Length;

        if (p.Y < 0 || p.Y >= mazeHeight || p.X < 0 || p.X >= mazeWidth) 
            return false;
        
        var c = maze[p.Y][p.X];
        return c != '#' && c != 'O';
    }

    public enum NodeType {
        Start, Target, Crossing
    }

    public class Node {

        public Node(Point p, NodeType nodeType) {
            Point = p;
            Connections = new List<Node>();
            DistanceInfos = new List<DistanceInfo>();
            Type = nodeType;
        }

        public Point Point { get; }
        public List<Node> Connections { get; }
        public List<DistanceInfo> DistanceInfos { get; }
        public NodeType Type;

        public override string ToString()
        {
            return $"{Point} [{Type}]";
        }
    }

    public class DistanceInfo {

        public DistanceInfo(Node from, Node to, int distanceBetween) {
            From = from;
            To = to;
            DistanceBetween = distanceBetween;
        }

        public Node From { get; }
        public Node To { get; }
        public int DistanceBetween { get; }
    }
}
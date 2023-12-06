using System.Text;

public class Solution_16_1 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting ... ");

        var input = Input_16.input;

        var nodes = new List<Node>();
        var index = 0;

        // create the nodes
        foreach(var line in input.Split('\n')) {
            
            // Example: Valve JX has flow rate=22; tunnels lead to valves CI, ZH, UR
            var parts = line.Split(' ');
            var id = parts[1];
            var flow = parts[4].Substring(5, parts[4].Length-6);
            nodes.Add(new Node(id, int.Parse(flow), index++));
        }

        //link the nodes
        foreach(var line in input.Split('\n')) {
            
            // Example: Valve JX has flow rate=22; tunnels lead to valves CI, ZH, UR
            var parts = line.Split(' ');
            var id = parts[1];

            var node = GetNodeById(nodes, id);

            for (int i=9; i<parts.Length; i++) {
                if (parts[i].Length == 2) {
                    node.Links.Add(GetNodeById(nodes, parts[i]));
                } else
                    node.Links.Add(GetNodeById(nodes, parts[i].Substring(0,2)));
            }
        }

        // precompute distances
        var numberOfNodes = nodes.Count;
        var distanceMatrix = new int[numberOfNodes, numberOfNodes];

        for (var from=0; from<numberOfNodes; from++) {
            for (var to=0; to<numberOfNodes; to++) {
                distanceMatrix[from, to] = GetShortestPath(nodes, nodes[from], nodes[to]).Count-1;
            }
        }

        var startNode = GetNodeById(nodes, "AA");
        var flowNodes = new List<Node> {
            GetNodeById(nodes, "JX"),
            GetNodeById(nodes, "IK"),
            //GetNodeById(nodes, "WH"),
            GetNodeById(nodes, "CF"),
            GetNodeById(nodes, "QQ"),
            GetNodeById(nodes, "VK"),
            //GetNodeById(nodes, "WL"),
            GetNodeById(nodes, "DS"),
            GetNodeById(nodes, "WM"),
            GetNodeById(nodes, "EZ"),
            GetNodeById(nodes, "LB"),
            GetNodeById(nodes, "BV"),
            //GetNodeById(nodes, "HG"),
            GetNodeById(nodes, "UY"),
            //GetNodeById(nodes, "IT")
        };
        var flowNodeCount = flowNodes.Count;
        var result = new Result(flowNodeCount);
        Permute(flowNodes, 0, flowNodeCount-1, result, startNode, distanceMatrix); 

        Console.WriteLine($"done! Result: {result.Max}");
    }    

    private string NodeToString(Node n) {
        if (n.Flow > 0) {
            return $"{n.Id}-{n.Flow}";
        } else {
            return n.Id;
        }
    }

    private class Result {

        private long iteration;
        private long neededIterations;
        private long current = 0;

        public Result(int n) {

            iteration = fac(n);
            neededIterations = iteration/1000000;
        }

        private long fac(int num) {
            long n = num;
            while (num > 0) {
                n = num;
                for (int i = (int)(n - 1); i > 0; i--)
                {
                    n *= i;
                }
                return n;
            }

            throw new ArgumentException();
        }

        public int Max { get; set; } = 0;

        public void Increase() {
            current++;

            if (current % 1000000 == 0) {
                Console.WriteLine($"{current/1000000}/{neededIterations}");
            }
        }
    }

    private static void Permute(List<Node> flowNodes, int l, int r, Result result, Node start, int[,] distanceMatrix) 
    { 
        if (l == r) {            
            var newVal = EvaluateSequence(flowNodes, start, distanceMatrix);
            result.Increase();
            if (newVal > result.Max)
                result.Max = newVal;
        } else { 
            for (int i = l; i <= r; i++) 
            { 
                Swap(flowNodes, l, i); 
                Permute(flowNodes, l + 1, r, result, start, distanceMatrix); 
                Swap(flowNodes, l, i); 
            } 
        } 
    } 

    private static int EvaluateSequence(List<Node> flownodes, Node start, int[,] distanceMatrix) {

        var currentNode = start;
        var timer = 0;
        var summedFlow = 0;
        var currentFlow = 0;

        for (var nodeIndex=0; nodeIndex<flownodes.Count; nodeIndex++) {
            var nextNode = flownodes[nodeIndex];
            var stepsToNextNode = distanceMatrix[currentNode.Index, nextNode.Index];

            for (int i=0; i<stepsToNextNode; i++) {
                timer++;
                if (timer > 30)
                    break;

                summedFlow += currentFlow;
            }

            timer++; 
            if (timer > 30)
                break;

            summedFlow += currentFlow;
            currentFlow += nextNode.Flow;
            currentNode = nextNode;
        }

        if (timer < 30) {
            summedFlow += currentFlow * (30-timer);
        }
        return summedFlow;
    }
  
    private static void Swap(List<Node> flowNodes, int i, int j) 
    { 
       var tmp = flowNodes[i];
       flowNodes[i] = flowNodes [j];
       flowNodes[j] = tmp;
    } 

    private List<Node> GetShortestPath(List<Node> allNodes, Node from, Node to) {

        // init search
        foreach (var n in allNodes) {
            n.StepsToGetHere = -1;
        }

        from.StepsToGetHere = 0;
        var shortestPath = from.SearchAndSet(new List<Node>() {from}, to);

        if (shortestPath == null)
            return new List<Node>();
        else
            return shortestPath;
    }

    private static Node GetNodeById(List<Node> nodes, string id) {
        return nodes.First(n => n.Id == id);
    }

    private class Node {

        public Node(string id, int flow, int index) {
            Index = index;
            Id = id;
            Flow = flow;
            Links = new List<Node>();
        }

        public int Index { get; }
        public string Id { get; }
        public int Flow { get; }

        public List<Node> Links { get; }

        public override string ToString()
        {
            return $"{Id} (Flow: {Flow}); Links: {string.Join(',', Links.Select(n => n.Id))}";
        }

        public int StepsToGetHere { get; set; }
        public List<Node>? SearchAndSet(List<Node>? resultList, Node target) {

            if (this == target) {
                return resultList;
            }
            
            var resultLists = new List<List<Node>?>();
            foreach(var node in Links) {

                if ((node.StepsToGetHere == -1) || (node.StepsToGetHere > StepsToGetHere+1)) {
                    node.StepsToGetHere = StepsToGetHere+1;
                    var newList = resultList?.ToList();
                    newList?.Add(node);
                    resultLists.Add(node.SearchAndSet(newList, target));
                } 
            }

            if (resultLists.Count == 0) {
                return null;
            }

            return resultLists.Where(l => l != null)
                              .OrderBy(l => l?.Count)
                              .FirstOrDefault();
        }
    }
}
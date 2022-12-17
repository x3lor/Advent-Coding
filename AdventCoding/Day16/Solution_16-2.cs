using System.Text;

public class Solution_16_2 : ISolution
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
        
        var myFlowNodes = new List<Node> {            
            GetNodeById(nodes, "WL"),  //
            GetNodeById(nodes, "WH"),  //                     
            GetNodeById(nodes, "HG"),  //         
            GetNodeById(nodes, "IT")   //
        };

         
        var toChooseFrom = new Node[] {
            GetNodeById(nodes, "DS"),
            GetNodeById(nodes, "QQ"),
            GetNodeById(nodes, "VK"),
            GetNodeById(nodes, "JX"),
            GetNodeById(nodes, "IK"),
            GetNodeById(nodes, "CF"),
            GetNodeById(nodes, "WM"),
            GetNodeById(nodes, "EZ"),
            GetNodeById(nodes, "LB"),
            GetNodeById(nodes, "BV"),
            GetNodeById(nodes, "UY"),
        }; // 11

        int max = 0;

        for (int elephantItems=6; elephantItems<=9; elephantItems++) {

            Console.WriteLine(elephantItems);

            var items = Combinations.CombinationsRosettaWoRecursion(toChooseFrom, elephantItems);

            foreach (var comb in items) {

                var elephantNodes = comb;
                var myNodes = myFlowNodes.ToList();
                myNodes.AddRange(toChooseFrom.Where(n => !elephantNodes.Contains(n)));

                var best = Get2Result(myNodes, elephantNodes.ToList(), startNode, distanceMatrix);
                if (best > max)
                    max = best;
            }

        }

        Console.WriteLine($"done! Result: {max}");
    }    

    private int Get2Result(List<Node> myFlowNodes, List<Node> elFlowNodes, Node startNode, int[,] distanceMatrix) {
        var myFlowNodeCount = myFlowNodes.Count;
        var myResult = new Result(myFlowNodeCount);
        Permute(myFlowNodes, 0, myFlowNodeCount-1, myResult, startNode, distanceMatrix);

        var elFlowNodeCount = elFlowNodes.Count;
        var elResult = new Result(elFlowNodeCount);
        Permute(elFlowNodes, 0, elFlowNodeCount-1, elResult, startNode, distanceMatrix); 

        return myResult.Max+elResult.Max;
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
                if (timer > 26)
                    break;

                summedFlow += currentFlow;
            }

            timer++; 
            if (timer > 26)
                break;

            summedFlow += currentFlow;
            currentFlow += nextNode.Flow;
            currentNode = nextNode;
        }

        if (timer < 26) {
            summedFlow += currentFlow * (26-timer);
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

/*
fixed approach

var myFlowNodes = new List<Node> {            
            GetNodeById(nodes, "DS"),
            GetNodeById(nodes, "QQ"),
            GetNodeById(nodes, "VK"),
            GetNodeById(nodes, "WL"),  //
            GetNodeById(nodes, "WH"),  //                     
            GetNodeById(nodes, "HG"),  //         
            GetNodeById(nodes, "IT")   //
        };

        var myFlowNodeCount = myFlowNodes.Count;
        var myResult = new Result(myFlowNodeCount);
        Permute(myFlowNodes, 0, myFlowNodeCount-1, myResult, startNode, distanceMatrix); 

        var elFlowNodes = new List<Node> {
            GetNodeById(nodes, "JX"),
            GetNodeById(nodes, "IK"),
            GetNodeById(nodes, "CF"),
            GetNodeById(nodes, "WM"),
            GetNodeById(nodes, "EZ"),
            GetNodeById(nodes, "LB"),
            GetNodeById(nodes, "BV"),
            GetNodeById(nodes, "UY"),
        };

        var elFlowNodeCount = elFlowNodes.Count;
        var elResult = new Result(elFlowNodeCount);
        Permute(elFlowNodes, 0, elFlowNodeCount-1, elResult, startNode, distanceMatrix);

*/

/*
var result = new List<string>();

        foreach(var node in nodes) {
            foreach(var link in node.Links) {
                var newEntry = $"{NodeToString(node)} --- {NodeToString(link)}";
                var opposite = $"{NodeToString(link)} --- {NodeToString(node)}";

                if (!result.Contains(opposite))
                    result.Add(newEntry);
            }
        }

        foreach(var r in result) {
            Console.WriteLine(r);
        }
*/

/*
graph TD
ZT ---> QQ-11
ZT ---> DS-9
JX-22 ---> CI
JX-22 ---> ZH
JX-22 ---> UR
EM ---> WH-12
EM ---> IT-10
AA ---> EQ
AA ---> QD
AA ---> NP
AA ---> ZP
AA ---> KX
HW ---> CI
HW ---> BV-13
IK-8 ---> ET
IK-8 ---> NU
IK-8 ---> ZO
IK-8 ---> XL
IK-8 ---> QD
HA ---> WQ
HA ---> LB-17
WH-12 ---> EM
WH-12 ---> LW
KU ---> BV-13
KU ---> CF-18
QD ---> AA
QD ---> IK-8
CF-18 ---> KU
CF-18 ---> JT
CF-18 ---> CM
VC ---> AD
VC ---> UY-5
JT ---> CF-18
JT ---> ZH
QQ-11 ---> ZT
ZP ---> EZ-16
ZP ---> AA
LI ---> LB-17
LI ---> CM
CI ---> HW
CI ---> JX-22
VK-6 ---> YM
VK-6 ---> LC
VK-6 ---> HE
VK-6 ---> NU
VK-6 ---> TI
WL-20 ---> LW
WL-20 ---> TO
TI ---> VK-6
TI ---> YW
NU ---> VK-6
NU ---> IK-8
DS-9 ---> NP
DS-9 ---> MV
DS-9 ---> FR
DS-9 ---> ZT
DS-9 ---> YW
HE ---> VK-6
HE ---> EQ
ZH ---> JT
ZH ---> JX-22
TO ---> MT
TO ---> WL-20
CM ---> LI
CM ---> CF-18
WM-14 ---> MO
WM-14 ---> WQ
WM-14 ---> EC
WM-14 ---> RN
EZ-16 ---> RT
EZ-16 ---> RZ
EZ-16 ---> ZP
PB ---> YM
PB ---> UY-5
XL ---> IK-8
XL ---> MS
LB-17 ---> LI
LB-17 ---> HA
LB-17 ---> ON
LB-17 ---> UR
LB-17 ---> AD
WQ ---> WM-14
WQ ---> HA
BV-13 ---> KU
BV-13 ---> RT
BV-13 ---> HW
BV-13 ---> MO
BV-13 ---> EH
RN ---> WM-14
RN ---> RZ
LW ---> WH-12
LW ---> WL-20
NP ---> AA
NP ---> DS-9
MT ---> TO
MT ---> HG-19
ET ---> IK-8
ET ---> EC
HG-19 ---> MT
MV ---> UY-5
MV ---> DS-9
RT ---> BV-13
RT ---> EZ-16
ON ---> LB-17
ON ---> EH
MO ---> BV-13
MO ---> WM-14
UY-5 ---> PB
UY-5 ---> BR
UY-5 ---> MS
UY-5 ---> VC
UY-5 ---> MV
UR ---> JX-22
UR ---> LB-17
YM ---> PB
YM ---> VK-6
RZ ---> RN
RZ ---> EZ-16
AD ---> VC
AD ---> LB-17
EH ---> ON
EH ---> BV-13
EQ ---> AA
EQ ---> HE
KX ---> AA
KX ---> BR
BR ---> UY-5
BR ---> KX
LC ---> VK-6
LC ---> IT-10
YW ---> TI
YW ---> DS-9
EC ---> ET
EC ---> WM-14
IT-10 ---> LC
IT-10 ---> EM
MS ---> UY-5
MS ---> XL
FR ---> DS-9
FR ---> ZO
ZO ---> FR
ZO ---> IK-8
  

*/
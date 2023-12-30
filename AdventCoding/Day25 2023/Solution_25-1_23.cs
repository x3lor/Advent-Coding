#pragma warning disable CS8602


public class Solution_25_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var nodes = Input_25_23.input
                               .Split('\n')
                               .Select(line => line.Remove(3, 1))
                               .Select(line => line.Split(' ').ToList())
                               .SelectMany(node => node)
                               .Distinct()
                               .Select(node => new Node(node))
                               .ToList();

        var nodesDict = nodes.ToDictionary(node => node.Name, node => node);

        int linkId = 0;

        foreach(var line in Input_25_23.input.Split('\n')) {
            var node = nodesDict[line[..3]];
            node.Links.AddRange(line[5..].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(nodeName => nodesDict[nodeName])
                                         .ToList());
            node.LinkIds.AddRange(Enumerable.Range(linkId, node.Links.Count).ToList());
            linkId += node.LinkIds.Count;

            foreach (var newAddedLinks in node.Links) {
                newAddedLinks.Links.Add(node);
                newAddedLinks.LinkIds.Add(linkId++);
            }
        }

        var linkIdDict = new Dictionary<int, Node>();
        foreach (var node in nodes) {
            foreach(var link in node.LinkIds) {
                linkIdDict.Add(link, node);
            }
        }

        foreach(var node in nodes) {
            node.MiddleDistanceToAllOthers = GetMiddleDistance(node);
        }

        nodes = nodes.OrderBy(n => n.MiddleDistanceToAllOthers).ToList();

        var allLinks = GetAllLinks(nodes).OrderBy(link => link.From.MiddleDistanceToAllOthers).ToList();

        var testNode1 = nodes.Last();
        var testNode2 = GetTestNode2(testNode1);

        for (int i=2; i<allLinks.Count; i++) {

            Console.WriteLine($"testing i=" + i);

            for (int j=1; j<i; j++) {
                for (int k=0; k<j; k++) {

                    if (TestGraph(allLinks[i], allLinks[j], allLinks[k], testNode1, testNode2)) {
                        var resultNumber = GetReachableNodes(allLinks[i], allLinks[j], allLinks[k], testNode1) *
                                           GetReachableNodes(allLinks[i], allLinks[j], allLinks[k], testNode2);
                        Console.WriteLine(resultNumber);
                        goto quiteLoops;
                    }
                }
            }
        }

        quiteLoops: 
        Console.WriteLine($"Done!");
    }  

    private static int GetReachableNodes(Link linkToIgnore1, Link linkToIgnore2, Link linkToIgnore3, Node n) {
         
        var alreadySearchedNodes = new List<string> { n.Name };
        var seachQueue = new Stack<Node>();
        seachQueue.Push(n);

        var resultCount = 1;

        while (seachQueue.Count > 0) {
            var currentNode = seachQueue.Pop();
            
            foreach(var link in currentNode.Links)
                if (!alreadySearchedNodes.Contains(link.Name) &&
                    !linkToIgnore1.Check(currentNode, link) && 
                    !linkToIgnore2.Check(currentNode, link) && 
                    !linkToIgnore3.Check(currentNode, link)) {
                    alreadySearchedNodes.Add(link.Name);
                    resultCount++;
                    seachQueue.Push(link);
                }                  
        }

        return resultCount;
    }

    private Node GetTestNode2 (Node testNode1) {

        var maxDistanceNode = testNode1;
        var maxDistance = 0;

        var alreadySearchedNodes = new List<string> { testNode1.Name };
        var seachQueue = new Stack<Tuple<Node, int>>();
        seachQueue.Push(new Tuple<Node, int>(testNode1, 0));

        while (seachQueue.Count > 0) {
            var currentNode = seachQueue.Pop();
            var currentDistance = currentNode.Item2;

            if (currentDistance > maxDistance) {
                maxDistance = currentDistance;
                maxDistanceNode = currentNode.Item1;
            }
            
            foreach(var link in currentNode.Item1.Links)
                if (!alreadySearchedNodes.Contains(link.Name)) {
                    alreadySearchedNodes.Add(link.Name);
                    seachQueue.Push(new Tuple<Node, int>(link, currentDistance+1));
                }
                    
        }

        return maxDistanceNode;
    }

    private double GetMiddleDistance(Node n) {

        var sum = 0;

        var alreadySearchedNodes = new List<string> { n.Name };
        var seachQueue = new Stack<Tuple<Node, int>>();
        seachQueue.Push(new Tuple<Node, int>(n, 0));

        while (seachQueue.Count > 0) {
            var currentNode = seachQueue.Pop();
            var currentDistance = currentNode.Item2;
            sum += currentDistance;
            
            foreach(var link in currentNode.Item1.Links)
                if (!alreadySearchedNodes.Contains(link.Name)) {
                    alreadySearchedNodes.Add(link.Name);
                    seachQueue.Push(new Tuple<Node, int>(link, currentDistance+1));
                }
                    
        }

        return (double)sum/alreadySearchedNodes.Count;
    }

    private List<Link> GetAllLinks(List<Node> nodes) {

        var n = nodes.First();

        var alreadySearchedNodes = new List<string> { n.Name };
        var seachQueue = new Stack<Node>();
        seachQueue.Push(n);

        var resultList = new List<Link>();

        while (seachQueue.Count > 0) {
            var currentNode = seachQueue.Pop();
            
            foreach(var link in currentNode.Links)
                if (!alreadySearchedNodes.Contains(link.Name)) {
                    alreadySearchedNodes.Add(link.Name);
                    resultList.Add(new Link(currentNode, link));
                    seachQueue.Push(link);
                }                  
        }

        return resultList;
    }

    private bool TestGraph(Link linkToIgnore1, Link linkToIgnore2, Link linkToIgnore3, 
                          Node testNode1, Node testNode2) {

        var alreadySearchedNodes = new List<string> { testNode1.Name };
        var seachQueue = new Stack<Node>();
        seachQueue.Push(testNode1);

        while (seachQueue.Count > 0) {
            var currentNode = seachQueue.Pop();

            if (currentNode == testNode2)
                return false;
            
            foreach(var link in currentNode.Links)
                if (!alreadySearchedNodes.Contains(link.Name) &&
                    !linkToIgnore1.Check(currentNode, link) && 
                    !linkToIgnore2.Check(currentNode, link) && 
                    !linkToIgnore3.Check(currentNode, link)) {
                    alreadySearchedNodes.Add(link.Name);
                    seachQueue.Push(link);
                }                  
        }
        return true;
    }

    private class Link {

        public Link(Node from, Node to)
        {
            From = from;
            To = to;
        }

        public Node To { get; }
        public Node From { get; }

        public bool Check(Node n1, Node n2) {
            return (From==n1 && To==n2) || (From==n2 && To==n1);
        }
    }

    private class Node {

        public Node(string name) {
            Name = name;
            Links = new List<Node>();
            LinkIds = new List<int>();
        }

        public string Name { get; }
        public List<Node> Links { get; }
        public List<int> LinkIds { get; }
        public double MiddleDistanceToAllOthers { get; set; }

        public override string ToString() => Name;
    }
}
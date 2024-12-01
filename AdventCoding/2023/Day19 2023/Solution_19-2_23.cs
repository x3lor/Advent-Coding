#pragma warning disable CS8618 
#pragma warning disable CS8625
using System.Numerics;

public class Solution_19_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var collectionRuleSets = Input_19_23.inputFlow
                                            .Split('\n')
                                            .Select(line => new RuleSet(line))
                                            .ToList();
        var rulesDict = collectionRuleSets.ToDictionary(r => r.Name, r => r);
        foreach(var ruleSet in collectionRuleSets) {
            ruleSet.AddRulesToRuleSets(rulesDict);
        }

        var start = rulesDict["in"]; 
        var topNode = new RuleGraph(null, start, rulesDict);
        var acceptLeafs = topNode.GetAllAcceptLeafs().ToList();
        using var writer = new StreamWriter("debug.txt");
        var resultList = acceptLeafs.Select(al => new PartCalculator(al))
                                    .Select(pc => pc.GetPossiblePartCount(writer))
                                    .ToList();

        var endResult = new BigInteger(0);

        foreach (var result in resultList) {            
            endResult += result;
        }

        Console.WriteLine($"Done! {endResult}");
    }

    private enum Variable {
        X,M,A,S
    }

    private class ConditionSet {

        private readonly RuleGraph leaf;

        public ConditionSet(RuleGraph leaf) {
            xMin = mMin = aMin = sMin = 1;
            xMax = mMax = aMax = sMax = 4000;
            this.leaf = leaf;
        }

        public void AddPassingCondition(Rule r) {
            if (r.Type == RuleType.Accept ||
                r.Type == RuleType.Reject ||
                r.Type == RuleType.Jump) {
                return;
            }

            if (r.Op == Operator.Greater) {
                switch (r.Var) {
                    case Variable.X: if (xMin <= r.Number) xMin = r.Number+1; break;
                    case Variable.M: if (mMin <= r.Number) mMin = r.Number+1; break;
                    case Variable.A: if (aMin <= r.Number) aMin = r.Number+1; break;
                    case Variable.S: if (sMin <= r.Number) sMin = r.Number+1; break;
                }
            } else {
                switch (r.Var) {
                    case Variable.X: if (xMax >= r.Number) xMax = r.Number-1; break;
                    case Variable.M: if (mMax >= r.Number) mMax = r.Number-1; break;
                    case Variable.A: if (aMax >= r.Number) aMax = r.Number-1; break;
                    case Variable.S: if (sMax >= r.Number) sMax = r.Number-1; break;
                }
            }
        }

        public void AddNotPassingCondition(Rule r) {            
            if (r.Op == Operator.Greater) {
                switch (r.Var) {
                    case Variable.X: if (xMax >= r.Number) xMax = r.Number; break;
                    case Variable.M: if (mMax >= r.Number) mMax = r.Number; break;
                    case Variable.A: if (aMax >= r.Number) aMax = r.Number; break;
                    case Variable.S: if (sMax >= r.Number) sMax = r.Number; break;
                }
            } else {
                switch (r.Var) {
                    case Variable.X: if (xMin <= r.Number) xMin = r.Number; break;
                    case Variable.M: if (mMin <= r.Number) mMin = r.Number; break;
                    case Variable.A: if (aMin <= r.Number) aMin = r.Number; break;
                    case Variable.S: if (sMin <= r.Number) sMin = r.Number; break;
                }
            }
        }

        private int xMin;
        private int xMax;
        private int mMin;
        private int mMax;
        private int aMin;
        private int aMax;
        private int sMin;
        private int sMax;        

        public BigInteger GetPossibilities() {
            if (xMin >= xMax || mMin >= mMax || aMin >= aMax || sMin >= sMax) 
                return new BigInteger(0);

            return new BigInteger(xMax-xMin+1) *
                   new BigInteger(mMax-mMin+1) *
                   new BigInteger(aMax-aMin+1) *
                   new BigInteger(sMax-sMin+1);
        }

        public void PrintResult(StreamWriter writer) {
            var listOfNodes = new List<RuleGraph>() {leaf};

            var currentNode = leaf;
            while (currentNode.MasterNode != null) {
                currentNode = currentNode.MasterNode;
                listOfNodes.Add(currentNode);
            }
            listOfNodes.Reverse();

            var path = string.Join(" -> ", listOfNodes.Select(node => node.Name).ToList());
            writer.WriteLine($"Path: " + path);                
            writer.WriteLine($"X: {xMin} - {xMax}");
            writer.WriteLine($"M: {mMin} - {mMax}");
            writer.WriteLine($"A: {aMin} - {aMax}");
            writer.WriteLine($"S: {sMin} - {sMax}\n\n");
        }
    }

    private class PartCalculator {

        private readonly RuleGraph acceptLeaf;

        public PartCalculator(RuleGraph acceptLeaf) {
            this.acceptLeaf = acceptLeaf;
        }

        public BigInteger GetPossiblePartCount(StreamWriter writer) {

            var conditionSet = new ConditionSet(acceptLeaf);
            var currentNode = acceptLeaf;
            
            // go the graph up and gather information
            while (currentNode.MasterNode != null) {
                var targetBefore = currentNode;
                currentNode = currentNode.MasterNode;

                // find condtion that leads to currentNode
                var ruleSet = currentNode.RuleSet;
                foreach (var rule in ruleSet.Rules) {
                    if (rule.TargetRuleSet == targetBefore.RuleSet) {
                        conditionSet.AddPassingCondition(rule);
                        break;
                    } else {
                        conditionSet.AddNotPassingCondition(rule);
                    }
                }
            }

            conditionSet.PrintResult(writer);
            return conditionSet.GetPossibilities();
        }
    }



    private class RuleSet {

        private static int Id = 0;
        private readonly int id = Id++;

        public List<Rule> Rules { get; private set; }
        public RuleSetType RuleSetType { get; }

        private readonly string init;

        public RuleSet(string init) {
            this.init = init;
            var indexOfBracket = init.IndexOf('{');
            Name = init[..indexOfBracket];
            RuleSetType = RuleSetType.Collection;
        }

        public void AddRulesToRuleSets(Dictionary<string, RuleSet> allCollectionRuleSets) {
            var indexOfBracket = init.IndexOf('{');
            Rules = init[(indexOfBracket+1)..^1].Split(',')
                                                .Select(s => new Rule(s, allCollectionRuleSets))
                                                .ToList();
        }

        public RuleSet(bool accept) {
            if (accept)
                RuleSetType = RuleSetType.Accept;
            else
                RuleSetType = RuleSetType.Reject;

            Rules = null;
        }
 

        public string Name { get; }

    }

    private class RuleGraph {

        public RuleGraph(RuleGraph masterNode, RuleSet ruleSet, Dictionary<string, RuleSet> allRuleSets) {
            IsLeaf = false;
            Accept = false;
            Name = ruleSet.Name;

            var subNodes = new List<RuleGraph>();

            foreach(var rule in ruleSet.Rules) {
                switch (rule.Type) {
                    case RuleType.Accept: {
                        subNodes.Add(new RuleGraph(this, rule.TargetRuleSet, true));
                        break;
                    }
                    case RuleType.Reject: {
                        subNodes.Add(new RuleGraph(this, rule.TargetRuleSet, false));
                        break;
                    }
                    case RuleType.Jump: {
                        subNodes.Add(new RuleGraph(this, rule.TargetRuleSet, allRuleSets));
                        break;
                    }
                    case RuleType.Condition: {
                        if (rule.TargetRuleSet.RuleSetType == RuleSetType.Accept) {
                            subNodes.Add(new RuleGraph(this, rule.TargetRuleSet, true));
                        } else if (rule.TargetRuleSet.RuleSetType == RuleSetType.Reject) {
                            subNodes.Add(new RuleGraph(this, rule.TargetRuleSet, false));
                        } else {
                            subNodes.Add(new RuleGraph(this,rule.TargetRuleSet, allRuleSets));
                        }
                        break;
                    }
                }
            }

            SubNodes = subNodes;
            MasterNode = masterNode;
            RuleSet = ruleSet;
        }

        private RuleGraph(RuleGraph masterNode, RuleSet ruleSet, bool accept) {
            IsLeaf = true;
            Accept = accept;
            SubNodes = new List<RuleGraph>();
            Name = accept ? "A" : "R";
            MasterNode = masterNode;
            RuleSet = ruleSet;
        }

        public bool IsLeaf { get; }
        public bool Accept { get; }
        public RuleSet RuleSet { get; }
        public RuleGraph MasterNode { get; }
        public List<RuleGraph> SubNodes { get; }
        public string Name { get; }

        public override string ToString() => Name;

        public IEnumerable<RuleGraph> GetAllAcceptLeafs() {

            if (IsLeaf) {
                if (Accept) {
                    return new List<RuleGraph>() {this};
                } else {
                    return new List<RuleGraph>();
                }
            } else {
                return SubNodes.SelectMany(s => s.GetAllAcceptLeafs());
            }
        }
    }

    private enum RuleType    { Condition, Accept, Reject, Jump }
    private enum RuleSetType { Collection, Accept, Reject }
    private enum Operator    { Greater, Less}

    private class Rule {     
        public Rule(string init, Dictionary<string, RuleSet> allCollectionRuleSets) {

            if (init == "A") {
                Type = RuleType.Accept;
                TargetRuleSet = new RuleSet(true);
            } else if (init == "R") {
                Type = RuleType.Reject;
                TargetRuleSet = new RuleSet(false);
            } else if (init.Contains('<') || init.Contains('>')) {
                Type = RuleType.Condition;

                Var = init[0] switch {
                    'x' => Variable.X,
                    'm' => Variable.M,
                    'a' => Variable.A,
                    's' => Variable.S,
                    _ => throw new NotImplementedException()
                };

                Op = init[1] switch  {
                    '>' => Operator.Greater,
                    '<' => Operator.Less,
                    _ => throw new NotImplementedException()
                };

                var indexOfColon = init.IndexOf(':');
                Number = int.Parse(init[2..indexOfColon]);
                var targetRuleSetName = init[(indexOfColon+1)..];

                if (targetRuleSetName == "A")
                    TargetRuleSet = new RuleSet(true);
                else if (targetRuleSetName == "R") {
                    TargetRuleSet = new RuleSet(false);
                } else {
                    TargetRuleSet = allCollectionRuleSets[targetRuleSetName];
                }                
            } else {
                Type = RuleType.Jump;
                TargetRuleSet = allCollectionRuleSets[init];
            }
        }
        
        public RuleType Type { get; }
        public Operator Op { get; }
        public Variable Var { get; }
        public int Number { get; }
        public RuleSet TargetRuleSet { get; }
    }
}
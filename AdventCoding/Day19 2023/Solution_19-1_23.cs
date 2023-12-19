public class Solution_19_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        // {x=1679,m=44,a=2067,s=496}
        // qqz{s>2770:qs,m<1801:hdj,R}

        var rules = Input_19_23.inputFlow
                               .Split('\n')
                               .Select(line => new RuleSet(line))
                               .ToList();
        var rulesDict = rules.ToDictionary(r => r.Name, r => r);
        var startingRule = rulesDict["in"];
        var parts = Input_19_23.inputParts
                               .Split('\n')
                               .Select(line => new Part(line))
                               .ToList();
            
        var sum = parts.Where(p => startingRule.AcceptPart(p, rulesDict))
                       .Select(p => p.Sum())
                       .Sum();

        Console.WriteLine($"Done! Sum: {sum}");
    }

    private class Part {

        public Part(string init) {
            values = init[1..^1].Split(',')
                                .Select(s => int.Parse(s[2..]))
                                .ToList();
        }

        private readonly List<int> values;

        public int GetValue(Variable v) {
            return values[(int)v];
        }

        public long Sum() {
            return values.Sum();
        }
    }

    private enum Variable {
        X,M,A,S
    }

    private class RuleSet {

        private readonly List<Rule> rules;

        // qqz{s>2770:qs,m<1801:hdj,R}
        public RuleSet(string init) {
            var indexOfBracket = init.IndexOf('{');
            Name = init[..indexOfBracket];
            rules = init[(indexOfBracket+1)..^1].Split(',')
                                                .Select(s => new Rule(s))
                                                .ToList();
        }

        public string Name { get; }

        public bool AcceptPart(Part part, Dictionary<string, RuleSet> allRuleSets) {
            return rules.First(rule => rule.IsRuleAppliable(part)).ApplyRule(part, allRuleSets);
        }
    }

    private class Rule {

        private enum RuleType { Condition, Accept, Reject, Jump }
        private enum Operator { Greater, Less}

        // s>2770:qs
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Rule(string init) {

            if (init == "A") {
                Type = RuleType.Accept;
            } else if (init == "R") {
                Type = RuleType.Reject;
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
                TargetRuleSet = init[(indexOfColon+1)..];
            } else {
                Type = RuleType.Jump;
                TargetRuleSet = init;
            }
        }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private readonly RuleType Type;
        private readonly Operator Op;
        private readonly Variable Var;
        private readonly int Number;
        private readonly string TargetRuleSet;


        public bool IsRuleAppliable(Part part) {

            if (Type == RuleType.Accept || Type == RuleType.Reject || Type == RuleType.Jump) return true;

            var partNumber = part.GetValue(Var);
            
            if (Op == Operator.Greater)
                return partNumber > Number;
            else
                return partNumber < Number;
        }

        public bool ApplyRule(Part part, Dictionary<string, RuleSet> allRuleSets) {

            if (Type == RuleType.Accept) return true;
            if (Type == RuleType.Reject) return false;

            if (TargetRuleSet == "A") return true;
            if (TargetRuleSet == "R") return false;

            return allRuleSets[TargetRuleSet].AcceptPart(part, allRuleSets);
        }
    }
    
}

public class Solution_5_1_24 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var rules   = Input_5_24.input1.Split('\n').Select(s => new  Rule(s)).ToList();
        var updates = Input_5_24.input2.Split('\n').Select(s => new Pages(s)).ToList();

        var sum = updates.Where(p => p.Check(rules))
                         .Select(p => p.GetMiddle())
                         .Sum();

        Console.WriteLine($"done! Sum: {sum}");
    }

    private class Rule {

        public Rule (string s) {
            var parts = s.Split('|');
            X = int.Parse(parts[0]);
            Y = int.Parse(parts[1]);
        }

        public int X { get; }
        public int Y { get; }
    }

    private class Pages {

        public Pages(string s) {
            PagesToUpdate = s.Split(',').Select(s => int.Parse(s)).ToList();
        }

        public List<int> PagesToUpdate { get; }

        public int GetMiddle() {
            return PagesToUpdate[(PagesToUpdate.Count-1)/2];
        }

        public bool Check(List<Rule> rules) {

            for (int i=0; i<PagesToUpdate.Count-1; i++) {
                for (int j=i+1; j<PagesToUpdate.Count; j++) {
                    if (!CheckPair(rules, PagesToUpdate[i], PagesToUpdate[j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CheckPair(List<Rule> rules, int x, int y) => !rules.Where(r => r.Y == x).Any(r => r.X == y);
    }
}


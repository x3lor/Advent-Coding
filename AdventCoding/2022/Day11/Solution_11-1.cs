public class Solution_11_1 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var monkeys = new List<Monkey>();

        var lines = Input_11.input.Split('\n').ToList();
        for (int i=0; i<(lines.Count+1)/7; i++) {
            monkeys.Add(new Monkey(lines.GetRange(i*7+1, 5)));
        }

        for (int round=0; round<20; round++) {
            foreach (var activeMonkey in monkeys) {
                while(activeMonkey.HasMoreItems()) {
                    activeMonkey.ActionCounter++;
                    var item = activeMonkey.GetNextItem();
                    var updatedWorryLevel = activeMonkey.UpdateWorryLevel(item);
                    var monkeyIdToPassTheItem = activeMonkey.GetNextMonkey(updatedWorryLevel);
                    monkeys[monkeyIdToPassTheItem].TakeItemFromOtherMonkey(updatedWorryLevel);
                }
            }
        }

        var result = monkeys.Select(m => m.ActionCounter)
                            .OrderByDescending(a => a)
                            .Take(2)
                            .Aggregate((a, b) => a*b);
       
        Console.WriteLine($"done! Result: {result}");
    }

    public class Monkey {

        private List<int> items;
        private Func<int, int> operation;
        private Func<int, bool> test;
        private int targetTrue;
        private int targetFalse;

        public Monkey(IList<string> description) {
            
            items = description[0].Substring(18)
                                  .Split(',')
                                  .Select(s => int.Parse(s.Trim()))
                                  .ToList();

            var updateOperator = description[1].Substring(23,1);
            var updateFactor   = description[1].Substring(25);
            operation = level => {
                
                int factor1 = level;
                int factor2 = updateFactor=="old" ? level : int.Parse(updateFactor);

                return updateOperator=="+" ? factor1+factor2 : factor1*factor2;
            };

            var testFactor = int.Parse(description[2].Substring(21));
            test = input => {
                return input % testFactor == 0;
            };
            targetTrue  = int.Parse(description[3].Substring(29));
            targetFalse = int.Parse(description[4].Substring(30));
        }

        public int ActionCounter { get; set; } = 0;

        public int GetNextItem() {
            var item = items.First();
            items.Remove(item);
            return item;
        }

        public void TakeItemFromOtherMonkey(int item) {
            items.Add(item);
        }

        public bool HasMoreItems() => items.Any();
        public int UpdateWorryLevel(int item) => operation(item)/3;
        public int GetNextMonkey(int item) => test(item) ? targetTrue : targetFalse;
    }    
}
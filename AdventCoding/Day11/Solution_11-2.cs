
public class Solution_11_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var monkeys = new List<Monkey>();

        var lines = Input_11.input.Split('\n').ToList();
        for (int i=0; i<(lines.Count+1)/7; i++) {
            monkeys.Add(new Monkey(lines.GetRange(i*7+1, 5)));
        }

        for (int round=0; round<10000; round++) {
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
                            .Select(a => (long)a)
                            .Aggregate((a, b) => a*b);
       
        Console.WriteLine($"done! Result: {result}");
    }

    public class Monkey {

        private List<long> items;
        private Func<long, long> operation;
        private Func<long, bool> test;
        private int targetTrue;
        private int targetFalse;

        public Monkey(IList<string> description) {
            
            items = description[0].Substring(18)
                                  .Split(',')
                                  .Select(s => long.Parse(s.Trim()))
                                  .ToList();

            var updateOperator = description[1].Substring(23,1);
            var updateFactor   = description[1].Substring(25);
            operation = level => {
                
                long factor1 = level;
                long factor2 = updateFactor=="old" ? level : int.Parse(updateFactor);

                var result = updateOperator=="+" ? factor1+factor2 : factor1*factor2;
                var breakFactor = 5 * 2 * 19 * 7 * 17 * 13 * 3 * 11;

                if (result>breakFactor) {
                    result = result%breakFactor;
                }

                return result;
            };

            var testFactor = int.Parse(description[2].Substring(21));
            test = input => {
                return input % testFactor == 0;
            };
            targetTrue  = int.Parse(description[3].Substring(29));
            targetFalse = int.Parse(description[4].Substring(30));
        }

        public int ActionCounter { get; set; } = 0;

        public long GetNextItem() {
            var item = items.First();
            items.Remove(item);
            return item;
        }

        public void TakeItemFromOtherMonkey(long item) {
            items.Add(item);
        }

        public bool HasMoreItems() => items.Any();
        public long UpdateWorryLevel(long item) => operation(item);
        public int GetNextMonkey(long item) => test(item) ? targetTrue : targetFalse;
    }    
}
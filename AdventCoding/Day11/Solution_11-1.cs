public class Solution_11_1 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var lines = Input_11.input.Split('\n').ToList();
       
        var monkeys = new List<Monkey>();

        for (int i=0; i<8; i++) {
            monkeys.Add(new Monkey(lines.GetRange(i*7, 6)));
        }

        for (int round=0; round<20; round++) {
            for (int monkeyId=0; monkeyId<8; monkeyId++) {
                var activeMonkey = monkeys[monkeyId];

                while(activeMonkey.HasMoreItems()) {
                    var item = activeMonkey.GetItem();

                    activeMonkey.ActionCounter++;
                    var updatedWorryLevel = activeMonkey.Operation(item)/3;

                    var nextMonkey = activeMonkey.GetNextMonkey(updatedWorryLevel);
                    monkeys[nextMonkey].TakeItem(updatedWorryLevel);
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
        public Monkey(IList<string> description) {
            Id = int.Parse(description[0].Substring(7, 1));
            Items = description[1].Substring(18)
                                  .Split(',')
                                  .Select(s => int.Parse(s.Trim()))
                                  .ToList();
            var opDescription = description[2].Substring(23,1);
            var opFactor      = description[2].Substring(25);
            Operation = level => {
                
                int factor1 = level;
                int factor2 = opFactor=="old" ? level : int.Parse(opFactor);

                return opDescription=="+" ? factor1+factor2 : factor1*factor2;
            };
            var testFactor = int.Parse(description[3].Substring(21));
            Test = input => {
                return input % testFactor == 0;
            };
            TargetTrue  = int.Parse(description[4].Substring(29));
            TargetFalse = int.Parse(description[5].Substring(30));
        }


        public int Id { get; }
        public List<int> Items { get; }
        public Func<int, int> Operation { get; }
        public Func<int, bool> Test { get; }
        public int TargetTrue { get; }
        public int TargetFalse { get; }

        public int ActionCounter { get; set; } = 0;

        public int GetItem() {
            if (!Items.Any())
                return -1;

            var item = Items.First();
            Items.Remove(item);
            return item;
        }

        public int GetNextMonkey(int item) {
            if (Test(item))
                return TargetTrue;
            else 
                return TargetFalse;
        }

        public bool HasMoreItems() {
            return Items.Any();
        }

        public void TakeItem(int item) {
            Items.Add(item);
        }
    }    
}
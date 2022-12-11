using System.Numerics;

public class Solution_11_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var lines = Input_11.input.Split('\n');
       
        var monkeys = new List<Monkey>();

        for (int i=0; i<8; i++) {
            var monkeyDescription = new List<string>();
            for (int j=0; j<6; j++) {
                monkeyDescription.Add(lines[i*7+j]);
            }
            monkeys.Add(new Monkey(monkeyDescription));
        }

        for (int round=0; round<10000; round++) {
            for (int monkeyId=0; monkeyId<8; monkeyId++) {
                var activeMonkey = monkeys[monkeyId];

                while(activeMonkey.HasMoreItems()) {
                    var item = activeMonkey.GetItem();

                    activeMonkey.ActionCounter++;
                    var updatedWorryLevel = activeMonkey.Operation(item);

                    var nextMonkey = activeMonkey.GetNextMonkey(updatedWorryLevel);
                    monkeys[nextMonkey].TakeItem(updatedWorryLevel);
                }
            }
        }

        var result = monkeys.Select(m => m.ActionCounter)
                            .OrderByDescending(a => a)
                            .Take(2)
                            .ToList();
                            //.Aggregate((a, b) => a*b);
        long r = result[0]*(long)result[1];

        Console.WriteLine($"done! Result: {r}");
    }

    public class Monkey {
        public Monkey(IList<string> description) {
            Id = int.Parse(description[0].Substring(7, 1));
            Items = description[1].Substring(18)
                                  .Split(',')
                                  .Select(s => long.Parse(s.Trim()))
                                  .ToList();
            var opDescription = description[2].Substring(23,1);
            var opFactor      = description[2].Substring(25);
            Operation = level => {
                
                long factor1 = level;
                long factor2 = opFactor=="old" ? level : long.Parse(opFactor);

                var r = opDescription=="+" ? factor1+factor2 : factor1*factor2;

                var factor = 5 * 2 * 19 * 7 * 17 * 13 * 3 * 11;

                if (r>factor) {
                    r = r%factor;
                }

                return r;
            };
            var testFactor = int.Parse(description[3].Substring(21));
            Test = input => {
                return input % testFactor == 0;
            };
            TargetTrue  = int.Parse(description[4].Substring(29));
            TargetFalse = int.Parse(description[5].Substring(30));
        }


        public int Id { get; }
        public List<long> Items { get; }
        public Func<long, long> Operation { get; }
        public Func<long, bool> Test { get; }
        public int TargetTrue { get; }
        public int TargetFalse { get; }

        public int ActionCounter { get; set; } = 0;

        public long GetItem() {
            if (!Items.Any())
                return -1;

            var item = Items.First();
            Items.Remove(item);
            return item;
        }

        public int GetNextMonkey(long item) {
            if (Test(item))
                return TargetTrue;
            else 
                return TargetFalse;
        }

        public bool HasMoreItems() {
            return Items.Any();
        }

        public void TakeItem(long item) {
            Items.Add(item);
        }
    }    
}
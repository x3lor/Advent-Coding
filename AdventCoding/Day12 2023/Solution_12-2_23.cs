using System.Text;

public class Solution_12_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

    
        //Console.WriteLine(GetAllPerm(new List<int>{9, 1, 0}, 0, 1, true));

        // var sum = 0;
        
        // foreach(var line in Input_12_23.example.Split('\n')) {

            

        //     var parts = line.Split(' ');
        //     var pattern    = parts[0]+"?"+parts[0]+"?"+parts[0]+"?"+parts[0]+"?"+parts[0];
        //     var numberList = parts[1]+","+parts[1]+","+parts[1]+","+parts[1]+","+parts[1];
        // }
        // Console.WriteLine($"Done! Sum: {sum}");


        var sum = 0L;        
        var counter = 0;

        foreach(var line in Input_12_23.example.Split('\n')) {
            Console.Write($"\r{counter++} of 1000");
            var parts = line.Split(' '); 
            var pattern    = parts[0]+"?"+parts[0]+"?"+parts[0]+"?"+parts[0]+"?"+parts[0];
            var numberList = parts[1]+","+parts[1]+","+parts[1]+","+parts[1]+","+parts[1];
            var game = new Game(pattern, numberList);
            sum += game.GetPosibilities();  
        }

        Console.WriteLine($"\nDone! Sum: {sum}");
    }

    // private static int Counter = 0;

    // private long GetAllPerm(List<int> spaceList, int subtract, int add, bool isFirst) {

    //         long sum = 0;
            
    //         Console.WriteLine($"{Counter++}: " + string.Join('+', spaceList));
    //         sum++;

    //         while (spaceList[subtract] > 1 || (isFirst && spaceList[subtract] > 0)) {
    //             spaceList[subtract]--;
    //             spaceList[add]++;

    //             if (add < spaceList.Count-1)
    //                 sum += GetAllPerm(spaceList.ToList(), subtract+1, add+1, false);
    //             else {                    
    //                 Console.WriteLine($"{Counter++}: " + string.Join('+', spaceList));
    //                 sum++;
    //             }
    //         }

    //         return sum;
    //     }

    public class Game {

        private readonly string pattern;
        private readonly List<int> numberList;

        public Game(string pattern, string numberList) {
            this.pattern = pattern;

            this.numberList = numberList.Split(',')
                                        .Select(n => int.Parse(n))
                                        .ToList();
        }

        public long GetPosibilities() {

            var freeSpace = pattern.Length - numberList.Sum();
            var split = numberList.Count+1;

            var startList = new List<int>();
            startList.Add(freeSpace-(split-2));
            for (int i=0; i<split-2; i++) {
                startList.Add(1);
            }
            startList.Add(0);

            return GetAllPerm(startList, 0, 1, true);

        }

        private long GetAllPerm(List<int> spaceList, int subtract, int add, bool isFirst) {

            long sum = 0;
            
            if (IsMatch(spaceList))
                sum++;

            while (spaceList[subtract] > 1 || (isFirst && spaceList[subtract] > 0)) {
                spaceList[subtract]--;
                spaceList[add]++;

                if (add < spaceList.Count-1)
                    sum += GetAllPerm(spaceList.ToList(), subtract+1, add+1, false);
                else {                    
                    if (IsMatch(spaceList))
                        sum++;
                }
            }

            return sum;
        }

        private bool IsMatch(List<int> spaceList) {

            var sb = new StringBuilder();
            for (int i=0; i<spaceList.Count-1; i++) {
                sb.Append('.', spaceList[i]);
                sb.Append('#', numberList[i]);
            }
            sb.Append('.', spaceList[spaceList.Count-1]);

            var teststring = sb.ToString();

            for (int i=0; i<pattern.Length; i++) {
                if ((pattern[i] == '.' && teststring[i] == '#') ||
                    (pattern[i] == '#' && teststring[i] == '.')) {
                        return false;
                    }
            }

            return true;
        }
    }
}
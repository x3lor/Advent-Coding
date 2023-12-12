public class Solution_12_2_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting...");

    
        var sum = 0;

        var counter = 0;
        
        foreach(var line in Input_12_23.example.Split('\n')) {

            Console.WriteLine($"{counter++} of 6");

            var parts = line.Split(' ');
            var pattern    = parts[0]+"?"+parts[0]+"?"+parts[0]+"?"+parts[0]+"?"+parts[0];
            var numberList = parts[1]+","+parts[1]+","+parts[1]+","+parts[1]+","+parts[1];

            // ???.### ? ???.### ? ???.### ? ???.### ? ???.### 1,1,3 , 1,1,3,1,1,3,1,1,3,1,1,3

            // ?###???????? ? ?###???????? ? ?###???????? ? ?###???????? ? ?###???????? 3,2,1

            // find the left most possible
            // find the right most possible
            // move the elements from rights to left ... to the right to find all solutions



            var numberOfQ = pattern.Count(c => c == '?'); 

            var maxBinary = (long)Math.Pow(2, numberOfQ)-1;
             
            for (long i=0; i<=maxBinary; i++) {
                var binaryString = Convert.ToString(i, 2);
                binaryString = new string('0', numberOfQ-binaryString.Length) + binaryString;

                if (IsMatch(binaryString, pattern, numberList))
                    sum++;
            }
        }
        Console.WriteLine($"Done! Sum: {sum}");
    }

    private bool IsMatch(string binaryString, string pattern, string numberList) {

        foreach(char c in binaryString) {
            var index = pattern.IndexOf('?');
            binaryString = binaryString.Remove(index, 1).Insert(index, c.ToString());
        }

        var parts = binaryString.Split('0', StringSplitOptions.RemoveEmptyEntries)
                                .Select(n => n.Length.ToString())
                                .ToList();
        
        var s = string.Join(',', parts);
        return s==numberList;
    }

    public class Game {
        private string pattern;

        public Game(string pattern) {
            this.pattern = pattern;
        }


        
    }

    private class Solution {

        private string numberList;

        public Solution(string pattern, string numberList){
            this.numberList = numberList;
            PositionsOfTheBlocks = new List<int>();
        }

        // ?###??????????###??????????###??????????###??????????###???????? 3,2,1,3,2,1,3,2,1,3,2,1,3,2,1
        // ### ## # ### ## # ### ## # ### ## # ### ## # 
        // next big block? wenn der nächste block größer ist als ich muss ich davor .. wenn kleiner, bin ich frei

        private List<int> GetLeftMostSolution() {

            return new List<int>();
        }

        public List<int> PositionsOfTheBlocks { get; }
    }
}
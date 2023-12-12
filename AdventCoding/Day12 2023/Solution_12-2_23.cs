public class Solution_12_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

    
        Console.WriteLine(GetAllPerm(new List<int>{8, 1, 1}, 0, 1));

        // var sum = 0;
        
        // foreach(var line in Input_12_23.example.Split('\n')) {

            

        //     var parts = line.Split(' ');
        //     var pattern    = parts[0]+"?"+parts[0]+"?"+parts[0]+"?"+parts[0]+"?"+parts[0];
        //     var numberList = parts[1]+","+parts[1]+","+parts[1]+","+parts[1]+","+parts[1];
        // }
        // Console.WriteLine($"Done! Sum: {sum}");


        // var sum = 0;        
        
        // foreach(var line in Input_12_23.input.Split('\n')) {

        //     var parts = line.Split(' ');
        //     var pattern = parts[0];
        //     var numberList = parts[1];
             

            
        // }
        // Console.WriteLine($"\nDone! Sum: {sum}");
    }

    public class Game {

        public Game(string pattern, string numberList) {
            
        }

    }


    private long GetAllPerm(List<int> three, int subtract, int add) {

        long sum = 1;
        //Console.WriteLine($"{Counter++}: " + string.Join('+', three));

        while (three[subtract] > 1) {
            three[subtract]--;
            three[add]++;

            if (add < three.Count-1)
                sum += GetAllPerm(three.ToList(), subtract+1, add+1);
            else {
                sum++;// Console.WriteLine($"{Counter++}: " + string.Join('+', three));
            }
        }

        return sum;
    }
}
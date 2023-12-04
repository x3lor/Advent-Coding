public class Solution_4_2_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        
        var games = new List<Game>();
        

        foreach(var line in Input_4_23.input.Split('\n')) {

                var gameNumber = int.Parse(line.Substring(5, 3))-1;

                var firstNumbers = line.Substring(10, 29)
                                       .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(part => int.Parse(part))
                                       .ToList();

                var secondNumbers = line.Substring(42)
                                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                        .Select(part => int.Parse(part))
                                        .ToList();   

                var matches = firstNumbers.Intersect(secondNumbers).Count();   
                            
                games.Add(new Game {
                    Number = gameNumber,
                    Matches = matches
                });
        }

        var stack = new Stack<int>();
        for (int i=games.Count-1; i>=0; i--) {
            stack.Push(i);
        }

        var sum = 0;

        while (stack.Count > 0) {
            var gameNumber = stack.Pop();
            var matches = games[gameNumber].Matches;
            sum++;

            for (int i=gameNumber+1; i<gameNumber+1+matches; i++) {
                stack.Push(i);
            }
        }        

        Console.WriteLine($"done! Sum: {sum}");
    }

    public class Game {
        public int Number {get; set;}
        public int Matches {get; set;}
    }
}
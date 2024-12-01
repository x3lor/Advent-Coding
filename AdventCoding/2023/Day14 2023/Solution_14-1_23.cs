public class Solution_14_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var grid = Input_14_23.input.Split('\n').ToList();        

        for (int i=1; i<grid.Count; i++) {

            var roundRockIndecies = grid[i].Select((b, i) => b.Equals('O') ? i : -1)
                                           .Where(i => i != -1)
                                           .ToList();

            foreach (var rockindex in roundRockIndecies) {

                var newRockline = i;

                while (newRockline-1 >= 0 && grid[newRockline-1][rockindex] == '.')
                    newRockline--;

                if (newRockline != i) {
                    grid[i]=grid[i].Remove(rockindex, 1).Insert(rockindex, ".");
                    grid[newRockline]=grid[newRockline].Remove(rockindex, 1).Insert(rockindex, "O");
                }
            }
        }

        var sum = 0L;
        for (int i=0; i<grid.Count; i++) {
            var rockCount = grid[i].Where(c => c == 'O').Count();
            sum += rockCount * (grid.Count-i);
        }
       
        Console.WriteLine($"Done! Sum: {sum}");
    }

    private static void PrintGrid(List<string> grid) {
        foreach(var line in grid) 
            Console.WriteLine(line);
    }
}
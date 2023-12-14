public class Solution_14_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var grid = Input_14_23.input.Split('\n').ToList();  

        var counter = -1;
        var numbersList = new List<int>();

        var cycleDetected = false;
        var cycleStart=0;

        while (!cycleDetected) {

            counter++;

            DoNorth(grid);
            DoWest(grid);
            DoSouth(grid);
            DoEast(grid);

            var gridNr = GetGridNr(grid);
            numbersList.Add(gridNr);

            if (counter == 500) {
                cycleStart = numbersList.Count-1;
            }

            if (counter > 500) {
                if (numbersList[cycleStart] == numbersList.Last()) {
                    var cycleTry = CycleDetection(numbersList, cycleStart);

                    if (cycleTry != -1) {
                        cycleDetected = true;
                        var target2 = 1000000000;
                        var result2 = numbersList[(target2-cycleStart)%cycleTry+cycleStart-1];
                        Console.WriteLine(result2);
                    }
                }
            }
        }
    }

    private static int CycleDetection(List<int> numbersList, int start) {
        var doubleCycleLength = numbersList.Count-start;

        if (numbersList[start+(doubleCycleLength/2)] == numbersList[start]) {
            return Iscycle(numbersList, start, doubleCycleLength/2);
        } else {
            return -1;
        }
    }

    private static int Iscycle(List<int> numberlist, int index, int cycleLenth) {
        for (int i=0; i<cycleLenth; i++) {
            if (numberlist[index+i] != numberlist[index+i+cycleLenth])
                return -1;
        }
        return cycleLenth;
    }

    private static int GetGridNr(List<string> grid) {
        var sum = 0;
        for (int i=0; i<grid.Count; i++) {
            var rockCount = grid[i].Where(c => c == 'O').Count();
            sum += rockCount * (grid.Count-i);
        }
        return sum;
    }

    private static void DoNorth(List<string> grid) {
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
    }

    private static void DoSouth(List<string> grid) {
        for (int i=grid.Count-1; i>=0; i--) {

            var roundRockIndecies = grid[i].Select((b, i) => b.Equals('O') ? i : -1)
                                           .Where(i => i != -1)
                                           .ToList();

            foreach (var rockindex in roundRockIndecies) {

                var newRockline = i;

                while (newRockline+1 <= grid.Count-1 && grid[newRockline+1][rockindex] == '.')
                    newRockline++;

                if (newRockline != i) {
                    grid[i]=grid[i].Remove(rockindex, 1).Insert(rockindex, ".");
                    grid[newRockline]=grid[newRockline].Remove(rockindex, 1).Insert(rockindex, "O");
                }
            }
        }
    }

    private static void DoWest(List<string> grid) {
        for (int i=0; i<grid.Count; i++) {

             var roundRockIndecies = grid[i].Select((b, i) => b.Equals('O') ? i : -1)
                                            .Where(i => i != -1)                                    
                                            .ToList();

            foreach (var rockindex in roundRockIndecies) {

                var newRockIndex = rockindex;

                while (newRockIndex-1 >= 0 && grid[i][newRockIndex-1] == '.')
                    newRockIndex--;

                if (newRockIndex != rockindex) {
                    grid[i]=grid[i].Remove(rockindex, 1).Insert(rockindex, ".").Remove(newRockIndex, 1).Insert(newRockIndex, "O");
                }
            }
        }
    }

    private static void DoEast(List<string> grid) {
        for (int i=0; i<grid.Count; i++) {

             var roundRockIndecies = grid[i].Select((b, i) => b.Equals('O') ? i : -1)
                                            .Where(i => i != -1)
                                            .OrderByDescending(x => x)
                                            .ToList();

            foreach (var rockindex in roundRockIndecies) {

                var newRockIndex = rockindex;

                while (newRockIndex+1 < grid[i].Length && grid[i][newRockIndex+1] == '.')
                    newRockIndex++;

                if (newRockIndex != rockindex) {
                    grid[i]=grid[i].Remove(rockindex, 1).Insert(rockindex, ".").Remove(newRockIndex, 1).Insert(newRockIndex, "O");
                }
            }
        }
    }
}
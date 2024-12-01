public class Solution_9_2_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting...");

        var sum = 0;

        foreach(var line in Input_9_23.input.Split('\n')) {

            var listOfLines = new List<List<int>>();

            var currentLine = line.Split(' ')
                                  .Select(num => int.Parse(num))
                                  .ToList();

            while (!IsLineOnlyZero(currentLine)) {
                listOfLines.Add(currentLine);
                currentLine = GetNextLine(currentLine);
            }
            listOfLines.Add(currentLine);
                            
            // now build up

            listOfLines[listOfLines.Count-1].Insert(0,0);

            for (int i=listOfLines.Count-2; i>=0; i--) {
                var nextNum = listOfLines[i][0] - listOfLines[i+1][0];
                listOfLines[i].Insert(0, nextNum);
            }

            sum += listOfLines[0][0];
        }        

        Console.WriteLine($"Done ... sum: {sum}");
    }

    private List<int> GetNextLine(List<int> list) {
        var result = new List<int>();

        for (int i=0; i<list.Count-1; i++) {
            result.Add(list[i+1]-list[i]);
        }

        return result;
    }

    private bool IsLineOnlyZero(List<int> list) {
        return list.All(x => x == 0);
    }
}
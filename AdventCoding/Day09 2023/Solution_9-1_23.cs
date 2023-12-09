public class Solution_9_1_23 : ISolution
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

            listOfLines[listOfLines.Count-1].Add(0);

            for (int i=listOfLines.Count-2; i>=0; i--) {
                var nextNum = listOfLines[i+1].Last() + listOfLines[i].Last();
                listOfLines[i].Add(nextNum);
            }

            sum += listOfLines[0].Last();
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
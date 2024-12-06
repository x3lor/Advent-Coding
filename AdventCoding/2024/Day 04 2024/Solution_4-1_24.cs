using System.Text;
using System.Text.RegularExpressions;

public class Solution_4_1_24 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_4_24.input.Split('\n').ToList();

        var resultCounter = 0;
        resultCounter += CountList(input);
        resultCounter += CountList(Transpose(input));
        resultCounter += CountList(DiagonalsFromLeftTop(input));
        resultCounter += CountList(DialonalsFromRightTop(input));

        Console.WriteLine($"done! Sum: {resultCounter}");
    }

    public int CountList(List<string> list) {
        return list.Select(Count).Sum();
    }

    public int Count(string s) {
        return Regex.Matches(s, "XMAS").Count + Regex.Matches(s, "SAMX").Count;
    }

    public List<string> Transpose (List<string> list) {

        var width = list[0].Length;
        var height = list.Count;

        var resultList = new List<string>();

        for (int x=0; x<width; x++) {
            var sb = new StringBuilder();
            for (int y=0; y<height; y++) {
                sb.Append(list[y][x]);
            }
            resultList.Add(sb.ToString());
        }

        return resultList;
    }

    public List<string> DiagonalsFromLeftTop (List<string> list) {
        var width = list[0].Length;
        var height = list.Count;

        var resultList = new List<string>();

        for (int x=-width+1; x<width; x++) {
            
            var sb = new StringBuilder();
            
            for (int y=0; y<height; y++) {
                if (x+y >= 0 && x+y < width)
                    sb.Append(list[y][x+y]);
            }

            resultList.Add(sb.ToString());
        }

        return resultList;
    }

    public List<string> DialonalsFromRightTop (List<string> list) {
        var width = list[0].Length;
        var height = list.Count;

        var resultList = new List<string>();

        for (int x=(width-1)*2; x>=0; x--) {
            
            var sb = new StringBuilder();
            
            for (int y=0; y<height; y++) {

                var xCoord = x-y;
                var yCoord = y;

                if (xCoord >= 0 && xCoord < width)
                    sb.Append(list[y][x-y]);
            }

            resultList.Add(sb.ToString());
        }

        return resultList;
    }
}


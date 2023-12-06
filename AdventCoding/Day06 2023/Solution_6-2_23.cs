using System.Diagnostics.CodeAnalysis;

public class Solution_6_2_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting...");

        var lines = Input_6_23.input2.Split('\n').ToList();
        var time     = long.Parse(lines[0]);
        var distance = long.Parse(lines[1]);

        var counter = 0L;
        for (long timetries=1; timetries<time; timetries++) {
            if ((time-timetries)*timetries > distance)
                counter++;
        }

        Console.WriteLine($"Done: {counter}");
    }   
   
}
using System.Diagnostics.CodeAnalysis;

public class Solution_6_1_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting...");

        var lines = Input_6_23.input.Split('\n').ToList();
        var times     = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(num => int.Parse(num)).ToList();
        var distances = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(num => int.Parse(num)).ToList();

        var sum = 1;

        for (int i=0; i<times.Count; i++) {
            var time = times[i];
            var distance = distances[i];

            var counter = 0;
            for (var timetries=1; timetries<time; timetries++) {
                if ((time-timetries)*timetries > distance)
                    counter++;
            }

            sum *= counter;
        }
        
        Console.WriteLine($"Done: {sum}");
    }   
   
}
#pragma warning disable CS8602

using System.Drawing;
using System.Numerics;

public class Solution_24_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var functions = Input_24_23.example
                                   .Split('\n')
                                   .Select(line => new Function(line))
                                   .ToList();
        
        var testAreaMin = 7L;    //200000000000000L;
        var testAreaMax = 27L;   //400000000000000L

        var sum = 0;

        for (int i=0; i<functions.Count; i++) {
            for (int j=i+1; j<functions.Count; j++) {
                if (IntersectionWithinTestarea(functions[i], functions[j], testAreaMin, testAreaMax))
                    sum++;
            }
        }
        
        Console.WriteLine($"Done! sum: {sum}");
    }

    private static bool IntersectionWithinTestarea(Function f1, Function f2, long testAreaMin, long testAreaMax) {

        var f1Origin = new Vector2(f1.Origin.X, f1.Origin.Y);
        var f2Origin = new Vector2(f2.Origin.X, f2.Origin.Y);
        var f1Direction = new Vector2(f1.Velocity.X, f1.Velocity.Y);
        var f2Direction = new Vector2(f2.Velocity.X, f2.Velocity.Y);

        //f1DirectionNorm = System.Numerics.Normalize(f1Direction);
        
        
        return false;
    }

    private static bool DEquals(double d1, double d2) {
        const double equalDiff = 0.000000001;
        return Math.Abs(d1-d2) < equalDiff;
    }

    public class Function {

    
        public Function(string init) {

            var charAr = new char[2];
            charAr[0] = ',';
            charAr[1] = '@';

            var numbers = init.Split(charAr, StringSplitOptions.RemoveEmptyEntries)
                              .Select(num => long.Parse(num))
                              .ToList();
     
            Origin = new Vector3(numbers[0], numbers[1], numbers[2]);
            Velocity = new Vector3(numbers[3], numbers[4], numbers[5]);
        }

        public Vector3 Origin { get; }
        public Vector3 Velocity { get; }
    }
}
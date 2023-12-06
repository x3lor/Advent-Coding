using System.Text;

public class Solution_15_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_15.input;
        var coordMax = 4000000;
        var rhombs = new List<Rhomb>();

        foreach(var line in input.Split('\n')) {
            
            var parts = line.Split(' ');

            var sensorX = long.Parse(parts[2].Substring(2,parts[2].Length-3));
            var sensorY = long.Parse(parts[3].Substring(2,parts[3].Length-3));
            var beaconX = long.Parse(parts[8].Substring(2,parts[8].Length-3));
            var beaconY = long.Parse(parts[9].Substring(2,parts[9].Length-2));

            rhombs.Add(new Rhomb(new Coord() {X=sensorX, Y=sensorY},
                                 new Coord() {X=beaconX, Y=beaconY}));
        }

        var overallLenght = coordMax;
        var inputSquareList = new List<Square>() { new Square(new Coord(), overallLenght) };
        var resultSquareList = new List<Square> ();
        
        while (true) {
            foreach(var square in inputSquareList) {
                var subsquares = CreateSqares(square.P1, overallLenght, (overallLenght > 100) ? 100 : 4);
                foreach(var subSquare in subsquares) {
                    if (!rhombs.Any(r => IsSquareFullyCoveredByRhomb(subSquare, r))) {
                        resultSquareList.Add(subSquare);
                    }
                }
            }

            if (resultSquareList.Count == 1)
                break;

            inputSquareList = resultSquareList;
            resultSquareList = new List<Square>();
            overallLenght /= 100;
        }

        var result = resultSquareList.First();
        Console.WriteLine($"DONE! Frequency: {result.P1.X*4000000+result.P1.Y}");
    }  

    private List<Square> CreateSqares(Coord origin, long overallLenght, int divideFactor) {
        var resultList = new List<Square>();

        var lengthOfResultSquare = overallLenght / divideFactor;

        for (long x=origin.X; x<origin.X+overallLenght; x+=lengthOfResultSquare) {
            for (long y=origin.Y; y<origin.Y+overallLenght; y+=lengthOfResultSquare) {
                resultList.Add(new Square(new Coord() {X=x, Y=y}, lengthOfResultSquare-1));
            }
        }

        return resultList;
    }

    private static bool IsSquareFullyCoveredByRhomb(Square s, Rhomb r) {
        return IsPointWithinRhomb(s.P1, r) &&
               IsPointWithinRhomb(s.P2, r) &&
               IsPointWithinRhomb(s.P3, r) &&
               IsPointWithinRhomb(s.P4, r);
    }

    private static bool IsPointWithinRhomb(Coord c, Rhomb r) {
        return GetManhattenDistance(c, r.Center) <= r.Distance;
    }

    public static long GetManhattenDistance(Coord a, Coord b) {
        return Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y);
    } 
    
    public class Coord {
        public long X { get; set; }
        public long Y { get; set; }
    }

    private class Square {

        public Square(Coord p1, long side) {
            P1 = p1;
            P2 = new Coord() {X=p1.X+side, Y=p1.Y };
            P3 = new Coord() {X=p1.X,      Y=p1.Y+side };
            P4 = new Coord() {X=p1.X+side, Y=p1.Y+side };
        }

        public Coord P1 { get; }
        public Coord P2 { get; }
        public Coord P3 { get; }
        public Coord P4 { get; }
    }

    private class Rhomb {

        public Rhomb(Coord sensor, Coord beacon) {
            Center = sensor;
            Distance = Solution_15_2.GetManhattenDistance(sensor, beacon);
        }

        public Coord Center { get; }
        public long Distance { get; }
    }
}
using System.Text;

public class Solution_15_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_15.input;
        var coordMax = 4000000;

        //var input = Input_15.inputExample;
        //var coordMax = 20;
        
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

        var squares = CreateSqares(new Coord() {X=0, Y=0}, coordMax, 100);

        var notFullyCoveredSquares = new List<Square>();

        foreach(var square in squares) {

            
            var anyRhombCoversThisSquare = rhombs.Any(r => IsSquareFullyCoveredByRaute(square, r));

            if (!anyRhombCoversThisSquare) {
                notFullyCoveredSquares.Add(square);
            }

        }

        var notFullyCoveredSquares2 = new List<Square>();

        foreach(var square in notFullyCoveredSquares) {

            var subsquares = CreateSqares(square.P1, 40000, 100);

            foreach(var subSquare in subsquares) {

                var anyRhombCoversThisSquare = rhombs.Any(r => IsSquareFullyCoveredByRaute(subSquare, r));

                if (!anyRhombCoversThisSquare) {
                    notFullyCoveredSquares2.Add(subSquare);
                }
            }

        }

        var notFullyCoveredSquares3 = new List<Square>();

        foreach(var square in notFullyCoveredSquares2) {

            var subsquares = CreateSqares(square.P1, 400, 100);

            foreach(var subSquare in subsquares) {

                var anyRhombCoversThisSquare = rhombs.Any(r => IsSquareFullyCoveredByRaute(subSquare, r));

                if (!anyRhombCoversThisSquare) {
                    notFullyCoveredSquares3.Add(subSquare);
                }
            }
        }

        var notFullyCoveredSquares4 = new List<Square>();

        foreach(var square in notFullyCoveredSquares3) {

            var subsquares = CreateSqares(square.P1, 4, 4);

            foreach(var subSquare in subsquares) {

                var anyRhombCoversThisSquare = rhombs.Any(r => IsSquareFullyCoveredByRaute(subSquare, r));

                if (!anyRhombCoversThisSquare) {
                    notFullyCoveredSquares4.Add(subSquare);
                }
            }
        }

        var result = notFullyCoveredSquares4.First();

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

    private bool IsSquareFullyCoveredByRaute(Square s, Rhomb r) {
        return IsPointWithinRhomb(s.P1, r) &&
               IsPointWithinRhomb(s.P2, r) &&
               IsPointWithinRhomb(s.P3, r) &&
               IsPointWithinRhomb(s.P4, r);
    }

    private bool IsPointWithinRhomb(Coord c, Rhomb r) {
        var dist = GetManhattenDistance(c, r.Center);
        return dist <= r.Distance;
    }

    private long GetManhattenDistance(Coord a, Coord b) {
        return Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y);
    } 
    

    private class Coord {
        public long X { get; set; }
        public long Y { get; set; }

        public override string ToString()
        {
            return $"({X}|{Y})";
        }
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
            var distance = GetManhattenDistance(sensor, beacon);

            // P1 = new Coord() { X=sensor.X-distance, Y=sensor.Y          };
            // P2 = new Coord() { X=sensor.X,          Y=sensor.Y-distance };
            // P3 = new Coord() { X=sensor.X+distance, Y=sensor.Y          };
            // P4 = new Coord() { X=sensor.X,          Y=sensor.Y+distance };

            Center = sensor;
            Distance = distance;
        }

        public Coord Center { get; }
        public long Distance { get; }

        // public Coord P1 { get; }
        // public Coord P2 { get; }
        // public Coord P3 { get; }
        // public Coord P4 { get; }

        private long GetManhattenDistance(Coord a, Coord b) {
            return Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y);
        } 
    }
   
}
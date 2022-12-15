using System.Text;

public class Solution_15_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_15.input;
        var coordMax = 4000000;

        //  var input = Input_15.inputExample;
        //  var coordMax = 20;
        
        var sets = new List<Set>();

        foreach(var line in input.Split('\n')) {
            
            var parts = line.Split(' ');

            var sensorX = long.Parse(parts[2].Substring(2,parts[2].Length-3));
            var sensorY = long.Parse(parts[3].Substring(2,parts[3].Length-3));
            var beaconX = long.Parse(parts[8].Substring(2,parts[8].Length-3));
            var beaconY = long.Parse(parts[9].Substring(2,parts[9].Length-2));

            sets.Add(new Set(new Coord() {X=sensorX, Y=sensorY},
                             new Coord() {X=beaconX, Y=beaconY}));
        }

        var current = new Coord();

        for (long x=0; x<=coordMax; x++) {

            
            Console.Write($"{x}/{coordMax}");

            current.X=x;

            for (long y=0; y<coordMax; y++) {

                current.Y=y;

                var found = false;
                foreach(var set in sets) {

                    if (GetManhattenDistance(current, set.Sensor) <= set.Distance) {
                        found = true;
                    }
                }

                if (!found)
                { 
                    Console.WriteLine($"Frequency: {x*4000000+y}");
                    goto outL;
                }
            }
        }

        outL:
        Console.WriteLine($"DONE!");
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

    private class Set {
        public Set(Coord sensor, Coord beacon) {
            Sensor = sensor;
            Beacon = beacon;
            Distance = GetManhattenDistance(sensor, beacon);
        }

        private long GetManhattenDistance(Coord a, Coord b) {
            return Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y);
        } 

        public Coord Sensor { get; }
        public Coord Beacon { get; }

        public long Distance { get; }

        public override string ToString()
        {
            return $"Sensor: {Sensor.ToString()}; Beacon: {Beacon.ToString()}";
        }
    }
   
}
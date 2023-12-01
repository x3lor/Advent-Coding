public class Solution_15_1 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_15.input;
        var importantLine = 2000000;

        // for trying the example:
        // var input = Input_15.inputExample;
        // var importantLine = 10;
        
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

        var listOfCoveredCoordinates = new HashSet<long>();

        foreach(var set in sets) {
            var distanceSensorToBeacon = GetManhattenDistance(set.Sensor, set.Beacon);
            if (SensorIsInRelevantDistance(set.Sensor, distanceSensorToBeacon, importantLine)) {
                var currentPos = new Coord() {X=0, Y=importantLine};
                for (long x=set.Sensor.X-distanceSensorToBeacon; x<=set.Sensor.X+distanceSensorToBeacon; x++) {
                        currentPos.X = x;
                        if (GetManhattenDistance(set.Sensor, currentPos) <= distanceSensorToBeacon) {
                            if (NoBeaconAtCoord(sets, currentPos))                                
                                listOfCoveredCoordinates.Add(currentPos.X);
                    }
                }
            } 
        }

        Console.WriteLine($"DONE! NumberOfCoveredSpots in Line {importantLine}: {listOfCoveredCoordinates.Count}");
    }  

    private bool SensorIsInRelevantDistance(Coord sensor, long distanceSensorToBeacon, long importantLine) {
        return sensor.Y-distanceSensorToBeacon < importantLine && sensor.Y+distanceSensorToBeacon > importantLine;
    }

    private bool NoBeaconAtCoord(List<Set> sets, Coord a) {
        return !sets.Any(c => c.Beacon.X == a.X && c.Beacon.Y == a.Y);
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
        }

        public Coord Sensor { get; }
        public Coord Beacon { get; }
    }
}
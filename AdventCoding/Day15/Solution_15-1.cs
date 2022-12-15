using System.Text;

public class Solution_15_1 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting ... ");

        var input = Input_15.input;

        var sets = new List<Set>();
        
        // var minX = long.MaxValue;
        // var minY = long.MaxValue;
        // var maxX = 0L;
        // var maxY = 0L;

        foreach(var line in input.Split('\n')) {
            
            // Example: Sensor at x=1112863, y=496787: closest beacon is at x=1020600, y=2000000
            var parts = line.Split(' ');

            var i1 = parts[2].Substring(2,parts[2].Length-3);
            var i2 = parts[3].Substring(2,parts[3].Length-3);
            var sensorX = long.Parse(i1);
            var sensorY = long.Parse(i2);

            // if (sensorX<minX) minX = sensorX;
            // if (sensorX>maxX) maxX = sensorX;
            // if (sensorY<minY) minY = sensorY;
            // if (sensorY>maxY) maxY = sensorY;

            var i3 = parts[8].Substring(2,parts[8].Length-3);
            var i4 = parts[9].Substring(2,parts[9].Length-2);
            var beaconX = long.Parse(i3);
            var beaconY = long.Parse(i4);

            // if (beaconX<minX) minX = beaconX;
            // if (beaconX>maxX) maxX = beaconX;
            // if (beaconY<minY) minY = beaconY;
            // if (beaconY>maxY) maxY = beaconY;

            sets.Add(new Set(new Coord() {X=sensorX, Y=sensorY},
                             new Coord() {X=beaconX, Y=beaconY}));
        }

        // var gridExtention = Math.Max(maxX-minX, maxY-minY);

        // var gridWidth = maxX-minX+1+(2*gridExtention);
        // var gridHeight = maxY-minY+1+(2*gridExtention);

        // Console.WriteLine($"gridHeight: {gridHeight}");
        // Console.WriteLine($"gridWidth:  {gridWidth}");

        // setting all koordinates new grid-coord

        // foreach(var set in sets) {
        //     set.Sensor.X = set.Sensor.X - minX + gridExtention;
        //     set.Sensor.Y = set.Sensor.Y - minY + gridExtention;
        //     set.Beacon.X = set.Beacon.X - minX + gridExtention;
        //     set.Beacon.Y = set.Beacon.Y - minY + gridExtention;
        // }

        // create & init grid
        //var grid = new char[gridWidth, gridHeight];

        // for(int x=0; x<gridWidth; x++) {
        //     for (int y=0; y<gridHeight; y++) {
        //         grid[x,y] = '.';
        //     }
        // }

        // put sets into the grid

        // foreach(var set in sets) {
        //     grid[set.Sensor.X, set.Sensor.Y] = 'S';
        //     grid[set.Beacon.X, set.Beacon.Y] = 'B';
        // }

        //PrintGrid(grid, gridWidth, gridHeight);

        // draw NO-areas into the grid

        var listOfNos = new List<Coord>();
        
        //var importantLine = 10;
        var importantLine = 2000000;

        var counter = 1;

        foreach(var set in sets.Take(1)) {

            Console.Write($"{counter++}/40; ");

            var dis = GetManhattenDistance(set.Sensor, set.Beacon);

            Console.WriteLine($"Need {(set.Sensor.X+dis)-(set.Sensor.X-dis)} iterations");
            if (set.Sensor.Y-dis < importantLine && set.Sensor.Y+dis > importantLine) {

                for (long x=set.Sensor.X-dis; x<=set.Sensor.X+dis; x++) {

                        if (x%100000 == 0)
                            Console.Write("x");

                        var currentPos = new Coord() {X=x, Y=importantLine};
                        if (GetManhattenDistance(set.Sensor, currentPos) <= dis) {
                            if (!IsThereAnB(sets, currentPos))
                                if (!IsItAlreadyInTheList(listOfNos, currentPos))
                                    listOfNos.Add(currentPos);
                    }
                }
            } 
        }

        //PrintGrid(grid, gridWidth, gridHeight);

        // var noCounter = 0;
        // for (int x=0; x<gridWidth; x++) {
        //     if (grid[x, 10-minY+gridExtention] == '#')
        //         noCounter++;
        // }

        Console.WriteLine(listOfNos.Count);
    }  

    private bool IsItAlreadyInTheList(List<Coord> coords, Coord c) {
        return coords.Any(k => k.X == c.X && k.Y == c.Y);
    }

    private bool IsThereAnB(List<Set> sets, Coord a) {
        return sets.Any(c => c.Beacon.X == a.X && c.Beacon.Y == a.Y);
    }

    private long GetManhattenDistance(Coord a, Coord b) {
        return Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y);
    } 

    // private void PrintGrid(char[,]grid, long gridWidth, long gridHeight) {
        
    //     Console.WriteLine("\n");

    //     for(int y=0; y<gridHeight; y++) {
    //         var sb = new StringBuilder();
    //         for (int x=0; x<gridWidth; x++) {
    //             sb.Append(grid[x,y]);
    //         }
    //         Console.WriteLine(sb.ToString());
    //     }
    // } 

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

        public override string ToString()
        {
            return $"Sensor: {Sensor.ToString()}; Beacon: {Beacon.ToString()}";
        }
    }
   
}
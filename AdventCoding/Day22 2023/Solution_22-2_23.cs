#pragma warning disable CS8602

using System.Drawing;

public class Solution_22_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        int cubeId = 0;

        var cubes = Input_22_23.input
                               .Split('\n')
                               .Select(line => new Cube(line, cubeId++))
                               .OrderBy(cube => cube.Origin.Z)
                               .ToList();

        var cubesDict = cubes.ToDictionary(c => c.ID, c=>c);
        
        foreach(var cube in cubes) {
            MoveCubeDownAsMuchAsPossible(cube, cubes);
        }

        var listOfBricksForExamination = new List<Cube>();

        int sum=0;
        foreach(var cube in cubes) {
            if (!cube.CanOthersMoveWithoutMe(cubes))
                sum++;
            else
                listOfBricksForExamination.Add(cube);
        }    

        var finalSum = 0;
        foreach(var cube in listOfBricksForExamination) {
            finalSum += cube.HowManyWouldFallWithoutMe(cubes, cubesDict);
        }

        Console.WriteLine($"Done! sum: {finalSum}");
    }

    private static void MoveCubeDownAsMuchAsPossible(Cube c, List<Cube> allCubes) {
        
        while (c.Origin.Z > 0 && !c.Intersect(allCubes)) {            
            c.Origin.Z--;
        }

        c.Origin.Z++;
    }

    // 2,7,26~2,7,26
    public class Cube {
        public Cube (string init, int id) {
            var parts = init.Split('~');
            Origin = new Coordinate(parts[0]);
            var extend = new Coordinate(parts[1]);

            DimX = extend.X-Origin.X+1;
            DimY = extend.Y-Origin.Y+1;
            DimZ = extend.Z-Origin.Z+1;

            ID = id;
        }

        public Cube(Cube c) {
            ID = c.ID;
            Origin = new Coordinate(c.Origin.X, c.Origin.Y, c.Origin.Z);
            DimX=c.DimX;
            DimY=c.DimY;
            DimZ=c.DimZ;
        }

        public int ID { get; }
        public Coordinate Origin { get; }
        public int DimX { get; }
        public int DimY { get; }
        public int DimZ { get; }


        public List<Coordinate> GetAllCubes() {

            if (DimX>1) return Enumerable.Range(Origin.X, DimX).Select(x => new Coordinate(x,        Origin.Y, Origin.Z)).ToList();
            if (DimY>1) return Enumerable.Range(Origin.Y, DimY).Select(y => new Coordinate(Origin.X, y,        Origin.Z)).ToList();
            if (DimZ>1) return Enumerable.Range(Origin.Z, DimZ).Select(z => new Coordinate(Origin.X, Origin.Y, z       )).ToList();

            return new List<Coordinate> { Origin };
        }

        public bool Intersect(Cube other) {
            if (ReferenceEquals(other, this))
                return false;

            return GetAllCubes().Intersect(other.GetAllCubes()).Count() > 0;
        }

        public bool Intersect(List<Cube> others) {
            return others.Where(CubesIsInRage).Any(Intersect);
        }

        public bool Intersect(List<Cube> others, Cube ignore) {
            return others.Where(c => CubesIsInRage(c) && !ReferenceEquals(c, ignore)).Any(Intersect);
        }

        public bool CanOthersMoveWithoutMe(List<Cube> others) {
            
            foreach(var cube in others) {
                if (ReferenceEquals(cube, this))
                    continue;

                cube.Origin.Z--;
                if (cube.Origin.Z == 0) {
                    cube.Origin.Z++;
                    continue;
                }
                if (!cube.Intersect(others, this)) {
                    cube.Origin.Z++;
                    return true;
                }
                cube.Origin.Z++;
            }
            return false;
        }

        public int HowManyWouldFallWithoutMe(List<Cube> others, Dictionary<int, Cube> cubesDict) {

            var copy = others.Where(c => c != this)
                             .Select(c => new Cube(c))
                             .ToList();

            foreach(var cube in copy) {
                while (cube.Origin.Z > 0 && !cube.Intersect(copy)) {            
                    cube.Origin.Z--;
                }
                cube.Origin.Z++;
            }

            var sum = 0;
            foreach (var cube in copy) {
                if (cube.Origin.Z != cubesDict[cube.ID].Origin.Z)
                    sum++;
            }

            return sum;
        }

        public bool CubesIsInRage(Cube other) { 

            if (other.Origin.Z+other.DimZ < Origin.Z) return false;
            if (other.Origin.Z > Origin.Z+DimZ) return false;

            return true;
        }

        public override string ToString()
        {
            return Origin.ToString();
        }
    }

    public class Coordinate {

        public Coordinate(int x, int y, int z) {
            X=x;
            Y=y;
            Z=z;
        }

        public Coordinate(string init) {
            var parts = init.Split(',');
            X = int.Parse(parts[0]);
            Y = int.Parse(parts[1]);
            Z = int.Parse(parts[2]);
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Coordinate) return false;
            var point = obj as Coordinate;
            return point.X == X && point.Y == Y && point.Z == Z;
        }

        public static bool operator ==(Coordinate obj1, Coordinate obj2)
        {
            if (ReferenceEquals(obj1, obj2)) return true;
            if (obj1 is null) return false;
            if (obj2 is null) return false;
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Coordinate obj1, Coordinate obj2) => !(obj1 == obj2);

        public override int GetHashCode()
        {
            return X.GetHashCode()^Y.GetHashCode()^Z.GetHashCode();
        }

        public override string ToString()
        {
            return $"({X}|{Y}|{Z})";
        }
    }
}
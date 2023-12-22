#pragma warning disable CS8602

public class Solution_22_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var cubes = Input_22_23.input
                               .Split('\n')
                               .Select(line => new Cube(line))
                               .OrderBy(cube => cube.Origin.Z)
                               .ToList();
        
        foreach(var cube in cubes) {
            cube.MoveDownAsMuchAsPossible(cubes);
        }

        int sum=0;
        foreach(var cube in cubes) {
            if (!cube.CanOthersMoveWithoutMe(cubes))
                sum++;
        }    

        Console.WriteLine($"Done! sum: {sum}");
    }

    public class Cube {
        public Cube (string init) {
            var parts = init.Split('~');
            Origin = new Coordinate(parts[0]);
            var extend = new Coordinate(parts[1]);

            DimX = extend.X-Origin.X+1;
            DimY = extend.Y-Origin.Y+1;
            DimZ = extend.Z-Origin.Z+1;
        }

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

        public void MoveDownAsMuchAsPossible(List<Cube> allCubes) {
            while (Origin.Z > 0 && !Intersect(allCubes)) {            
                Origin.Z--;
            }
            Origin.Z++;
        }

        public bool Intersect(Cube other) {
            if (ReferenceEquals(other, this))
                return false;

            return GetAllCubes().Intersect(other.GetAllCubes()).Any();
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
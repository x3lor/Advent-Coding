public class Solution_12_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_12.input.Split('\n');

        var rows    = input.Length;
        var columns = input[0].Length;

        var map = new Position[rows, columns];

        for (int r=0; r<rows; r++) {
            for (int c=0; c<columns; c++) {
                var ch = input[r].Substring(c, 1).ElementAt(0);
                map[r, c] = new Position {
                    Height = CharToHeight(ch),
                    IsStart = ch == 'S',
                    IsGoal = ch == 'E'
                };
            }
        }

        for (int r=0; r<rows; r++) {
            for (int c=0; c<columns; c++) {
                map[r, c].Left   = GetLeft  (map, r, c);
                map[r, c].Top    = GetTop   (map, r, c);
                map[r, c].Bottom = GetBottom(map, r, c, rows);
                map[r, c].Right  = GetRight (map, r, c, columns);
            }
        }

        var allA = map.Cast<Position>().Where(p => p.Height == 0);
        int min = 408;

        foreach (var p in allA) {

            foreach(var pos in map.Cast<Position>()) {
                pos.StepsToGetHere = -1;
            }

            p.StepsToGetHere = 0;
            p.SearchAndSet();

            var steps = map.Cast<Position>().First(p => p.IsGoal).StepsToGetHere;

            if (steps != -1 && steps < min)
                min = steps;
        }

       
        Console.WriteLine($"done! Min: {min}");
    }    

    private Position? GetLeft  (Position [,] map, int x, int y)                { return ((x>0)           && map[x-1, y].Height-map[x, y].Height < 2) ? map[x-1, y] : null; }
    private Position? GetTop   (Position [,] map, int x, int y)                { return ((y>0)           && map[x, y-1].Height-map[x, y].Height < 2) ? map[x, y-1] : null; }
    private Position? GetBottom(Position [,] map, int x, int y, int mapHeight) { return ((x<mapHeight-1) && map[x+1, y].Height-map[x, y].Height < 2) ? map[x+1, y] : null; }
    private Position? GetRight (Position [,] map, int x, int y, int mapWidth ) { return ((y<mapWidth-1)  && map[x, y+1].Height-map[x, y].Height < 2) ? map[x, y+1] : null; }

    private int CharToHeight(char c) {
        if (c >= 'a' && c <= 'z')
            return ((int)c)-((int)'a');

        if (c == 'S')
            return CharToHeight('a');
        
        if (c == 'E') {
            return CharToHeight('z');
        }

        throw new ArgumentException("sollte nicht passieren");
    }

    public class Position {

        public bool IsStart { get; set; }
        public bool IsGoal { get; set; }

        public int Height { get; set; }

        public Position? Left   { get; set; }
        public Position? Top    { get; set; }
        public Position? Right  { get; set; }
        public Position? Bottom { get; set; }

        public int StepsToGetHere { get; set; } = -1;

        public void SearchAndSet() {
            if (Left != null) {
                if ((Left.StepsToGetHere == -1) || (Left.StepsToGetHere > StepsToGetHere+1)) {
                    Left.StepsToGetHere = StepsToGetHere+1;
                    Left.SearchAndSet();
                } 
            }

            if (Top != null) {
                if ((Top.StepsToGetHere == -1) || (Top.StepsToGetHere > StepsToGetHere+1)) {
                    Top.StepsToGetHere = StepsToGetHere+1;
                    Top.SearchAndSet();
                } 
            }

            if (Right != null) {
                if ((Right.StepsToGetHere == -1) || (Right.StepsToGetHere > StepsToGetHere+1)) {
                    Right.StepsToGetHere = StepsToGetHere+1;
                    Right.SearchAndSet();
                } 
            }

            if (Bottom != null) {
                if ((Bottom.StepsToGetHere == -1) || (Bottom.StepsToGetHere > StepsToGetHere+1)) {
                    Bottom.StepsToGetHere = StepsToGetHere+1;
                    Bottom.SearchAndSet();
                } 
            }
        }

        public int ShortestWayToA() {

            if (Height == 0)
                return StepsToGetHere;

            var directions = new List<Position?> {Left, Top, Bottom, Right};
            var c = directions.Where(a => a != null)
                              .Cast<Position>()
                              .OrderBy(p => p.StepsToGetHere)
                              .First();

            return c.ShortestWayToA();
        }
    }
}
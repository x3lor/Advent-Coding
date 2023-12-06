public class Solution_12_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_12.input.Split('\n');

        var rows_x    = input.Length;
        var columns_y = input[0].Length;

        // Initializing the 2D-Array 
        var map = new Position[rows_x, columns_y];
        for (int x=0; x<rows_x; x++) {
            for (int y=0; y<columns_y; y++) {
                map[x, y] = new Position(input[x].Substring(y, 1).ElementAt(0));
            }
        }

        // Creating the Graph with all possible connections
        for (int x=0; x<rows_x; x++) {
            for (int y=0; y<columns_y; y++) {
                map[x, y].Left   = ((x>0)           && (map[x-1, y].Height-map[x, y].Height < 2)) ? map[x-1, y] : null;
                map[x, y].Top    = ((y>0)           && (map[x, y-1].Height-map[x, y].Height < 2)) ? map[x, y-1] : null;
                map[x, y].Bottom = ((x<rows_x-1)    && (map[x+1, y].Height-map[x, y].Height < 2)) ? map[x+1, y] : null;
                map[x, y].Right  = ((y<columns_y-1) && (map[x, y+1].Height-map[x, y].Height < 2)) ? map[x, y+1] : null;;
            }
        }

        // Find the shortest way from all a to the goal
        int min = 408;

        var allA = map.Cast<Position>().Where(p => p.Height == 0);
        foreach (var posA in allA) {

            // reset all nodes
            foreach(var node in map.Cast<Position>()) {
                node.StepsToGetHere = -1;
            }

            // search from the selected a-node
            posA.StepsToGetHere = 0;
            posA.SearchAndSet();

            var steps = map.Cast<Position>()
                           .First(p => p.IsGoal)
                           .StepsToGetHere;

            if (steps != -1 && steps < min)
                min = steps;
        }

        Console.WriteLine($"done! Min: {min}");
    }    

    public class Position {

        public Position(char c) {
            Height  = CharToHeight(c);
            IsGoal  = c == 'E';
        }

        private int CharToHeight(char c) =>
        c switch
        {
            'S' => CharToHeight('a'),
            'E' => CharToHeight('z'),
            (>= 'a') and (<= 'z') => ((int)c)-((int)'a'),
            _ => throw new ArgumentException("sollte nicht passieren")
        };

        public bool IsGoal { get; }
        public int  Height { get; }

        public Position? Left   { get; set; }
        public Position? Top    { get; set; }
        public Position? Right  { get; set; }
        public Position? Bottom { get; set; }

        public int StepsToGetHere { get; set; } = -1;

        public void SearchAndSet() {
            var adjacentNodes = new List<Position?> { Left, Right, Top, Bottom };

            foreach(var node in adjacentNodes.Where(n => n != null).Cast<Position>()) {

                if ((node.StepsToGetHere == -1) || (node.StepsToGetHere > StepsToGetHere+1)) {
                    node.StepsToGetHere = StepsToGetHere+1;
                    node.SearchAndSet();
                } 
            }
        }
    }
}
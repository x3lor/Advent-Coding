public class Solution_12_1 : ISolution
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
                map[x, y].Left   = ((x>0)           && map[x-1, y].Height-map[x, y].Height < 2) ? map[x-1, y] : null;
                map[x, y].Top    = ((y>0)           && map[x, y-1].Height-map[x, y].Height < 2) ? map[x, y-1] : null;
                map[x, y].Bottom = ((x<rows_x-1)    && map[x+1, y].Height-map[x, y].Height < 2) ? map[x+1, y] : null;
                map[x, y].Right  = ((y<columns_y-1) && map[x, y+1].Height-map[x, y].Height < 2) ? map[x, y+1] : null;;
            }
        }

        // Find the start and begin with the search for the best way to the goal
        var start = map.Cast<Position>()
                       .First(p => p.IsStart);

        start.StepsToGetHere = 0;
        start.SearchAndSet();

        var steps =  map.Cast<Position>()
                        .First(p => p.IsGoal)
                        .StepsToGetHere;

        Console.WriteLine($"done! Steps: {steps}");
    }    

    public class Position {

        public Position(char c) {
            Height  = CharToHeight(c);
            IsStart = c == 'S';
            IsGoal  = c == 'E';
        }

        public bool IsStart { get; }
        public bool IsGoal  { get; }

        public int Height { get; }

        public Position? Left   { get; set; }
        public Position? Top    { get; set; }
        public Position? Right  { get; set; }
        public Position? Bottom { get; set; }

        public int StepsToGetHere { get; set; } = -1;

        private int CharToHeight(char c) =>
        c switch
        {
            'S' => CharToHeight('a'),
            'E' => CharToHeight('z'),
            (>= 'a') and (<= 'z') => ((int)c)-((int)'a'),
            _ => throw new ArgumentException("sollte nicht passieren")
        };

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
public class Solution_18_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var instructions = Input_18_23.input
                                      .Split('\n')
                                      .Select(line => new Instruction(line))
                                      .ToList();

        var maxUp   = instructions.Where(i => i.Direction == Direction.Up).Sum(i => i.Distance);
        var maxLeft = instructions.Where(i => i.Direction == Direction.Left).Sum(i => i.Distance);

        var gridHeight = maxUp*2+2;
        var gridWidth = maxLeft*2+2;

        var grid = new string[gridHeight];
        for (int y=0; y<gridHeight; y++) {
            grid[y] = new string('.', gridWidth);
        }

        var currentY = maxUp;
        var currentX = maxLeft;

        foreach (var instruction in instructions) {

            for (int i=0; i<instruction.Distance; i++) { 

                SetGridMark(grid, currentX, currentY);

                switch (instruction.Direction) {
                    case Direction.Up:    currentY--; break;
                    case Direction.Down:  currentY++; break;
                    case Direction.Left:  currentX--; break;
                    case Direction.Right: currentX++; break;
                }
            }
        }

        SetGridMark(grid, currentX, currentY);

        //PrintGridToFile(grid, "unfilled.txt");

        var sum2 = 0;

        for (int y=0; y<gridHeight; y++) {

            var blockList = GetBlocks(grid[y], grid, y);
            blockList.Reverse();
            var blockStack = new Stack<Block>(blockList);

            while (blockStack.Count > 0) {

                var item = blockStack.Pop();

                if (item.Changer == false) {
                    sum2 += item.Length;
                    continue;
                }

                var nextItem = blockStack.Pop();                

                while (nextItem.Changer == false) {                     
                     nextItem = blockStack.Pop();
                }

                //FillGrid(grid, y, item.Start+item.Length, nextItem.Start-1);

                sum2 += nextItem.Start+nextItem.Length-item.Start;
            }
        } 

        //PrintGridToFile(grid, "filled.txt");

        Console.WriteLine($"Done! Sum: {sum2}");
    }

    // private static void FillGrid(string[] grid, int line, int from, int to) {
    //     for (int x=from; x<=to; x++) {
    //         if (grid[line][x] == '.') {
    //             grid[line] = grid[line].Remove(x, 1).Insert(x, "F");
    //         }
    //     }
    // }

    // private static void PrintGridToFile(string[] grid, string fileName) {
    //     using var writer = new StreamWriter(fileName);

    //     foreach (var line in grid)
    //     {
    //         writer.WriteLine(line);
    //     }
    // }

    public void SetGridMark(string[] grid, int x, int y) {
        grid[y] = grid[y].Remove(x, 1).Insert(x, "#");
    }

    public enum Direction {
        Up, Down, Right, Left
    }

    public class Block {
        public int Start { get; set; }
        public int Length { get; set; }
        public bool Changer { get; set; }
    }

    public List<Block> GetBlocks(string line, string[] grid, int lineNr) {
        
        var resultList = new List<Block>();

        var currentStart = -1;
        var onEdge = false;

        for (int i=0; i<line.Length; i++) {

            if (onEdge == false && line[i] == '#') {
                currentStart = i;
                onEdge = true;
                continue;
            }

            if (onEdge == true && line[i] == '#') {
                continue;
            }

            if (line[i]=='.' && onEdge == true) {
                resultList.Add(new Block() {Start = currentStart, Length = i-currentStart});
                onEdge = false;
            }

            if (line[i]=='.' && onEdge == false) {
                continue;
            }
        }

        foreach (var block in resultList) {
            if (block.Length == 1) {
                block.Changer = true;
            } else {
                var leftUp  = grid[lineNr-1][block.Start] == '#';
                var rightUp = grid[lineNr-1][block.Start+block.Length-1] == '#';
                block.Changer = leftUp != rightUp;
            }
        }

        return resultList;
    }

    public class Instruction {

        public Instruction(string input) {
            var parts = input.Split(' ');

            Direction = parts[0] switch {
                "U" => Direction.Up,
                "R" => Direction.Right,
                "L" => Direction.Left,
                _ => Direction.Down
            };

            Distance = int.Parse(parts[1]);            
        }

        public Direction Direction { get; }
        public int Distance { get; }     
    }
}
using System.Text;

public class Solution_17_1 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting ... ");

        var tetrisGrid = new TetrisGrid(Input_17.input, 10000);

        for (int i=0; i<2022; i++) {
            tetrisGrid.AddNextShape();
        }

        //tetrisGrid.PrintGrid(20);


        Console.WriteLine($"done! Height: {tetrisGrid.GetCurrentHeight()}");
    }   

    private class TetrisGrid {
        private char[,] grid;
        private int gridHeight;
        private Directions directions;
        private Shapes shapes;
        private int currentHeight = 0;

        public TetrisGrid(string input, int gridHeight) {

            directions = new Directions(input);
            shapes = new Shapes();
            this.gridHeight = gridHeight;

            grid = new char[7,gridHeight];

            for (int x=0; x<7; x++) {
                for (int y=0; y<gridHeight; y++) {
                    grid[x,y]='.';
                }
            }
        }

        public void AddNextShape() {
            
            var shape = shapes.GetNext();
            var coord = GetStartingPositionForShape(shape);

            while (true) {
                var nextLeftRight = directions.GetNext();

                if (IsMovePossible(shape, nextLeftRight, coord)) {
                    if (nextLeftRight == Direction.Left)  coord.X--;
                    if (nextLeftRight == Direction.Right) coord.X++;
                }

                if (IsMovePossible(shape, Direction.Down, coord)) {
                    coord.Y++;
                } else {
                    break;
                }
            }

            WriteShape(shape, coord);
        }

        private bool IsMovePossible(Shape s, Direction r, Coord c) {

            var newCoord = r switch {
                Direction.Left  => new Coord() {X=c.X-1, Y=c.Y  },
                Direction.Right => new Coord() {X=c.X+1, Y=c.Y  },
                Direction.Down  => new Coord() {X=c.X,   Y=c.Y+1},
                _ => throw new ArgumentException()
            };

            switch (s) {
                case Shape.Minus:  return IsMinusPossibleHere (newCoord);
                case Shape.Plus:   return IsPlusPossibleHere  (newCoord);
                case Shape.InvEl:  return IsInvElPossibleHere (newCoord);
                case Shape.Pipe:   return IsPipePossibleHere  (newCoord);
                case Shape.Square: return IsSquarePossibleHere(newCoord);
            }

            throw new ArgumentException();
        }

        private void WriteShape(Shape s, Coord c) {
            switch (s) {
                case Shape.Minus:  WriteMinus (c); break;
                case Shape.Plus:   WritePlus  (c); break;
                case Shape.InvEl:  WriteInvEl (c); break;
                case Shape.Pipe:   WritePipe  (c); break;
                case Shape.Square: WriteSquare(c); break;
            }
        }

        private bool IsMinusPossibleHere(Coord c) {
            return c.X >= 0 && c.X<=3 && c.Y < gridHeight && 
                   grid[c.X,  c.Y] == '.' &&
                   grid[c.X+1,c.Y] == '.' &&
                   grid[c.X+2,c.Y] == '.' &&
                   grid[c.X+3,c.Y] == '.';
        }

        private void UpdateCurrentHeight(int topBlock) {
            var top = gridHeight-topBlock;
            if (top > currentHeight)
                currentHeight = top;
        }

        private void WriteMinus(Coord c) {
            grid[c.X,  c.Y] = '#';
            grid[c.X+1,c.Y] = '#';
            grid[c.X+2,c.Y] = '#';
            grid[c.X+3,c.Y] = '#';
            UpdateCurrentHeight(c.Y);
        }

        private bool IsPlusPossibleHere(Coord c) {
            return c.X >= 0 && c.X<=4 && c.Y+2 < gridHeight && 
                   grid[c.X+1,c.Y  ] == '.' &&
                   grid[c.X,  c.Y+1] == '.' &&
                   grid[c.X+1,c.Y+1] == '.' &&
                   grid[c.X+2,c.Y+1] == '.' &&
                   grid[c.X+1,c.Y+2] == '.';
        }

        private void WritePlus(Coord c) {
            grid[c.X+1,c.Y  ] = '#';
            grid[c.X,  c.Y+1] = '#';
            grid[c.X+1,c.Y+1] = '#';
            grid[c.X+2,c.Y+1] = '#';
            grid[c.X+1,c.Y+2] = '#';
            UpdateCurrentHeight(c.Y);
        }

        private bool IsInvElPossibleHere(Coord c) {
            return c.X >= 0 && c.X<=4 && c.Y+2 < gridHeight &&
                   grid[c.X+2,c.Y  ] == '.' &&
                   grid[c.X+2,c.Y+1] == '.' &&
                   grid[c.X+2,c.Y+2] == '.' &&
                   grid[c.X  ,c.Y+2] == '.' &&
                   grid[c.X+1,c.Y+2] == '.';
        }

        private void WriteInvEl(Coord c) {
            grid[c.X+2,c.Y  ] = '#';
            grid[c.X+2,c.Y+1] = '#';
            grid[c.X+2,c.Y+2] = '#';
            grid[c.X  ,c.Y+2] = '#';
            grid[c.X+1,c.Y+2] = '#';
            UpdateCurrentHeight(c.Y);
        }

        private bool IsPipePossibleHere(Coord c) {
            return c.X >= 0 && c.X<=6 && c.Y+3 < gridHeight && 
                   grid[c.X,c.Y  ] == '.' &&
                   grid[c.X,c.Y+1] == '.' &&
                   grid[c.X,c.Y+2] == '.' &&
                   grid[c.X,c.Y+3] == '.';
        }

        private void WritePipe(Coord c) {
            grid[c.X,c.Y  ] = '#';
            grid[c.X,c.Y+1] = '#';
            grid[c.X,c.Y+2] = '#';
            grid[c.X,c.Y+3] = '#';
            UpdateCurrentHeight(c.Y);
        }

        private bool IsSquarePossibleHere(Coord c) {
            return c.X >= 0 && c.X<=5 && c.Y+1 < gridHeight &&
                   grid[c.X,  c.Y  ] == '.' &&
                   grid[c.X+1,c.Y+1] == '.' &&
                   grid[c.X,  c.Y+1] == '.' &&
                   grid[c.X+1,c.Y  ] == '.';
        }

        private void WriteSquare(Coord c) {
            grid[c.X,  c.Y  ] = '#';
            grid[c.X+1,c.Y+1] = '#';
            grid[c.X,  c.Y+1] = '#';
            grid[c.X+1,c.Y  ] = '#';
            UpdateCurrentHeight(c.Y);
        }

        private Coord GetStartingPositionForShape(Shape shape)
        {
            var currentHeight = GetCurrentHeight();

            var coord = shape switch {
                Shape.Minus  => new Coord() {X=2, Y=3+currentHeight},
                Shape.Plus   => new Coord() {X=2, Y=5+currentHeight},
                Shape.InvEl  => new Coord() {X=2, Y=5+currentHeight},
                Shape.Pipe   => new Coord() {X=2, Y=6+currentHeight},
                Shape.Square => new Coord() {X=2, Y=4+currentHeight},
                _ => throw new ArgumentException()
            };

            return ToGridCoordinates(coord);            
        }

        private Coord ToGridCoordinates(Coord c) {
            return new Coord() {
                X = c.X,
                Y = (gridHeight-1) - c.Y
            };
        }

        public int GetCurrentHeight() {
            return currentHeight;
        }

        public void PrintGrid(int height) {

            for (int y=0; y<height; y++) {

                var sb = new StringBuilder();
                sb.Append('|');
                for (int x=0; x<7; x++) {
                    sb.Append(grid[x, gridHeight-height+y]);
                }
                sb.Append('|');
                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine("|-------|");
        }
    } 

    private class Coord {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"({X}|{Y})";
        }
    } 

    private enum Direction {
        Left, Right, Down
    }

    private class Directions {
        private string input;
        private int position;

        public Directions(string input) {
            this.input = input;
            position = 0;
        }

        public Direction GetNext() {
            var currentChar = input[position++];

            if (position == input.Length)
                position = 0;

            if (currentChar == '<') return Direction.Left;
            if (currentChar == '>') return Direction.Right;

            throw new ArgumentException();
        }
    }

    private enum Shape {
        Minus,
        Plus,
        InvEl,
        Pipe,
        Square
    }

    private class Shapes {
        private int index;

        private List<Shape> allShapes;

        public Shapes() {
            allShapes = new List<Shape>() {
                Shape.Minus,
                Shape.Plus,
                Shape.InvEl,
                Shape.Pipe,
                Shape.Square
            };
        }

        public Shape GetNext() {
            var currentShape = allShapes[index++];

            if (index == allShapes.Count)
                index = 0;

           return currentShape;
        }
    }
    
}
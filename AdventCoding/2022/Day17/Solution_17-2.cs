using System.Text;

public class Solution_17_2 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting ... ");

        var input = Input_17.input;

        // var experimentGrid = new TetrisGrid(input, 2000*5, 0);
        // for (int i=0; i<2000; i++) {
        //     experimentGrid.AddNextShape();
        // }


        /*
            2611 | 1723
            2630 | 1725
            2630 | 1725
            2630 | 1725
            2630 | 1725
            2630 | 1725

,
            1524637681152

            1524637681149 (too high)
            1524637681148 ... too high

            1524637681137 --> muss auch too low sein
            1524637681136 --> muss auch too low sein
            1524637681135 --> muss auch too low sein
            1524637681134 --> muss auch too low sein
            1524637681133 --> muss auch too low sein
            1524637681132 --> muss auch too low sein
            1524637681131 --> muss auch too low sein
            1524637681130 (too low)
            1524637681128
            1524637681126 (too low)
            1524637681112

        */

        var heightOfJunk = 2630;
        var heightOfFirstJunk = 2613;
        var numberOfBricksInFirstJunk = 1724;
        var numberOfBricksInAJunk = 1725;

        var bricks = 1000000000000L;
        var bricksWithoutFirstIteration = bricks - numberOfBricksInFirstJunk; // 999999998277

        var totalHeight = 0L;
        totalHeight += heightOfFirstJunk; 

        var numberOfJunks = bricksWithoutFirstIteration / numberOfBricksInAJunk; // 579710143
        totalHeight += numberOfJunks * heightOfJunk;

        var restOfTheBricks = (bricksWithoutFirstIteration % numberOfBricksInAJunk)-1; // 1602

        //for (int startingShapeIndex=0; startingShapeIndex<5; startingShapeIndex++) {
            var tetrisGrid = new TetrisGrid(input, (int)restOfTheBricks*5, 0);
            for (int i=0; i<restOfTheBricks; i++) {
                tetrisGrid.AddNextShape();
            }
            Console.WriteLine(totalHeight + tetrisGrid.GetCurrentHeight());
        //}

        Console.WriteLine("Done");
    }   

    private class TetrisGrid {
        private char[,] grid;
        private int gridHeight;
        private Directions directions;
        private Shapes shapes;
        private int currentHeight = 0;

        public TetrisGrid(string input, int gridHeight, int startShapeIndex) {

            directions = new Directions(input);
            shapes = new Shapes(startShapeIndex);
            this.gridHeight = gridHeight;

            grid = new char[7,gridHeight];

            for (int x=0; x<7; x++) {
                for (int y=0; y<gridHeight; y++) {
                    grid[x,y]='.';
                }
            }
        }


        public int GetShapeIndex() {
            return shapes.GetIndex();
        }

        public int GetMoveIndes() {
            return directions.GetIndex();
        }

        // private int lastPrintedHeight = 0;
        // private int shapeCounter = 0;
        // private int printNext = -1;

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

            //shapeCounter++;
            WriteShape(shape, coord);

            // if (printNext > 0) {
            //     printNext--;
            //     Console.WriteLine();
            //     PrintGrid(currentHeight-15, currentHeight+5);
            //     Console.WriteLine();
            // }

            // if (directions.GetIndex() == 0) {
            //     Console.WriteLine($"{currentHeight-lastPrintedHeight} | {shapeCounter} | next shape: {shapes.GetIndex()}");
            //     lastPrintedHeight = currentHeight;
            //     shapeCounter = 0;
            //     Console.WriteLine();
            //     PrintGrid(currentHeight-15, currentHeight+5);
            //     Console.WriteLine();
            //     printNext = 10;
            // }
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

        private void UpdateCurrentHeight(int topBlock) {
            var top = gridHeight-topBlock;
            if (top > currentHeight)
                currentHeight = top;
        }

        private bool IsMinusPossibleHere(Coord c) {
            return c.X >= 0 && c.X<=3 && c.Y < gridHeight && 
                   grid[c.X,  c.Y] == '.' &&
                   grid[c.X+1,c.Y] == '.' &&
                   grid[c.X+2,c.Y] == '.' &&
                   grid[c.X+3,c.Y] == '.';
        }

        private void WriteMinus(Coord c) {
            grid[c.X,  c.Y] = '-';
            grid[c.X+1,c.Y] = '-';
            grid[c.X+2,c.Y] = '-';
            grid[c.X+3,c.Y] = '-';
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
            grid[c.X+1,c.Y  ] = '+';
            grid[c.X,  c.Y+1] = '+';
            grid[c.X+1,c.Y+1] = '+';
            grid[c.X+2,c.Y+1] = '+';
            grid[c.X+1,c.Y+2] = '+';
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
            grid[c.X+2,c.Y  ] = 'L';
            grid[c.X+2,c.Y+1] = 'L';
            grid[c.X+2,c.Y+2] = 'L';
            grid[c.X  ,c.Y+2] = 'L';
            grid[c.X+1,c.Y+2] = 'L';
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
            grid[c.X,c.Y  ] = 'P';
            grid[c.X,c.Y+1] = 'P';
            grid[c.X,c.Y+2] = 'P';
            grid[c.X,c.Y+3] = 'P';
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
            grid[c.X,  c.Y  ] = 'S';
            grid[c.X+1,c.Y+1] = 'S';
            grid[c.X,  c.Y+1] = 'S';
            grid[c.X+1,c.Y  ] = 'S';
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
                sb.Append($"{height-y}: |");
                for (int x=0; x<7; x++) {
                    sb.Append(grid[x, gridHeight-height+y]);
                }
                sb.Append('|');
                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine("|-------|");
        }

        public void PrintGrid(int from, int to) {

            for (int y=0; y<=to-from; y++) {

                var sb = new StringBuilder();
                sb.Append($"{to-y}: |");
                for (int x=0; x<7; x++) {
                    sb.Append(grid[x, gridHeight-to+y]);
                }
                sb.Append('|');
                Console.WriteLine(sb.ToString());
            }
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

        public int GetIndex() {
            return position;
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

        public Shapes(int startShapeIndex) {
            index = startShapeIndex;
            allShapes = new List<Shape>() {
                Shape.Minus,
                Shape.Plus,
                Shape.InvEl,
                Shape.Pipe,
                Shape.Square
            };
        }

        public int GetIndex() {
            return index;
        }

        public Shape GetNext() {
            var currentShape = allShapes[index++];

            if (index == allShapes.Count)
                index = 0;

           return currentShape;
        }
    }
    
}
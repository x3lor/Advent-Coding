public class Solution_10_2_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting...");

        string[] grid = Input_10_23.input2.Split('\n');
        string[] shadow = new string[grid.Length];
    
        for (int i=0; i<grid.Length; i++) {
            shadow[i] = new string(' ', grid[0].Length);
        }

        const int posSX = 53;
        const int posSY = 75;

        var posX = posSX;
        var posY = posSY;

        shadow[posY] = shadow[posY].Remove(posX, 1).Insert(posX, "X");       
        posX++;

        var commingFrom = Direction.Left;

        while (!(posX == posSX && posY==posSY)) {

            shadow[posY] = shadow[posY].Remove(posX, 1).Insert(posX, "X");           

            switch (grid[posY][posX]) {
                case '|': {                    
                    if (commingFrom == Direction.Top) {                       
                        posY++;
                    } else {                        
                        posY--;                        
                    }
                    break;
                }
                case '-': {
                    if (commingFrom == Direction.Left) {                        
                        posX++;                       
                    } else {                        
                        posX--;
                    }
                    break;
                }
                case 'L': {
                    if (commingFrom == Direction.Top) {                        
                        posX++;
                        commingFrom = Direction.Left;
                    } else {
                        posY--;
                        commingFrom = Direction.Bottom;
                    }
                    break;
                }
                case 'J': {                    
                    if (commingFrom == Direction.Top) {
                        posX--;
                        commingFrom = Direction.Right;
                    } else {
                        posY--;
                        commingFrom = Direction.Bottom;
                    }
                    break;
                }
                case '7': {                    
                    if (commingFrom == Direction.Left) {
                        posY++;
                        commingFrom = Direction.Top;
                    } else {
                        posX--;
                        commingFrom = Direction.Right;
                    }
                    break;
                }
                case 'F': {                    
                    if ( commingFrom == Direction.Right) {
                        posY++;
                        commingFrom = Direction.Top;
                    } else {
                        posX++;
                        commingFrom = Direction.Left;
                    }
                    break;
                }
                default: throw new Exception("Should not happen!");
            }
        }
        
        shadow[posY] = shadow[posY].Remove(posX, 1).Insert(posX, "X");

        var innerCounter = 0;
        for (int y=0; y<grid.Length; y++) {

            bool outerPos = true;
            for (int x=0; x<grid[0].Length; x++) {
                
                if (shadow[y][x] == 'X') {

                    if (grid[y][x] == 'L') {
                        while (true) {
                            x++;
                            if (grid[y][x] == 'J') {
                                break;
                            }
                            if (grid[y][x] == '7') {
                                outerPos = !outerPos;
                                break;
                            }
                        }
                    }

                    if (grid[y][x] == 'F') {
                        while (true) {
                            x++;
                            if (grid[y][x] == '7') {
                                break;
                            }
                            if (grid[y][x] == 'J') {
                                outerPos = !outerPos;
                                break;
                            }
                        }
                    }

                    if (grid[y][x] == '|') {
                        outerPos = !outerPos;
                    }                                                    

                } else {
                    if (outerPos) {
                        shadow[y] = shadow[y].Remove(x, 1).Insert(x, "O");                        
                    } else {
                        innerCounter++;
                        shadow[y] = shadow[y].Remove(x, 1).Insert(x, "I");
                    }
                }
            }
        }

        Console.WriteLine($"Done ... counter: {innerCounter}");
    }

    public enum Direction {
        Left, Right, Top, Bottom, None
    }
}
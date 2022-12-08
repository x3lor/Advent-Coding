public class Solution_8_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input   = Input_8.input.Split('\n');
        var rows    = input.Length;
        var columns = input[0].Length;
        var forest  = new Tree[rows, columns];

        for (int r=0; r<rows; r++) {
            for (int c=0; c<columns; c++) {
                forest[r, c] = new Tree {
                    Height = int.Parse(input[r].Substring(c, 1)),
                    Row = r,
                    Column = c
                };
            }
        }

        var max = forest.Cast<Tree>()
                        .Select(t => CalcScore(forest, t, rows, columns))
                        .Max();

        Console.WriteLine($"done! Max: {max}");
    }   

    private int CalcScore(Tree[,] forest, Tree tree, int rows, int columns) {

        var r = tree.Row;
        var c = tree.Column;
        int treeHight = forest[r, c].Height;

        var up    = r; do { up--;    } while (up>0            && forest[up,    c].Height < treeHight);
        var down  = r; do { down++;  } while (down<rows-1     && forest[down,  c].Height < treeHight);
        var left  = c; do { left--;  } while (left>0          && forest[r,  left].Height < treeHight);
        var right = c; do { right++; } while (right<columns-1 && forest[r, right].Height < treeHight);

        return (r - up) * (down - r) * (c-left) * (right-c); 
    }

    private class Tree {
        public int Height { get; set; }
        public int Row    { get; set; } 
        public int Column { get; set; } 
    } 
}
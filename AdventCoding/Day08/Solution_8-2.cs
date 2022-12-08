public class Solution_8_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var input = Input_8.input.Split('\n');

        var rows    = input.Length;
        var columns = input[0].Length;

        var forest = new Tree[rows, columns];

        for (int r=0; r<rows; r++) {
            for (int c=0; c<columns; c++) {
                forest[r, c] = new Tree {
                    Height = int.Parse(input[r].Substring(c, 1)),
                    Score = 0
                };
            }
        }

        for (int r=1; r<rows-1; r++) {
            for (int c=1; c<columns-1; c++) {
                forest[r, c].Score = CalcScore(forest, r, c, rows, columns);
            }
        }

        var max = 0;
        for (int r=0; r<rows; r++) {
            for (int c=0; c<columns; c++) {
                if (forest[r,c].Score > max) {
                    max = forest[r,c].Score;
                }
            }
        }

        Console.WriteLine($"done! Max: {max}");
    }   

    private int CalcScore(Tree[,] forest, int r, int c, int rows, int columns) {

        int treeHight = forest[r, c].Height;

        int upfactor = 1;
        for (int up=r-1; up>0; up--) {
            if (forest[up, c].Height >= treeHight) {
                break;
            } else {
                upfactor++;
            }
        }

        int downfactor = 1;
        for (int down=r+1; down<rows; down++) {
            if (forest[down, c].Height >= treeHight) {
                break;
            } else {
                downfactor++;
            }
        }

        int leftfactor = 1;
        for (int left=c-1; left>0; left--) {
            if (forest[r, left].Height >= treeHight) {
                break;
            } else {
                leftfactor++;
            }
        }

        int rightfactor = 1;
        for (int left=c+1; left<columns-1; left++) {
            if (forest[r, left].Height >= treeHight) {
                break;
            } else {
                rightfactor++;
            }
        }

        return upfactor * downfactor * leftfactor * rightfactor;
    }

    private class Tree {
        public int Height { get; set; }
        public int Score { get; set; } 
    } 
}
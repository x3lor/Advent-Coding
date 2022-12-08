public class Solution_8_1 : ISolution
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
                    Visible = r==0 || r==rows-1 || c==0 || c==columns-1 // set edges to true
                };
            }
        }

        // from left
        for (int r=1; r<rows; r++) {
            var growLeft = forest[r,0].Height;
            for (int c=1; c<columns; c++) {
                if (forest[r,c].Height > growLeft) {
                    forest[r,c].Visible = true;
                    growLeft = forest[r,c].Height;
                }
            }
        }

        // fromright
        for (int r=1; r<rows; r++) {
            var growright = forest[r,columns-1].Height;
            for (int c=columns-2; c>0; c--) {
                if (forest[r,c].Height > growright) {
                    forest[r,c].Visible = true;
                    growright = forest[r,c].Height;
                }
            }
        }

        // from top
        for (int c=1; c<columns; c++) {
            var growTop = forest[0,c].Height;
            for (int r=1; r<rows; r++) {
                if (forest[r,c].Height > growTop) {
                    forest[r,c].Visible = true;
                    growTop = forest[r,c].Height;
                }
            }
        }

        // from bottom
        for (int c=1; c<columns; c++) {
            var growBottom = forest[rows-1,c].Height;
            for (int r=rows-2; r>0; r--) {
                if (forest[r,c].Height > growBottom) {
                    forest[r,c].Visible = true;
                    growBottom = forest[r,c].Height;
                }
            }
        }

        var sum = 0;
        for (int r=0; r<rows; r++) {
            for (int c=0; c<columns; c++) {
                if (forest[r,c].Visible) {
                    sum++;
                }
            }
        }

        Console.WriteLine($"done! Sum: {sum}");
    }   

    private class Tree {
        public int Height { get; set; }
        public bool Visible { get; set; } 
    } 
}
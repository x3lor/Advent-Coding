using System.Text;

public class Solution_14_2 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting ... ");

        var fInput = Input_14.input;
        
        // Get size of the grid
        var minX = int.MaxValue;
        var minY = int.MaxValue;
        var maxX = 0;
        var maxY = 0;

        foreach(var line in fInput.Split('\n')) {
            
            var parts = line.Split(" -> ");

            foreach(var koord in parts) {
                var koordParts = koord.Split(',');
                var x = int.Parse(koordParts[0]);
                var y = int.Parse(koordParts[1]);

                if (x<minX) minX = x;
                if (x>maxX) maxX = x;
                if (y<minY) minY = y;
                if (y>maxY) maxY = y;
            }

            if (500<minX) minX = 500;
            if (500>maxX) maxX = 500;
            if (0<minY) minY = 0;
            if (0>maxY) maxY = 0;
        }

        var gridHeight = maxY-minY+1+2;
        var additionalWidthExpansion = (2*gridHeight);
        var gridWidth = maxX-minX+1+(2*additionalWidthExpansion);
        
        // get lines with real grid koordinates
        var polyLines = new List<List<Koord>>();

        foreach(var line in fInput.Split('\n')) {
            
            var parts = line.Split(" -> ");
            var kLine = new List<Koord>();

            foreach(var koord in parts) {
                var koordParts = koord.Split(',');
                var realX = int.Parse(koordParts[0])-minX+additionalWidthExpansion;
                var realY = int.Parse(koordParts[1]);

                var k = new Koord {X=realX,Y=realY};
                kLine.Add(k);
            }

            polyLines.Add(kLine);
        }    

        // create grid
        var grid = new char[gridWidth, gridHeight];

        for(int x=0; x<gridWidth; x++) {
            for (int y=0; y<gridHeight; y++) {
                grid[x,y] = '.';
            }
        }

        for (int x=0; x<gridWidth; x++) {
            grid[x, gridHeight-1] = '#';
        }

        grid[500-minX+additionalWidthExpansion, 0] = '+';

        // draw lines into grid
        foreach(var polyLine in polyLines) {

            for (int i=0; i<polyLine.Count-1; i++) {
                var from = polyLine[i];
                var to   = polyLine[i+1];

                if (from.X == to.X) {
                    var top    = Math.Min(from.Y, to.Y);
                    var bottom = Math.Max(from.Y, to.Y);
                    for (var y=top; y<=bottom; y++) {
                        grid[from.X, y] = '#';
                    }
                } else {
                    var left  = Math.Min(from.X, to.X);
                    var right = Math.Max(from.X, to.X);
                    for (var x=left; x<=right; x++) {
                        grid[x, from.Y] = '#';
                    }
                }
            }
        }

        //PrintGrid(grid, gridWidth, gridHeight);

        // fill it with sand!

        bool endIt = false;
        int sandcounter = 0;

        while(true) {

            if (endIt)
                break;

            // start at 500|0
            var current = new Koord() {X=500-minX+additionalWidthExpansion, Y=0};

            while (true) {

                if (endIt)
                    break;

                while (grid[current.X, current.Y+1] == '.') {

                    current.Y++;

                    if(current.Y+1 >= gridHeight) {
                        endIt=true;
                        break;
                    }
                }

                if(current.X-1 < 0 || current.Y+1 >= gridHeight) {
                    endIt=true;
                    break;
                }

                // is leftdown possible?
                if (grid[current.X-1, current.Y+1] == '.') {

                    current.X--;
                    current.Y++;
                    continue;
                }

                if(current.X+1 >= gridWidth || current.Y+1 >= gridHeight) {
                    endIt=true;
                    break;
                }

                // is rightdown possible?
                if (grid[current.X+1, current.Y+1] == '.') {
                    current.X++;
                    current.Y++;
                    continue;
                }

                // sand has it's place
                grid[current.X, current.Y] = 'o';
                sandcounter++;

                if (current.X == 500-minX+additionalWidthExpansion &&
                    current.Y == 0) {
                    endIt=true;
                    break;
                }

                //PrintGrid(grid, gridWidth, gridHeight);
                break;
            }
        }

        //PrintGrid(grid, gridWidth, gridHeight);

        Console.WriteLine("Done! Sand:" + sandcounter);

    }  

    private void PrintGrid(char[,]grid, int gridWidth, int gridHeight) {
        
        Console.WriteLine("\n");

        for(int y=0; y<gridHeight; y++) {

            var sb = new StringBuilder();

            for (int x=0; x<gridWidth; x++) {
                sb.Append(grid[x,y]);
            }

            Console.WriteLine(sb.ToString());
        }
    }

    private class Koord {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"({X}|{Y})";
        }
    }  
}
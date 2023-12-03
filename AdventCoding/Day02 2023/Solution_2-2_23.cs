public class Solution_2_2_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var result = Input_2_23.input
                               .Split('\n')
                               .Select(line => new Game(line))
                               .Select(game => game.power())
                               .Sum();
                         

        Console.WriteLine($"done! Sum: {result}");
    }

    public class Game {
        public Game (string init) {
            var parts = init.Substring(5).Split(':');
            Num = int.Parse(parts[0]);
            Testlist = parts[1].Split(';').Select(subpart => new GameTest(subpart)).ToList();
        }

        public int Num { get; }
        public IList<GameTest> Testlist { get; }

        public bool possibleGame() {

            foreach(var gameTest in Testlist) {
                if (gameTest.Red   > 12) return false;
                if (gameTest.Green > 13) return false;
                if (gameTest.Blue  > 14) return false;
            }

            return true;
        }

        public int power() {
            var minRed = 0;
            var minBlue = 0;
            var minGreen = 0;

            foreach(var gameTest in Testlist) {
                if (gameTest.Red > minRed) minRed = gameTest.Red;
                if (gameTest.Blue > minBlue) minBlue = gameTest.Blue;
                if (gameTest.Green > minGreen) minGreen = gameTest.Green;
            }

            return minRed*minBlue*minGreen;
        }
    }

    public class GameTest {
        public GameTest (string init) {
            var parts = init.Split(',');

            var green = 0;
            var red = 0;
            var blue = 0;

            foreach (var part in parts) {
                var subsplit = part.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (subsplit[1] == "blue")  blue  = int.Parse(subsplit[0]);
                if (subsplit[1] == "red")   red   = int.Parse(subsplit[0]);
                if (subsplit[1] == "green") green = int.Parse(subsplit[0]);
            }

            Green = green;
            Blue = blue;
            Red = red;
        }

        public int Green { get; }
        public int Red {get;}
        public int Blue {get;}
    }
}
public class Solution_3_2_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var stringarray = Input_3_23.input.Split('\n').ToList();
        var lineLenth = stringarray[0].Length;

        var stars = new List<Star>();
        var numbers = new List<Number>();

        var ptrX = 0;
        var ptrY = 0;

        while (ptrY < stringarray.Count) {
            ptrX = 0;
            while (ptrX < lineLenth) {

                if (stringarray[ptrY][ptrX] == '*') {
                    stars.Add(new Star(ptrX, ptrY));                                                    
                    ptrX++;
                } else if (isAtNumber(stringarray, ptrX, ptrY)) {
                    var number = getNumber(stringarray, ptrX, ptrY);
                    numbers.Add(number);
                    ptrX += number.Length;
                } else {
                    ptrX++;
                }
            }
            ptrY++;
        }

        var result = stars.Select(star => star.GetGear(numbers))
                          .Sum();
                              

        Console.WriteLine($"done! Sum: {result}");
    }

    public static bool isAtNumber(List<string> array, int x, int y) {
        return array[y][x] >= '0' && array[y][x] <= '9';
    }

    public static Number getNumber(List<string> array, int x, int y) {

         var lineLenth = array[0].Length;

        var start = x;
        var end = x;

        while (end<lineLenth && isAtNumber(array, end, y)) {
            end++;
        }

        return new Number(int.Parse(array[y].Substring(start, end-start)), x, y);
    }

    public class Number {
        public Number(int num, int x, int y) {
            Num = num;
            X = x;
            Y = y;
        }

        public int Num { get; }
        public int X { get; }
        public int Y { get; }

        public int Length { get {
            if (Num < 10) return 1;
            if (Num < 100) return 2;
            if (Num < 1000) return 3;
            return 4;
        }}
    }

    public class Star {

        private int x;
        private int y;

        public Star (int x, int y) {
            this.x = x;
            this.y = y;
        }

        public int GetGear(List<Number> numbers) {
            var nums = numbers.Where(IsNumberAdjecent).ToList();

            if (nums.Count == 2) {
                return nums[0].Num*nums[1].Num;
            }

            return 0;
        }

        private bool IsNumberAdjecent(Number num) {
            
            if (Math.Abs(num.Y-y) > 1) return false;
            if (num.X+num.Length < x) return false;
            if (num.X > x+1) return false;

            return true;
        }
    }
 
}
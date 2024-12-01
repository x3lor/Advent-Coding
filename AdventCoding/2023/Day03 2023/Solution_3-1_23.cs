public class Solution_3_1_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var sum = 0;

        var stringarray = Input_3_23.input.Split('\n').ToList();
        var lineLenth = stringarray[0].Length;

        var arraypointerX = 0;
        var arraypointerY = 0;

        while (arraypointerY < stringarray.Count) {

            arraypointerX = 0;
            while (arraypointerX < lineLenth) {

                if (isAtNumber(stringarray, arraypointerX, arraypointerY)) {

                    var number = getNumber(stringarray, arraypointerX, arraypointerY);
                    
                    if (doesNumberCount(stringarray, arraypointerX, arraypointerY)) {
                        sum += number; 
                    } 

                    var numberLenght = getNumberLength(number);
                    arraypointerX += numberLenght;
                } else {
                    arraypointerX++;
                }

            }

            // goto next line
            arraypointerY++;
        }
                              

        Console.WriteLine($"done! Sum: {sum}");
    }

    public static bool isAtNumber(List<string> array, int x, int y) {
        return array[y][x] >= '0' && array[y][x] <= '9';
    }

    public static int getNumber(List<string> array, int x, int y) {

         var lineLenth = array[0].Length;

        var start = x;
        var end = x;

        while (end<lineLenth && isAtNumber(array, end, y)) {
            end++;
        }

        return int.Parse(array[y].Substring(start, end-start));
    }

    public static int getNumberLength(int number) {
        if (number < 10) return 1;
        if (number < 100) return 2;
        if (number < 1000) return 3;
        return 4;
    }

    public static bool doesNumberCount(List<string> array, int x, int y) {
         var lineLenth = array[0].Length;
        var numberLenght = getNumberLength(getNumber(array, x,y));

        for (int yPtr=y-1; yPtr<=y+1; yPtr++) {
            for (int xPtr=x-1; xPtr<= x+numberLenght; xPtr++) {

                if (yPtr < 0 || yPtr >= array.Count || xPtr < 0 || xPtr >= lineLenth) {
                    continue;
                }

                if (!isAtNumber(array, xPtr, yPtr) && array[yPtr][xPtr] != '.') {
                    return true;
                }
            }
        }

        return false;
    }
 
}
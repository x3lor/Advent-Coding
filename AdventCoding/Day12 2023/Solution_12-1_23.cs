public class Solution_12_1_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var sum = 0;
        var counter = 0;
        
        foreach(var line in Input_12_23.input.Split('\n')) {

            Console.Write($"\r{counter++} of 1000");

            var parts = line.Split(' ');
            var pattern = parts[0];
            var numberList = parts[1];
            var maxBinary = (long)Math.Pow(2, pattern.Length)-1; 

            for (long i=0; i<=maxBinary; i++) {
                var binaryString = Convert.ToString(i, 2);
                if (IsMatch(binaryString, pattern, numberList))
                    sum++;
            }
        }
        Console.WriteLine($"\nDone! Sum: {sum}");
    }

    private bool IsMatch(string binaryString, string pattern, string numberList) {

        if (binaryString.Length != pattern.Length)
            binaryString = new string('0', pattern.Length-binaryString.Length) + binaryString;

        for (int i=0; i<binaryString.Length; i++) {

            if ((binaryString[i] == '0' && pattern[i] == '#') ||
                (binaryString[i] == '1' && pattern[i] == '.')) {
                    return false;
                }
        }

        var parts = binaryString.Split('0', StringSplitOptions.RemoveEmptyEntries)
                                .Select(n => n.Length.ToString())
                                .ToList();
        
        var s = string.Join(',', parts);
        return s==numberList;
    }
}
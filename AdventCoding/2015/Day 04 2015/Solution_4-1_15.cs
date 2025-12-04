using System.Security.Cryptography;
using System.Text;

public class Solution_4_1_15 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");
        
        var input = "ckczppom";

        for (long number=0L; number<10000000L; number++)
        {
            if (HasLeadingZeroHexDigits(input + number.ToString(), 5))
            {
                Console.WriteLine($"done! Result: {number}");
                break;
            }
        }
    }

    static readonly MD5 Md5 = MD5.Create(); // single-threaded okay

    static bool HasLeadingZeroHexDigits(string input, int zeroHexDigits)
    {
        // String -> Bytes
        byte[] data = Encoding.ASCII.GetBytes(input); // meist ASCII bei AoC
        byte[] hash = Md5.ComputeHash(data);

        int fullBytes = zeroHexDigits / 2;
        int halfByte  = zeroHexDigits % 2;

        // ganze Bytes müssen 0 sein
        for (int i = 0; i < fullBytes; i++)
        {
            if (hash[i] != 0)
                return false;
        }

        if (halfByte == 1)
        {
            // noch 1 Nibble (halbes Byte) prüfen -> oberes Nibble == 0
            if ((hash[fullBytes] & 0xF0) != 0)
                return false;
        }

        return true;
    }
}
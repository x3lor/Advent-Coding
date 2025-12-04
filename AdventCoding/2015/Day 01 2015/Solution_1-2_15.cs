public class Solution_1_2_15 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        int currentLevel = 0;

        var input = Input_1_15.input;

        for (int i=1; i<=input.Length; i++)
        {
            if (input[i-1] == '(')
                currentLevel++;
            else
                currentLevel--;

            if (currentLevel == -1)
            {
                Console.WriteLine($"done! {i}");
                return;
            }
        }

        Console.WriteLine($"done! no result");
    }
}
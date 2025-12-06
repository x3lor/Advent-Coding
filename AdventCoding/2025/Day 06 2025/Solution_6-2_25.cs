using System.Numerics;
using System.Text;

public class Solution_6_2_25 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var numberRows = 4;

        var input = Input_6_25.input.Split('\n');
        var operators = input[numberRows];

        var operatorIndecies = new List<int>();

        for (int i=0; i<operators.Length; i++)
        {
            if (operators[i] != ' ')
            {
                operatorIndecies.Add(i);
            }
        }

        var result = new BigInteger(0);

        for (int i=0; i<operatorIndecies.Count; i++)
        {
            var currentIndex = operatorIndecies[i];
            var nextIndex = i == operatorIndecies.Count-1 
                                ? operators.Length+1 
                                : operatorIndecies[i+1];
            var numberCount = nextIndex-currentIndex-1;

            var builder = new List<StringBuilder>();

            for (int number=0; number<numberCount; number++)
            {
                var sb = new StringBuilder();

                for (int j=0; j<numberRows; j++)
                {
                    sb.Append(input[j][currentIndex+number]);
                }

                builder.Add(sb);
            }

            var numbers = builder.Select(sb => long.Parse(sb.ToString().Trim())).ToList();

            if (operators[currentIndex] == '+')
            {
                var toAdd = numbers.Sum();
                result += toAdd;
            } else
            {
                var tmp = 1L;

                foreach(var num in numbers)
                {
                    tmp *= num;
                }

                result += tmp;
            }
        }

        Console.WriteLine($"done! Sum: {result}");
    }   
}
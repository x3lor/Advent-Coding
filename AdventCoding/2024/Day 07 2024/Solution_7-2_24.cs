public class Solution_7_2_24 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting ... ");

        var sum = Input_7_24.input
                            .Split('\n')
                            .Select(s => new Equation(s))
                            .Where(e => e.IsSolvable())
                            .Select(e => e.GetResult())
                            .Sum();

        Console.WriteLine($"done! Sum: {sum}");
    }

    private class Equation
    {
        long result;
        List<int> operators;

        public Equation(string s) {
            var parts = s.Split(':');
            result = long.Parse(parts[0]);
            operators = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Select(s => int.Parse(s))
                                .ToList();
        }

        public bool IsSolvable() 
        {
            var numberOperators = operators.Count-1;
            var tryMax = (long)Math.Pow(3, numberOperators);

            for(var currentTry = 0L; currentTry < tryMax; currentTry++) {
                if (TryIt(currentTry, numberOperators)) {
                    return true;
                }
            }

            return false;
        }

        private bool TryIt(long op, int bitLength) {
        
            long compuResult = operators[0];

            for (int i = 0; i < bitLength; i++)
            {
                var currentOperator = (op / (long)Math.Pow(3, i)) % 3;

                if (currentOperator == 0)                
                    compuResult += operators[i+1];                
                else if (currentOperator == 1)                
                    compuResult *= operators[i+1];                
                else if (currentOperator == 2)
                    compuResult = compuResult * (long)Math.Pow(10, operators[i + 1].ToString().Length) + operators[i + 1];                
            }
            
            return compuResult == result;
        }

        public long GetResult() => result;
    }

}
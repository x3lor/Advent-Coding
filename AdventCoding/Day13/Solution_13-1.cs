using System.Text;

public class Solution_13_1 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting ... ");

        var sum = 0;

        var input = Input_13.input.Split('\n');

        for(int i = 0; i<input.Length; i+=3) {
            
            var left  = new Element(input[i]);
            var right = new Element(input[i+1]);

            if (Compare(left, right) > 0) {
                sum += (i/3)+1;
                Console.WriteLine($"{(i/3)+1} is correct");
            } else {
                //Console.WriteLine($"{(i/3)+1} is wrong");
            }
        }

        Console.WriteLine($"done! Sum: {sum}");
    }    

    // 1: right order // left side smaller value // left side fewer items
    // -1: wrong order
    private int Compare(Element left, Element right) {

        if (left.Type == ElementType.Number || right.Type == ElementType.Number) {
            throw new ArgumentException("wrong input");
        }

        IList<Element> leftList  =  left.SubElements == null ? throw new ArgumentException() :  left.SubElements;
        IList<Element> rightList = right.SubElements == null ? throw new ArgumentException() : right.SubElements;

        for(int i=0; i<leftList.Count; i++) {
            var l = leftList[i];

            if (rightList.Count < i+1) {
                return -1; // right out of items
            }

            var r = rightList[i];

            if (l.Type == ElementType.Number && r.Type == ElementType.Number) {

                if (l.NumberValue > r.NumberValue) {
                    return -1; // left side is bigger
                } else if (l.NumberValue < r.NumberValue) {
                    return 1; 
                } else {
                    continue;
                }
            }

            if(l.Type == ElementType.List && r.Type == ElementType.List) {
                var intermediate = Compare(l, r);
                if (intermediate < 0)
                    return -1;
                if (intermediate > 0)
                    return 1;
                
                continue;
            }

            if (l.Type == ElementType.Number) {
                l = new Element($"[{l.NumberValue}]");
            }

            if (r.Type == ElementType.Number) {
                r = new Element($"[{r.NumberValue}]");
            }

            var intermediate2 = Compare(l, r);
                if (intermediate2 < 0)
                    return -1;
                if (intermediate2 > 0)  
                    return 1;       
        }

        return 1;   // left side out of items
    }

    private enum ElementType {
        List,
        Number
    }

    private class Element {

        public Element(string init) {

            Type = init.StartsWith("[") ? ElementType.List : ElementType.Number;

            if (Type == ElementType.Number) {
                SubElements = null;
                NumberValue = int.Parse(init);
                return;
            }

            // now handle Lists
            NumberValue = -1;
            SubElements = SeperateIntoSubelements(init).Select(sub => new Element(sub)).ToList();
        }

        private IList<string> SeperateIntoSubelements(string s) {
            var result = new List<string>();
            
            if (s.Length == 2)
                return result;

            var current = s.Substring(1, s.Length-2);

            var start = 0;
            var openBracketCounter = 0;

            for (int i = 0; i<current.Length; i++) {
                if (current[i] == ',' && openBracketCounter == 0) {
                    result.Add(current.Substring(start, i-start));
                    start = i+1;
                }
                if (current[i]=='[') openBracketCounter++;
                if (current[i]==']') openBracketCounter--;
            }

            result.Add(current.Substring(start, current.Length-start));
            return result;
        }

        public IList<Element>? SubElements { get; }
        public int NumberValue { get; }
        public ElementType Type { get; }

        public override string ToString()
        {
            if (Type == ElementType.Number) {
                return NumberValue.ToString();
            } else {
                var sb = new StringBuilder();
                for (int i=0; i<SubElements?.Count; i++) {
                    sb.Append(SubElements[i].ToString());
                    if (i != SubElements.Count-1)
                        sb.Append(",");
                }
                return $"[{sb.ToString()}]";
            }
        }
    }
}
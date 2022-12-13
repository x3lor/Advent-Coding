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
                Console.WriteLine($"{(i/3)+1} is wrong");
            }
        }

        Console.WriteLine($"done! Sum: {sum}");
    }    

    // 1: right order // left side smaller 
    // -1: wrong order
    private int Compare(Element left, Element right) {

        for(int i=0; i<left.SubElements.Count; i++) {
            var l = left.SubElements[i];

            if (right.SubElements.Count <= i) {
                return -1; // right out of items
            }

            var r = right.SubElements[i];

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
                else
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
                SubElements = new List<Element>();
                NumberValue = int.Parse(init);
                return;
            }

            // now handle Lists
            NumberValue = -1;
            SubElements = SeperateIntoSubelements(init).Select(sub => new Element(sub)).ToList();
        }

        // Example:
        // [[[[8,9,9,0],4,[3,7,0,1],5,10],4,[2,[4,4,3,4,4],9,3,5],4,[[],7,[9,8,2,0],10,4]],[],[ 6,6,0,[]]]
        // [[[6,8,9],1,[6,1,5],[6,5,10]],[2,4,[[10,1],8,[7,6,2],[],0]],[[0,9,[],[1,3]]],[[]],[0]]

        private IList<string> SeperateIntoSubelements(string s) {
            var result = new List<string>();
            if (s.Length == 2)
                return result;

            var current = s.Substring(1, s.Length-2);
            
            do {
                var firstElement = GetFirstElement(current);
                result.Add(firstElement);
                
                current = current.Substring(firstElement.Length);

                if (current.StartsWith(','))
                    current = current.Substring(1);

            } while (current.Length > 0);

            return result;
        }

        private string GetFirstElement(string s) {
            if (!s.Contains(','))
                return s;

            var openCounterCounter = 0;

            for (int i=0; i<s.Length; i++) {
                if (openCounterCounter == 0 && s[i]==',') {
                    return s.Substring(0, i);
                }

                if (s[i]=='[') openCounterCounter++;
                if (s[i]==']') openCounterCounter--;
            }

            return s;
        }

        public IList<Element> SubElements { get; }
        public int NumberValue { get; }
        public ElementType Type { get; }

        public override string ToString()
        {
            if (Type == ElementType.Number) {
                return NumberValue.ToString();
            } else {
                var sb = new StringBuilder();
                for (int i=0; i<SubElements.Count; i++) {
                    sb.Append(SubElements[i].ToString());
                    if (i != SubElements.Count-1)
                        sb.Append(",");
                }
                return $"[{sb.ToString()}]";
            }
        }
    }
}
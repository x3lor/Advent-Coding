public class Solution_13_2 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var the2 = new Element("[[2]]");
        var the6 = new Element("[[6]]");

        var listOfAll = Input_13.input
                                .Split('\n')
                                .Where(i => !string.IsNullOrEmpty(i))
                                .Select(i => new Element(i))
                                .Append(the2)
                                .Append(the6)
                                .OrderByDescending(e => e, new Comparer())
                                .ToList();

        var indexOf2 = listOfAll.IndexOf(the2)+1;
        var indexOf6 = listOfAll.IndexOf(the6)+1;

        Console.WriteLine($"done! Sum: {indexOf2 * indexOf6}");
        
    }

    private class Comparer : IComparer<Element>
    {
        public int Compare(Element? x, Element? y)
        {
            if (x == null || y == null)
                return -1;

            return CompareLists(x, y);
        }
    }

    // +1: right order // left side smaller value // left side fewer items
    // -1: wrong order
    private static int CompareLists(Element left, Element right) {

        for(int i=0; i<left.SubElements.Count; i++) {
            var l = left.SubElements[i];

            if (right.SubElements.Count < i+1) {
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
        
            if (l.Type == ElementType.Number) { l = new Element($"[{l.NumberValue}]"); }
            if (r.Type == ElementType.Number) { r = new Element($"[{r.NumberValue}]"); }

            var sublistResult = CompareLists(l, r);

            if (sublistResult < 0) return -1;
            if (sublistResult > 0) return 1;      
        }

        if (left.SubElements.Count == right.SubElements.Count)
            return 0;

        return 1;  // left side out of items
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
            SubElements = SeperateIntoSubElements(init)
                            .Select(sub => new Element(sub))
                            .ToList();
        }

        private IList<string> SeperateIntoSubElements(string s) {
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

        public IList<Element> SubElements { get; }
        public int NumberValue { get; }
        public ElementType Type { get; }

        public override string ToString()
        {
            if (Type == ElementType.Number) {
                return NumberValue.ToString();
            } else {
                return $"[{string.Join(',', SubElements.Select(e => e.ToString()))}]";
            }
        }
    }
}
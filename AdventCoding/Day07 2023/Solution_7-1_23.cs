using System.Diagnostics.CodeAnalysis;

public class Solution_7_1_23 : ISolution
{
    public void run()
    {
        Console.Write("Starting...");

        var hands = Input_7_23.input
                              .Split('\n')
                              .Select(s => new Hand(s))
                              .ToList();

        var sorted = hands.OrderByDescending(x => x, new Comparer()).ToList();

        for (int i=0; i<sorted.Count; i++) {
            sorted[i].SetRank(sorted.Count-i);
        }

        var sum = sorted.Sum(x => (long)x.GetValue());
        
        Console.WriteLine($"Done: {sum}");
    }

    public class Comparer : IComparer<Hand>
    {
        public int Compare(Hand? x, Hand? y)
        {
            //  0: equal
            // -1: x less y
            //  1: x greater y

            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            if (x.HandType > y.HandType) return 1;
            if (x.HandType < y.HandType) return -1;
            
            if (x.CompareNumber > y.CompareNumber) return 1;
            if (x.CompareNumber < y.CompareNumber) return -1;
            return 0;
        }
    }

    public class Hand {

        private static List<char> possibleChars = new List<char> { '2','3','4','5','6','7','8','9','T','J','Q','K','A' };

        public Hand(string init) {
            Cards = init.Substring(0, 5);
            Bid = int.Parse(init.Substring(6));
            HandType = GetHandType(Cards);
            Rank = 0;
            CompareNumber = GetCompareNumber(Cards);
        }

        public string Cards { get; }
        public int Bid { get; }
        public int Rank  {private set; get; }
        public long CompareNumber { get; }

        // 7: Five of a kind
        // 6: Four of a kind
        // 5: Full house
        // 4: Three of Kind
        // 3: Two Pair
        // 2: One Pair
        // 1: High Hard
        public int HandType { get; }

        public void SetRank(int rank) {
            Rank = rank;
        }

        public int GetValue() {
            return Bid*Rank;
        }

        public static long GetCompareNumber(string cards) {
            var value = 0L;
            for (int i=4; i>=0; i--) {
                value += possibleChars.IndexOf(cards[4-i])*(long)Math.Pow(10, i*2);
            }
            return value;
        }

        private static int GetHandType(string cards) {
            var listOfCounts = new List<int>();
            
            foreach (char c in possibleChars) {
                listOfCounts.Add(cards.Count(x => x == c));
            }
            listOfCounts = listOfCounts.OrderByDescending(x => x).ToList();

            if (listOfCounts[0] == 5) return 7;
            if (listOfCounts[0] == 4) return 6;
            if (listOfCounts[0] == 3 && listOfCounts[1] == 2) return 5;
            if (listOfCounts[0] == 3) return 4;
            if (listOfCounts[0] == 2 && listOfCounts[1] == 2) return 3;
            if (listOfCounts[0] == 2) return 2;
            return 1;
        }
    } 
   
}
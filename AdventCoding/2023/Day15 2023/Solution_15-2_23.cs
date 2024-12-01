public class Solution_15_2_23 : ISolution
{
    public void run()
    {
        Console.WriteLine("Starting...");

        var boxes = new List<List<string>>(256);
        for (int i=0; i<256; i++) {
            boxes.Add(new List<string>());
        }

        foreach (var part in Input_15_23.input.Split(',')) {

            if (part[^1] == '-') {
                var toRemove = part[..^1];
                var box = boxes[GetHash(toRemove)];
                var existingBox = box.FirstOrDefault(s => s.StartsWith(toRemove));
                if (existingBox != null) {                
                    box.Remove(existingBox);
                }
            }

            if (part[^2] == '=') {
                var element     = part[..^2];
                var focalLength = part[^1];
                var box = boxes[GetHash(element)];
                var existingBox = box.FirstOrDefault(s => s.StartsWith(element));
                if (existingBox != null) {                    
                    box[box.IndexOf(existingBox)] = element + " " + focalLength; 
                } else {
                    box.Add(element + " " + focalLength);
                }
            }
        }

        var sum=0;
        for (int i=0; i<boxes.Count; i++) {
            for (int j=0; j<boxes[i].Count; j++) {
                sum += (i+1)*(j+1)*int.Parse(boxes[i][j][^1..]);
            }
        }
        Console.WriteLine($"Done! Sum: {sum}");
    }

    private static int GetHash(string s) {
        var hash = 0;
        foreach(var c in s) {
            hash += c;
            hash *= 17;
            hash %= 256;
        }
        return hash;
    }

}
public class Solution_7_1 : ISolution
{
    public void run()
    {
        Console.Write("Starting ... ");

        var baseFolder = new Folder("base");
        BuildDirectory(Input_7.input.Split('\n'), baseFolder);
        baseFolder.CalculateSize();

        var sum = baseFolder.GetAllFoldersAsList()
                            .Where(f => f.Size <= 100000)
                            .Sum(f => f.Size);
       
        Console.WriteLine($"done! Sum: {sum}");
    }   

    private void BuildDirectory(IEnumerable<string> input, Folder baseFolder) {

        var currentFolder = baseFolder;

        foreach(var command in input) {
            
            var parts = command.Split(' ');
            if (command.StartsWith('$')) {

                if (parts[1] == "cd") {
                    if (parts[2] == "..") {
                        currentFolder = currentFolder?.ParentFolder;
                    } else {
                        currentFolder = currentFolder?.GetSubfolder(parts[2]);
                    }
                }

            } else {
                
                if (parts[0] == "dir") {
                    currentFolder?.AddSubFolder(parts[1]);
                } else {
                    currentFolder?.AddFileToFolder(parts[1], int.Parse(parts[0]));
                }
            }
        }
    }

    private class Folder {

        private List<Folder> subfolders;
        private Dictionary<string, int> files;
        private string name;

        public Folder (string name, Folder? parentFolder = null) {
            this.name = name;
            ParentFolder = parentFolder;

            subfolders = new List<Folder>();
            files = new Dictionary<string, int>();
        }

        public int Size { get; private set; } = 0;
        public Folder? ParentFolder { get; }

        public void AddSubFolder(string newSubfolderName) {
            subfolders.Add(new Folder(newSubfolderName, this));
        }

        public void AddFileToFolder(string name, int size) {
            files.Add(name, size);
        }

        public Folder GetSubfolder(string name) {
            return subfolders.First(s => s.name == name);
        }

        public void CalculateSize() {
            Size = subfolders.Sum(s => { s.CalculateSize(); return s.Size; }) +
                   files.Sum(f => f.Value);        
        }

        public IList<Folder> GetAllFoldersAsList() {
            return subfolders.SelectMany(f => f.GetAllFoldersAsList())
                             .Append(this)
                             .ToList();
        }
    } 
}
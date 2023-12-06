using System.Text;

public class VisualizerDay07 {

    public void DoIt(string textfileName, bool withSizes) {

        Console.Write("Starting ... ");

        using(StreamWriter textFile = File.CreateText(textfileName)) {

            var baseFolder = new Folder("base");
            BuildDirectory(Input_7.input.Split('\n'), baseFolder);
            baseFolder.CalculateSize();
            var initialInfo = new CrawlerInfo(baseFolder, 
                                              0, 
                                              false, 
                                              false, 
                                              new Dictionary<int, bool>() {{ 0, true }});

            
            HandleDirectory(initialInfo, textFile, withSizes);
        }

        Console.WriteLine("DONE");
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

        public string Name => name;
        public IReadOnlyList<Folder> SubFolders => subfolders;
        public IReadOnlyList<string> Files => files.Keys.ToList();

        public int Size { get; private set; } = 0;
        public Folder? ParentFolder { get; }

        public int GetFileSize(string name) {
            return files[name];
        }

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

    private class CrawlerInfo {

        public CrawlerInfo(Folder directory, 
                           int depth, 
                           bool directoryOnly, 
                           bool folderIsLastSubDirectory, 
                           Dictionary<int, bool> intendInfo) {

            Directory = directory;
            Depth = depth;
            DirectoryOnly = directoryOnly;
            FolderIsLastSubDirectory = folderIsLastSubDirectory;
            IntendInfo = intendInfo;

            DirectoryHasFiles = directory.Files.Any();
            DirectoryHasSubfolders = directory.SubFolders.Any();
        }

        public Folder Directory { get; }
        public int Depth { get; }
        public bool DirectoryOnly { get; }
        public bool FolderIsLastSubDirectory { get; }
        public Dictionary<int, bool> IntendInfo { get; }

        public bool DirectoryHasFiles { get; }
        public bool DirectoryHasSubfolders { get; }
    }

    private static void HandleDirectory (CrawlerInfo info, StreamWriter textfile, bool showSize)
    {
        if (showSize)
            textfile.WriteLine(GetFolderIntend(info) + info.Directory.Name + $" (Size: {info.Directory.Size})");
        else
            textfile.WriteLine(GetFolderIntend(info) + info.Directory.Name);

        if (!info.DirectoryOnly) {
            var files = info.Directory.Files;
            for (int i=0; i<files.Count; i++) {
                if (showSize)
                    textfile.WriteLine(GetFileIntend(info, i == files.Count-1) + files[i] + $" (Size: {info.Directory.GetFileSize(files[i])})");
                else
                    textfile.WriteLine(GetFileIntend(info, i == files.Count-1) + files[i]);
            }
        }

        var directories = info.Directory.SubFolders;
        for (int i=0; i<directories.Count; i++) {
            HandleDirectory(BuildNextInfo(info, directories[i], i == directories.Count -1), textfile, showSize);
        }
    }

    private static string GetFileIntend(CrawlerInfo info, bool isLastFile) {

        var sb = new StringBuilder();

        for (int i=0; i<info.Depth; i++) {
            if (!info.IntendInfo[i+1])
                sb.Append("    ");
            else
                sb.Append("┃   ");
        }

        sb.Append(info.DirectoryHasSubfolders ? "┃   " : "    ");
        sb.Append(isLastFile ? $"┗━ " : $"┣━ ");

        return sb.ToString();
    }

    private static string GetFolderIntend(CrawlerInfo info) {

        var sb = new StringBuilder();
        for (int i=0; i<info.Depth-1; i++) {
            if (info.IntendInfo[i+1])
                sb.Append("┃   ");
            else
                sb.Append("    ");
        }

        if (info.Depth > 0) {
            sb.Append(info.FolderIsLastSubDirectory ? "┗━━━" : "┣━━━");
            sb.Append(!info.DirectoryHasSubfolders || (!info.DirectoryOnly && !info.DirectoryHasFiles && !info.DirectoryHasSubfolders) 
                        ? "━━ " 
                        : "┳━ ");
        }

        return sb.ToString();     
    }

    private static CrawlerInfo BuildNextInfo(CrawlerInfo oldInfo, Folder targetFolder, bool folderIsLastFolderInDiretory) {
        
        var newIntendInfo = oldInfo.IntendInfo.ToDictionary(entry => entry.Key,
                                                            entry => entry.Value);
        
        newIntendInfo.Add(oldInfo.Depth+1, !folderIsLastFolderInDiretory);
        
        return new CrawlerInfo(targetFolder, 
                               oldInfo.Depth+1, 
                               oldInfo.DirectoryOnly, 
                               folderIsLastFolderInDiretory,
                               newIntendInfo);
    }
}
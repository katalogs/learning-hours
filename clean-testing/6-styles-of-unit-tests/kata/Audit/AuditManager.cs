namespace Audit
{

    public class AuditManager
    {
        private readonly int _maxEntriesPerFile;
        private readonly string _directoryName;
        private readonly IFileSystem _fileSystem;

        public AuditManager(
            int maxEntriesPerFile, 
            string directoryName,
            IFileSystem fileSystem)
        {
            _maxEntriesPerFile = maxEntriesPerFile;
            _directoryName = directoryName;
            _fileSystem = fileSystem;
        }
    
        public void AddRecord(string visitorName, DateTime timeOfVisit)
        {
            string[] filePaths = _fileSystem.GetFiles(_directoryName);
            (int index, string path)[] sorted = SortByIndex(filePaths);
            var newRecord = visitorName + ';' + timeOfVisit;
        
            if (sorted.Length == 0)
            {
                string newFile = Path.Combine(_directoryName, "audit_1.txt");
                _fileSystem.WriteAllText(newFile, newRecord);
            
                return;
            }
        
            (int currentFileIndex, string currentFilePath) = sorted.Last();
            List<string> lines = _fileSystem.ReadAllLines(currentFilePath).ToList();
        
            if (lines.Count < _maxEntriesPerFile)
            {
                lines.Add(newRecord);
                string newContent = string.Join(Environment.NewLine, lines);
                _fileSystem.WriteAllText(currentFilePath, newContent);
            }
            else
            {
                int newIndex = currentFileIndex + 1;
                string newName = $"audit_{newIndex}.txt";
                string newFile = Path.Combine(_directoryName, newName);
                _fileSystem.WriteAllText(newFile, newRecord);
            }
        }

        private static (int index, string path)[] SortByIndex(string[] filePaths)
            => filePaths
                .AsEnumerable()
                .Select((path, index) => (index + 1, path))
                .ToArray();
    }
}

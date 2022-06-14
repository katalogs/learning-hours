# Step by step

![Functional code](../img/functional.png)

## AuditManager
- Start by deleting `IFileSystem` constructor injection and `_directoryName` from `AuditManager`
  - Let your `compiler` guide you
- Change method signature to match what we want to achieve
  - Generate the 2 new types from your IDE 

```c#
public FileUpdate AddRecord(
            FileContent[] files,
            string visitorName, 
            DateTime timeOfVisit)
```

- Adapt the `SortByIndex` method to return a list of `Tuple<int, FileContent>`

```c#
private static (int index, FileContent)[] SortByIndex(FileContent[] files)
    => files
        .AsEnumerable()
        .Select((content, index) => (index + 1, content))
        .ToArray();
```

- Create a new file if no files already by instantiating a `FileUpdate` with new record content inside

```c#
public FileUpdate AddRecord(
            FileContent[] files,
            string visitorName, 
            DateTime timeOfVisit)
{
    (int index, FileContent content)[] sorted = SortByIndex(files);
    var newRecord = visitorName + ';' + timeOfVisit.ToString("yyyy-MM-dd HH:mm:ss");;
    
    if (sorted.Length == 0)
    {
        return new FileUpdate("audit_1.txt", newRecord);
    }
    ...
}

public record FileUpdate(string FileName, string NewContent);
```

- Do the same for the next step -> `Append into existing file` :
```c#
 public FileUpdate AddRecord(
    FileContent[] files,
    string visitorName, 
    DateTime timeOfVisit)
{
    (int index, FileContent content)[] sorted = SortByIndex(files);
    var newRecord = visitorName + ';' + timeOfVisit.ToString("yyyy-MM-dd HH:mm:ss");;
    
    if (sorted.Length == 0)
    {
        return new FileUpdate("audit_1.txt", newRecord);
    }

    (int currentFileIndex, FileContent currentFile) = sorted.Last();
    List<string> lines = currentFile.Lines.ToList();

    if (lines.Count < _maxEntriesPerFile)
    {
        lines.Add(newRecord);
        string newContent = string.Join(Environment.NewLine, lines);
        return new FileUpdate(currentFile.FileName, newContent);
    }
    ...
}

// File content when existing
public record FileContent(string FileName, string[] Lines);
```

- Finish with the case where we need to create a new audit file
  - Think about removing everything related to folders

```c#
public FileUpdate AddRecord(
    FileContent[] files,
    string visitorName, 
    DateTime timeOfVisit)
{
    (int index, FileContent content)[] sorted = SortByIndex(files);
    var newRecord = visitorName + ';' + timeOfVisit.ToString("yyyy-MM-dd HH:mm:ss");;
    
    if (sorted.Length == 0)
    {
        return new FileUpdate("audit_1.txt", newRecord);
    }

    (int currentFileIndex, FileContent currentFile) = sorted.Last();
    List<string> lines = currentFile.Lines.ToList();

    if (lines.Count < _maxEntriesPerFile)
    {
        lines.Add(newRecord);
        string newContent = string.Join(Environment.NewLine, lines);
        return new FileUpdate(currentFile.FileName, newContent);
    }
    else
    {
        int newIndex = currentFileIndex + 1;
        string newName = $"audit_{newIndex}.txt";
        return new FileUpdate(newName, newRecord);
    }
}
```

- We have a first pure version of our `AuditManager`

```c#
namespace Audit
{
    public class AuditManager
    {
        private readonly int _maxEntriesPerFile;

        public AuditManager(int maxEntriesPerFile)
            => _maxEntriesPerFile = maxEntriesPerFile;

        public FileUpdate AddRecord(
            FileContent[] files,
            string visitorName, 
            DateTime timeOfVisit)
        {
            (int index, FileContent content)[] sorted = SortByIndex(files);
            var newRecord = visitorName + ';' + timeOfVisit.ToString("yyyy-MM-dd HH:mm:ss");
            
            if (sorted.Length == 0)
            {
                return new FileUpdate("audit_1.txt", newRecord);
            }
        
            (int currentFileIndex, FileContent currentFile) = sorted.Last();
            List<string> lines = currentFile.Lines.ToList();
        
            if (lines.Count < _maxEntriesPerFile)
            {
                lines.Add(newRecord);
                string newContent = string.Join(Environment.NewLine, lines);
                return new FileUpdate(currentFile.FileName, newContent);
            }
            else
            {
                int newIndex = currentFileIndex + 1;
                string newName = $"audit_{newIndex}.txt";
                return new FileUpdate(newName, newRecord);
            }
        }

        private static (int index, FileContent)[] SortByIndex(FileContent[] files)
            => files
                .AsEnumerable()
                .Select((content, index) => (index + 1, content))
                .ToArray();
    }

    public record FileUpdate(string FileName, string NewContent);

    public record FileContent(string FileName, string[] Lines);
} 
```

- Let's adapt our tests accordingly
```c#
[Fact]
public void A_new_file_is_created_when_the_current_file_overflows()
{
    // Arrange
    var sut = new AuditManager(3);
    var existingFiles = new FileContent[]
    {
        new("audit_1.txt", Array.Empty<string>()),
        new("audit_2.txt", new[] {"Peter;2019-04-06 16:30:00", "Jane;2019-04-06 16:40:00", "Jack;2019-04-06 17:00:00"})
    };

    // Act
    var fileUpdate = sut.AddRecord(existingFiles, "Alice", DateTime.Parse("2019-04-06T18:00:00"));
    
    // Assert
    fileUpdate.Should()
        .Be(new FileUpdate("audit_3.txt", "Alice;2019-04-06 18:00:00"));
}
```

- Let's make some automatic refactoring to improve code readability
```c#
namespace Audit
{
    public class AuditManager
    {
        private readonly int _maxEntriesPerFile;

        private const string FirstFileName = "audit_1.txt";

        public AuditManager(int maxEntriesPerFile)
            => _maxEntriesPerFile = maxEntriesPerFile;

        public FileUpdate AddRecord(
            FileContent[] files,
            string visitorName, 
            DateTime timeOfVisit)
        {
            var sorted = SortByIndex(files);
            var newRecord = NewRecord(visitorName, timeOfVisit);

            if (sorted.Length == 0)
            {
                return CreateFirstFile(newRecord);
            }
        
            (int currentFileIndex, FileContent currentFile) = sorted.Last();
            
            if (currentFile.Lines.Length < _maxEntriesPerFile)
            {
                return AppendToExistingFile(currentFile, newRecord);
            }
            return CreateANewFile(currentFileIndex, newRecord);
        }

        private static FileUpdate CreateFirstFile(string newRecord)
            => new(FirstFileName, newRecord);

        private static FileUpdate AppendToExistingFile(FileContent currentFile, string newRecord)
        {
            List<string> lines = currentFile.Lines.ToList();
            lines.Add(newRecord);
            string newContent = string.Join(Environment.NewLine, lines);
            
            return new FileUpdate(currentFile.FileName, newContent);
        }
        
        private static FileUpdate CreateANewFile(int currentFileIndex, string newRecord)
        {
            int newIndex = currentFileIndex + 1;
            string newName = $"audit_{newIndex}.txt";
            
            return new FileUpdate(newName, newRecord);
        }

        private static string NewRecord(string visitorName, DateTime timeOfVisit)
            => visitorName + ';' + timeOfVisit.ToString("yyyy-MM-dd HH:mm:ss");

        private static (int index, FileContent)[] SortByIndex(FileContent[] files)
            => files
                .AsEnumerable()
                .Select((content, index) => (index + 1, content))
                .ToArray();
    }
}
```
- Simplify checks with `Pattern matching`
```c#
namespace Audit
{
    public class AuditManager
    {
        private readonly int _maxEntriesPerFile;

        private const string FirstFileName = "audit_1.txt";

        public AuditManager(int maxEntriesPerFile)
            => _maxEntriesPerFile = maxEntriesPerFile;

        public FileUpdate AddRecord(
            FileContent[] files,
            string visitorName, 
            DateTime timeOfVisit)
        {
            var sorted = SortByIndex(files);
            var newRecord = NewRecord(visitorName, timeOfVisit);

            return sorted switch
            {
                {Length: 0} => CreateFirstFile(newRecord),
                _ => CreateNewFileOrUpdate(sorted.Last(), newRecord)
            };
        }

        private static FileUpdate CreateFirstFile(string newRecord)
            => new(FirstFileName, newRecord);
        
        private FileUpdate CreateNewFileOrUpdate((int, FileContent) currentFile, string newRecord)
        {
            var (fileIndex, fileContent) = currentFile;
            return fileContent.Lines.Length < _maxEntriesPerFile 
                ? AppendToExistingFile(fileContent, newRecord) 
                : CreateANewFile(fileIndex, newRecord);
        }

        private static FileUpdate AppendToExistingFile(FileContent currentFile, string newRecord)
        {
            List<string> lines = currentFile.Lines.ToList();
            lines.Add(newRecord);
            string newContent = string.Join(Environment.NewLine, lines);
            
            return new FileUpdate(currentFile.FileName, newContent);
        }
        
        private static FileUpdate CreateANewFile(int currentFileIndex, string newRecord)
        {
            int newIndex = currentFileIndex + 1;
            string newName = $"audit_{newIndex}.txt";
            
            return new FileUpdate(newName, newRecord);
        }

        private static string NewRecord(string visitorName, DateTime timeOfVisit)
            => visitorName + ';' + timeOfVisit.ToString("yyyy-MM-dd HH:mm:ss");

        private static (int index, FileContent)[] SortByIndex(FileContent[] files)
            => files
                .AsEnumerable()
                .Select((content, index) => (index + 1, content))
                .ToArray();
    }
}
```

- We have lost some features in the battle
  - Let's implement the `Persister` class 

## Persister (Mutable shell) - Optional
- The `Persister` is responsible for
  - accessing files from a given Directory
  - applying update instruction (a.k.a update the file content)

```c#
namespace Audit
{
    public class Persister
    {
        public FileContent[] ReadDirectory(string directoryName)
            => Directory
                .GetFiles(directoryName)
                .Select(x => new FileContent(
                    Path.GetFileName(x),
                    File.ReadAllLines(x)))
                .ToArray();

        public void ApplyUpdate(string directoryName, FileUpdate update)
            => File.WriteAllText(
                Path.Combine(directoryName, update.FileName),
                update.NewContent);
    }
}
```

## AddRecordUseCase
We need to have some glue to control the flow of our application.
Let's create a `UseCase` for that :

```c#
public class AddRecordUseCase
{
    private readonly string _directoryName;
    private readonly AuditManager _auditManager;
    private readonly Persister _persister;
    
    public AddRecordUseCase(string directoryName, int maxEntriesPerFile)
    {
        _directoryName = directoryName;
        _auditManager = new AuditManager(maxEntriesPerFile);
        _persister = new Persister();
    }
    public void Handle(string visitorName, DateTime timeOfVisit)
    {
        FileContent[] files = _persister.ReadDirectory(_directoryName);
        FileUpdate update = _auditManager.AddRecord(files, visitorName, timeOfVisit);
        
        _persister.ApplyUpdate(_directoryName, update);
    }
}
```
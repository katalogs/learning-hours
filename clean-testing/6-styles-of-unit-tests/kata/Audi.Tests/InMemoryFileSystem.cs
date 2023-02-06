using Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audi.Tests
{
    public class InMemoryFileSystem : IFileSystem
    {
        private const string DirectoryName = "audits";
        public Dictionary<string, IEnumerable<string>> ListFiles = new Dictionary<string, IEnumerable<string>>();

        public void AddFiles()
        {
            ListFiles.Add("audit_1.txt", new List<string>());
            ListFiles.Add("audit_2.txt", new List<string>
            {
                "Peter;2019-04-06 16:30:00",
                "Jane;2019-04-06 16:40:00",
                "Jack;2019-04-06 17:00:00"
            });
            ListFiles.Add("audit_3.txt", new List<string>());
        }

        public string[] GetFiles(string directoryName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ReadAllLines(string filePath)
        {
            throw new NotImplementedException();
        }

        public void WriteAllText(string filePath, string content)
        {
            ListFiles.Add(filePath, content);
        }
    }
}

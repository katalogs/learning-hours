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
        public void AddFiles()
        {

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
            throw new NotImplementedException();
        }
    }
}

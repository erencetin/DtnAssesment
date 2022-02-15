using DtnAssesment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtnAssesment.Infrastructure
{
    public class FileSystemWrapper : IFileSystem
    {
        public IEnumerable<string> ReadLines(string filePath)
        {
            return File.ReadLines(filePath);
        }

        public string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}

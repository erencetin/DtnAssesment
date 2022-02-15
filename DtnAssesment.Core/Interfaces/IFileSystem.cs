using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtnAssesment.Core.Interfaces
{
    public interface IFileSystem
    {
        IEnumerable<string> ReadLines(string filePath);

        string ReadAllText(string filePath);
    }
}

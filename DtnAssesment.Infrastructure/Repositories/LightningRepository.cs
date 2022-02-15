using DtnAssesment.Core.Interfaces;
using DtnAssesment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DtnAssesment.Infrastructure.Repositories
{
    public class LightningRepository : ILightningRepository
    {
        IFileSystem _fileSystem;
        public LightningRepository(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public IEnumerable<Lightning> GetLightnings()
        {
            return _fileSystem.ReadLines($@"{Directory.GetCurrentDirectory()}\Data\lightning.txt")
                .Select(
                (l) => JsonSerializer.Deserialize<Lightning>(l, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                );
        }
    }
}

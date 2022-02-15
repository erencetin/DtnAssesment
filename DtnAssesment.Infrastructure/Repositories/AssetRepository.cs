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
    public class AssetRepository : IAssetRepository
    {
        IFileSystem _fileSystem;
        public AssetRepository(IFileSystem fileSystem)
        {
             _fileSystem = fileSystem;
        }
        public IEnumerable<Asset> GetAllAssets()
        {
            var jsonString = _fileSystem.ReadAllText($@"{Directory.GetCurrentDirectory()}\Data\assets.json");
            return JsonSerializer.Deserialize<List<Asset>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}

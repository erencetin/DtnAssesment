using DtnAssesment.Core.Interfaces;
using DtnAssesment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtnAssesment.Infrastructure.Services
{
    public class AssetAlertService : IAssetAlertService
    {
        private readonly ILightningRepository _lightningRepository;
        private readonly IQuadkeyConverterService _quadkeyConverterService;
        private readonly IAssetRepository _assetRepository;
        private readonly ILogService _logService;
        public AssetAlertService(ILightningRepository lightningRepository, IAssetRepository assetRepository, IQuadkeyConverterService quadkeyConverterService, ILogService logService)
        {
            _lightningRepository = lightningRepository;
            _quadkeyConverterService = quadkeyConverterService;
            _assetRepository = assetRepository;
            _logService = logService;
        }

        public void AlertAssets()
        {
            var lightnings = _lightningRepository.GetLightnings().Where(l => l.FlashType != 9);
            var assets = _assetRepository.GetAllAssets().ToList();
            var assetDictionary = assets.ToDictionary(x => x.QuadKey);
            var alertedAssets = new HashSet<string>();

            foreach (var light in lightnings)
            {
                var quadKey = _quadkeyConverterService.ConvertToQuadkey(light.Latitude, light.Longitude);
                if (!alertedAssets.Contains(quadKey) && assetDictionary.ContainsKey(quadKey))
                {
                    var asset = assetDictionary[quadKey];
                    _logService.Log($"lightning alert for {asset.AssetOwner}:{asset.AssetName}");
                    alertedAssets.Add(quadKey);
                }
            }

        }
    }
}

using DtnAssesment.Core.Interfaces;
using DtnAssesment.Core.Models;
using DtnAssesment.Infrastructure.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtnAssesment.Tests.Services
{
    public class AssetAlertServiceTests
    {
        [Test]
        public void AlertAssetShouldLogWritingAlertMessageCorrectly()
        {
            var lightningRepositoryMock = new Mock<ILightningRepository>();
            var assetRepositoryMock = new Mock<IAssetRepository>();
            var converterMock = new Mock<IQuadkeyConverterService>();
            var logServiceMock = new Mock<ILogService>();
            var expectedLightnings = new List<Lightning>
            {
                new Lightning { FlashType = 1, Latitude = 32.9862554, Longitude = -98.3471457 },
                new Lightning { FlashType = 0, Latitude = 33.7412419, Longitude = -96.6794229 },
                new Lightning { FlashType = 1, Latitude = 33.7412419, Longitude = -96.6794229 },
                new Lightning { FlashType = 9}
            };

            var expectedAssets = new List<Asset>
            {
                new Asset { AssetName = "Microsoft", AssetOwner = "Bill", QuadKey = "023112320021"},
                new Asset { AssetName = "Apple", AssetOwner = "Steve", QuadKey = "023112320003"},
                new Asset { AssetName = "Meta", AssetOwner = "Mark", QuadKey = "023112310322"},
            };

            lightningRepositoryMock.Setup(x => x.GetLightnings()).Returns(expectedLightnings);
            assetRepositoryMock.Setup(x => x.GetAllAssets()).Returns(expectedAssets);
            converterMock.Setup(x => x.ConvertToQuadkey(32.9862554, -98.3471457)).Returns("023112320021");
            converterMock.Setup(x => x.ConvertToQuadkey(33.7412419, -96.6794229)).Returns("023112310322");

            var service = new AssetAlertService(lightningRepositoryMock.Object, assetRepositoryMock.Object, converterMock.Object, logServiceMock.Object);
            service.AlertAssets();

            logServiceMock.Verify( x => x.Log(It.IsAny<string>()), Times.AtLeast(2));
        }
    }
}

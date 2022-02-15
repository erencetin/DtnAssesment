using DtnAssesment.Core.Interfaces;
using DtnAssesment.Core.Models;
using DtnAssesment.Infrastructure.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DtnAssesment.Tests.Repositories
{
    public class AssetRepositoryTests
    {
        [Test]
        public void GetAllAssetsShouldReturnProperAssetList()
        {
            var expectedJsonString = "[{\"assetName\" : \"A\"},{\"assetName\" : \"B\"}]" ;
            var fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(f => f.ReadAllText(It.IsAny<string>())).Returns(expectedJsonString);
            var repo = new AssetRepository(fileSystemMock.Object);
            var actualResult = repo.GetAllAssets().ToList();

            Assert.AreEqual(2, actualResult.Count());
            Assert.AreEqual("A", actualResult[0].AssetName);
            Assert.AreEqual("B", actualResult[1].AssetName);

        }
    }
}

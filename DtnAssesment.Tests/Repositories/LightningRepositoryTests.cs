using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using DtnAssesment.Core.Interfaces;
using DtnAssesment.Infrastructure.Repositories;

namespace DtnAssesment.Tests.Repositories
{
    public class LightningRepositoryTests
    {
        [Test]
        public void GetLightningsShouldReturnProperJsonStrings()
        {
            var expectedLines = new List<string> { "{\"flashType\" : 1}", "{\"flashType\" : 2}" };
            var fileSystemMock = new Mock<IFileSystem>();
            fileSystemMock.Setup(f => f.ReadLines(It.IsAny<string>())).Returns(expectedLines);
            var repo = new LightningRepository(fileSystemMock.Object);
            var actualResult = repo.GetLightnings().ToList();

            Assert.AreEqual(2, actualResult.Count());
            Assert.AreEqual(1, actualResult[0].FlashType);
            Assert.AreEqual(2, actualResult[1].FlashType);

        }
    }
}

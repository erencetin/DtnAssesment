using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using DtnAssesment.Infrastructure.Services;
using DtnAssesment.Core.Interfaces;

namespace DtnAssesment.Tests.Services
{
    public class QuadkeyConverterServiceTests
    {
        [TestCase(32.9862554, -98.3471457, "023112320021")]
        [TestCase(32.9905308, -98.34038, "023112320003")]
        [TestCase(33.7412419, -96.6794229, "023112310322")]
        public void ConvertQuadkeyShouldReturnCorrectQuadkeyWhenInputIsProper(double lat, double longt, string expectedQuadkey)
        {
            var converterService = new QuadkeyConverterService(new Mock<ILogService>().Object);
            var actualQuadkey = converterService.ConvertToQuadkey(lat, longt);
            
            Assert.AreEqual(expectedQuadkey, actualQuadkey);
        }

        [TestCase(32.9862554, 199, $"Longitude has been clipped : 199 => 180")]
        [TestCase(-95, -96.6794229, $"Latitude has been clipped : -95 => -85.05112878")]
        public void ConvertQuadkeyShouldLogWhenInputOutOfRange(double lat, double longt, string expectedLogMessage)
        {
            var logMock = new Mock<ILogService>();
            var converterService = new QuadkeyConverterService(logMock.Object);
            var actualQuadkey = converterService.ConvertToQuadkey(lat, longt);
            var result = converterService.ConvertToQuadkey(lat, longt);
            logMock.Verify(x => x.Log(expectedLogMessage));
        }
    }
}

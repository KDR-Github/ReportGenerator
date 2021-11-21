using ReportGenerator.Models;
using System.IO;
using Xunit;

namespace TestProject1
{
    public class ISINInfoTests
    {
        [Fact]
        public void SetPriceMultiplier_ValidInput_ReturnsValidNumber()
        {
            string algoParam = "NotionalCurr:EUR|;PriceMultiplier:20.0|;UnderlInstCode:DE0001234567|";

            ISINInfo iSINInfo= new ISINInfo();
            iSINInfo.SetPriceMultiplier(algoParam);

            Assert.Equal(20.0, iSINInfo.PriceMultiplier);
        }

        [Fact]
        public void SetPriceMultiplier_PriceMultiplierNotFound_ThrowsException()
        {
            string algoParam = "NotionalCurr:EUR|;UnderlInstCode:DE0001234567|";

            ISINInfo iSINInfo = new ISINInfo();
            
            var ex = Assert.Throws<InvalidDataException>(() => iSINInfo.SetPriceMultiplier(algoParam));

            Assert.Contains("PriceMultiplier in Algo Param is not found", ex.Message);
        }

        [Fact]
        public void SetPriceMultiplier_PriceMultiplierNotANumber_ThrowsException()
        {
            string algoParam = "NotionalCurr:EUR|;PriceMultiplier:20A|;UnderlInstCode:DE0001234567|";

            ISINInfo iSINInfo = new ISINInfo();

            var ex = Assert.Throws<InvalidDataException>(() => iSINInfo.SetPriceMultiplier(algoParam));

            Assert.Contains("PriceMultiplier in Algo Param  is not an Number", ex.Message);
        }
    }
}

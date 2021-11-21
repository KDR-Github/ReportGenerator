using System.IO;
using Xunit;

namespace TestProject1
{
    public class ReportGeneratorTests
    {
        private readonly string SampleFilesPath;

        public ReportGeneratorTests()
        {
            SampleFilesPath = Path.GetFullPath(@".\TestFiles\");
        }

        [Fact]
        public void SetPriceMultiplier_ValidInput_ReturnsValidNumber()
        {
            var inputPath = Path.Combine(SampleFilesPath, "InputSample_ValidFile.csv");
            var outputPath = Path.Combine(SampleFilesPath, "OutputSample02.csv");

            ReportGenerator.ReportGenerator reportGenerator = new ReportGenerator.ReportGenerator();
            reportGenerator.GenerateReport(inputPath, outputPath);

            Assert.True(File.Exists(outputPath));

            File.Delete(outputPath);
        }
    }
}


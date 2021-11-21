using ReportGenerator.Csv;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Xunit;

namespace ReportGenerator.Tests
{
    public class CsvProcessorTests
    {
        private readonly string SampleFilesPath;

        public CsvProcessorTests()
        {
            SampleFilesPath = Path.GetFullPath(@".\TestFiles\");
        }

        [Fact]
        public void ReadCsvToDataTable_ValidFile_ReturnsValidDataTable()
        {
            var fileName = $"InputSample_ValidFile.csv";
            var path = Path.Combine(SampleFilesPath, fileName);

            CsvProcessor csvProcessor = new CsvProcessor();

            var dataTable = csvProcessor.ReadCsvToDataTable(path, 1);

            Assert.True(dataTable.Columns.Contains("ISIN"));
            Assert.True(dataTable.Columns.Contains("Venue"));
            Assert.True(dataTable.Columns.Contains("CFICode"));
            Assert.True(dataTable.Columns.Contains("AlgoParams"));
        }

        [Fact]
        public void ReadCsvToDataTable_InvalidFile_ThrowsException()
        {
            var fileName = $"InputSample_InValidFile.csv";
            var path = Path.Combine(SampleFilesPath, fileName);

            CsvProcessor csvProcessor = new CsvProcessor();

            Assert.Throws<FileNotFoundException>(() => csvProcessor.ReadCsvToDataTable(path, 1));
        }

        [Fact]
        public void WriteToCsvWithDataTable_ValidFile_ThrowsException()
        {
            var dataTable = new DataTable();

            DataColumn dtColumn;
            DataRow myDataRow;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "ISIN";
            dataTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Venue";
            dataTable.Columns.Add(dtColumn);

            myDataRow = dataTable.NewRow();
            myDataRow["ISIN"] = "ASD";
            myDataRow["Venue"] = "LANJ";
            dataTable.Rows.Add(myDataRow);

            myDataRow = dataTable.NewRow();
            myDataRow["ISIN"] = "DDFG";
            myDataRow["Venue"] = "LANJA";
            dataTable.Rows.Add(myDataRow);

            var fileName = $"OutputSample.csv";
            var path = Path.Combine(SampleFilesPath, fileName);

            CsvProcessor csvProcessor = new CsvProcessor();

            csvProcessor.WriteToCsv(path, dataTable);

            Assert.True(File.Exists(path));

            File.Delete(path);
        }

        [Fact]
        public void WriteToCsvWithObjectList_ValidFile_ThrowsException()
        {
            var records = new List<object>
            {
                new { ISIN = "ASD", Venue = "LANJ" },
                new { ISIN = "DDFG", Venue = "LANJA" }
            };

            var fileName = $"OutputSample01.csv";
            var path = Path.Combine(SampleFilesPath, fileName);

            CsvProcessor csvProcessor = new CsvProcessor();

            csvProcessor.WriteToCsv(path, records);

            Assert.True(File.Exists(path));

            File.Delete(path);
        }
    }
}

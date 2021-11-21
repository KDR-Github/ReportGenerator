using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ReportGenerator.Models;
using ReportGenerator.Csv;

namespace ReportGenerator
{
    public class ReportGenerator
    {
        public void GenerateReport(string inputPath, string outputPath)
        {
            try
            {
                //Read Csv
                CsvProcessor csvProcessor = new CsvProcessor();
                var dataTable = csvProcessor.ReadCsvToDataTable(inputPath, 1);

                //Extract Data
                var records = PrepareReport(dataTable);

                //Write Csv
                var recordsObj = records.Select(p =>
                                        new
                                        {
                                            ISIN = p.ISIN,
                                            CFICode = p.CFICode,
                                            Venue = p.Venue,
                                            ContractSize = p.PriceMultiplier
                                        }).Cast<object>().ToList();

                csvProcessor.WriteToCsv(outputPath, recordsObj);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR  : File Not Processes : {ex.Message} \n {ex}");
            }
        }

        private List<ISINInfo> PrepareReport(DataTable dataTable)
        {
            var records = new List<ISINInfo>();

            foreach (DataRow row in dataTable.Rows)
            {
                var record = new ISINInfo
                {
                    ISIN = row["ISIN"].ToString(),
                    CFICode = row["CFICode"].ToString(),
                    Venue = row["Venue"].ToString()
                };
                record.SetPriceMultiplier(row["AlgoParams"].ToString());
                records.Add(record);
            }

            return records;
        }
    }
}

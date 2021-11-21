using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ReportGenerator.Csv
{
    public class CsvProcessor
    {
        public DataTable ReadCsvToDataTable(string path, int startLinesToIgnore)
        {
            var dataTable = new DataTable();

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //skip initial lines before reading
                Enumerable.Repeat(1, startLinesToIgnore).Select(_ => csv.Read()).ToList();

                using (var dr = new CsvDataReader(csv))
                {
                    dataTable.Load(dr);
                }
            }

            return dataTable;
        }

        public void WriteToCsv(string path, DataTable dataTable)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                foreach (DataColumn column in dataTable.Columns)
                    csv.WriteField(column.ColumnName);
                csv.NextRecord();

                foreach (DataRow row in dataTable.Rows)
                {
                    for (var i = 0; i < dataTable.Columns.Count; i++)
                        csv.WriteField(row[i]);
                    csv.NextRecord();
                }
            }
        }

        public void WriteToCsv(string path, List<Object> data)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(data);
            }
        }
    }
}

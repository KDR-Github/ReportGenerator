using System.IO;
using System.Linq;
using ReportGenerator.Constants;

namespace ReportGenerator.Models
{
    public class ISINInfo
    {
        public string ISIN { get; set; }
        public string CFICode { get; set; }
        public string Venue { get; set; }
        public double? PriceMultiplier { get; set; }

        public void SetPriceMultiplier(string algoParam)
        {
            string[] columns = algoParam.Split(ReportGeneratorConstants.colDelimStg, System.StringSplitOptions.RemoveEmptyEntries);

            var column = columns.FirstOrDefault(item => columns
                                    .Any(columns => item.Contains("PriceMultiplier")));

            if (column == null) { throw new InvalidDataException($"PriceMultiplier in Algo Param is not found"); }

            var priceMultiplier = column.Split(ReportGeneratorConstants.delimChar);

            double priceMultiplierVal;
            if (!double.TryParse(priceMultiplier[1], out priceMultiplierVal))
            {
                throw new InvalidDataException($"PriceMultiplier in Algo Param  is not an Number");
            }

            PriceMultiplier = priceMultiplierVal;
        }
    }
}

using System;
using System.IO;

namespace ReportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter File Path OR Drag and Drop csv File in Console : ");

            var inputPath = Console.ReadLine();

            inputPath = inputPath.Replace('"', ' ').Trim();

            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Invalid File path");
                return;
            }

            var currentPath = Path.GetFullPath(@".\");
            var fileName = $"Report_{DateTime.Now.ToString("yyyyMMdd_HHmm")}.csv";
            var outputPath = Path.Combine(currentPath, fileName);


            ReportGenerator reportGenerator = new ReportGenerator();
            reportGenerator.GenerateReport(inputPath, outputPath);

            Console.WriteLine($"Report path : {outputPath}");
            Console.WriteLine("Report Genererated Sucessfully!!!");
        }
    }
}

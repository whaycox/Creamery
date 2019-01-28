using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Curds.Infrastructure.Parsing.CSV.TestFiles
{
    public static class Files
    {
        public const string LargeCSV = "LargeCSV.csv";
        public const string ANSICSV = "WindowsANSI.csv";
        public const string UTF8CSV = "UTF8.csv";
        public const string UCS2LECSV = "UCS2LE.csv";
        public const string UCS2BECSV = "UCS2BE.csv";

        public static Stream PrepareStream(string fileName) => TestingFileStream(fileName);
        private static Stream TestingFileStream(string fileName) => File.OpenRead(TestingFilePath(fileName));
        private static string TestingFilePath(string fileName) => Path.Combine(Environment.CurrentDirectory, nameof(Parsing), nameof(CSV), nameof(TestFiles), fileName);
    }
}

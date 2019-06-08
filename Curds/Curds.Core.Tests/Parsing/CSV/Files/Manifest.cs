using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Curds.Parsing.CSV.Files
{
    public static class Manifest
    {
        private static string FilePath(string fileName) => Path.Combine(Environment.CurrentDirectory, nameof(Parsing), nameof(CSV), nameof(Files), fileName);

        public static Stream LargeStream => File.OpenRead(Large);
        public static string Large => FilePath($"{nameof(Large)}.csv");
    }
}

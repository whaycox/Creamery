using System;
using System.Collections.Generic;
using System.Text;

namespace Queso.Domain
{
    using Enumerations;

    public class Character
    {
        public static int Signature = -1437226411;

        public FileData File { get; }

        public string Name { get; set; }
        public Class Class { get; set; }

        public Character()
        {
            File = new FileData();
        }

        public override string ToString() => $"{Name} ({Class})";

        public class FileData
        {
            public int Signature { get; set; }
            public VersionID VersionID { get; set; }
            public int Size { get; set; }
            public int Checksum { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Queso.Domain
{
    using Enumerations;

    public class Character
    {
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
            public int VersionID { get; set; }
            public int Size { get; set; }
            public int Checksum { get; set; }
        }
    }
}

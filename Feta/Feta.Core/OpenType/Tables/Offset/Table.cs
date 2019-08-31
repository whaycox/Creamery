using System.Collections.Generic;

namespace Feta.OpenType.Tables.Offset
{
    using Enumerations;
    using OpenType.Domain;

    public class Table : BaseTable
    {
        public uint SfntVersion { get; set; }
        public ushort NumberOfTables { get; set; }
        public ushort SearchRange { get; set; }
        public ushort EntrySelector { get; set; }
        public ushort RangeShift { get; set; }

        public List<TableRecord> Records { get; } = new List<TableRecord>();

        public override string ToString() => $"{SfntVersion}: {NumberOfTables} tables";
    }
}

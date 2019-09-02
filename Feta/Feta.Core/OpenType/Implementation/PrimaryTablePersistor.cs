using System;
using System.Linq;

namespace Feta.OpenType.Implementation
{
    using Abstraction;
    using Domain;
    using Tables.Offset.Domain;

    public abstract class PrimaryTablePersistor : BaseTablePersistor
    {
        public abstract string Tag { get; }
    }

    public abstract class PrimaryTablePersistor<T> : PrimaryTablePersistor
        where T : PrimaryTable
    {
        public override void Read(IFontReader reader)
        {
            uint startingOffset = reader.CurrentOffset;
            T toAdd = ReadTable(startingOffset, reader);
            RegisterTablePaddingIfNecessary(startingOffset, reader, toAdd);
            reader.Tables.Add(toAdd);
        }
        protected abstract T ReadTable(uint startingOffset, IFontReader reader);
        private void RegisterTablePaddingIfNecessary(uint startingOffset, IFontReader reader, T table)
        {
            var offsetTable = reader.Tables.Retrieve<OffsetTable>();
            var tableRecord = offsetTable.Records.First(r => r.Tag == Tag);

            uint lastOffset = startingOffset + tableRecord.Length;
            uint padding = lastOffset % PrimaryTable.RoundBytes;
            if (padding > 0)
            {
                table.PaddedBytes = padding;
                reader.Offsets.RegisterParser(lastOffset, ConsumePadding);
            }
        }
        private void ConsumePadding(IFontReader reader)
        {
            T table = reader.Tables.Retrieve<T>();
            for (uint i = 0; i < table.PaddedBytes; i++)
                if (reader.ReadByte() != 0x00)
                    throw new FormatException("Found non-null bytes in empty padding");
        }

        public override void Write(IFontWriter writer, BaseTable table) => Write(writer, table as T);
        protected abstract void Write(IFontWriter writer, T table);
    }
}

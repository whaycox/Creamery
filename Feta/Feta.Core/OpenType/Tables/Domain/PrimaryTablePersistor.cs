using System;
using System.Linq;

namespace Feta.OpenType.Tables.Domain
{
    using Abstraction;
    using OpenType.Abstraction;
    using OpenType.Domain;

    public abstract class PrimaryTablePersistor<T> : ITablePersistor<T>
        where T : PrimaryTable
    {
        public void Read(IFontReader reader)
        {
            uint startingOffset = reader.CurrentOffset;
            T toAdd = ReadTable(startingOffset, reader);
            RegisterTablePaddingIfNecessary(startingOffset, reader, toAdd);
            reader.Tables.Add(toAdd);
        }
        protected abstract T ReadTable(uint startingOffset, IFontReader reader);
        private void RegisterTablePaddingIfNecessary(uint startingOffset, IFontReader reader, T table)
        {
            var offsetTable = reader.Tables.Retrieve<Offset.Table>();
            var tableRecord = offsetTable.Records.First(r => r.Tag == table.Tag);

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

        public abstract void Write(IFontWriter writer, T table);
    }
}

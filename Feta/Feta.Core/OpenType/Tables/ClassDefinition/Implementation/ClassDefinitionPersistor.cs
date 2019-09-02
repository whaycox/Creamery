﻿using System;
using System.Linq;

namespace Feta.OpenType.Tables.ClassDefinition.Implementation
{
    using Domain;
    using OpenType.Abstraction;
    using OpenType.Domain;
    using OpenType.Implementation;

    public abstract class ClassDefinitionPersistor<T> : SubtablePersistor<T, ClassDefinitionTable>
        where T : PrimaryTable
    {
        protected override ClassDefinitionTable ReadSubtable(IFontReader reader)
        {
            ClassDefinitionTable toReturn = new ClassDefinitionTable();
            toReturn.ClassFormat = reader.ReadUInt16();
            switch (toReturn.ClassFormat)
            {
                case 1:
                    toReturn = ReadFormatOne(reader, toReturn);
                    break;
                case 2:
                    toReturn = ReadFormatTwo(reader, toReturn);
                    break;
                default:
                    throw new FormatException($"Unexpected {nameof(toReturn.ClassFormat)}: {toReturn.ClassFormat}");
            }
            return toReturn;
        }
        private ClassDefinitionTable ReadFormatOne(IFontReader reader, ClassDefinitionTable table)
        {
            table.StartGlyphID = reader.ReadUInt16();
            table.GlyphCount = reader.ReadUInt16();
            table.ClassArrayValues = new ushort[table.GlyphCount];
            for (int i = 0; i < table.GlyphCount; i++)
                table.ClassArrayValues[i] = reader.ReadUInt16();

            return table;
        }
        private ClassDefinitionTable ReadFormatTwo(IFontReader reader, ClassDefinitionTable table)
        {
            table.ClassRangeCount = reader.ReadUInt16();
            table.ClassRangeRecords = new ClassRangeRecord[table.ClassRangeCount];
            for (int i = 0; i < table.ClassRangeCount; i++)
                table.ClassRangeRecords[i] = ReadClassRangeRecord(reader);

            return table;
        }
        private ClassRangeRecord ReadClassRangeRecord(IFontReader reader)
        {
            ClassRangeRecord toReturn = new ClassRangeRecord();
            toReturn.StartGlyphID = reader.ReadUInt16();
            toReturn.EndGlyphID = reader.ReadUInt16();
            toReturn.Class = reader.ReadUInt16();

            return toReturn;
        }

        protected override void Write(IFontWriter writer, ClassDefinitionTable table)
        {
            writer.WriteUInt16(table.ClassFormat);
            switch (table.ClassFormat)
            {
                case 1:
                    WriteFormatOne(writer, table);
                    break;
                case 2:
                    WriteFormatTwo(writer, table);
                    break;
                default:
                    throw new FormatException($"Unexpected {nameof(table.ClassFormat)}: {table.ClassFormat}");
            }
        }
        private void WriteFormatOne(IFontWriter writer, ClassDefinitionTable table)
        {
            writer.WriteUInt16(table.StartGlyphID);
            writer.WriteUInt16(table.GlyphCount);
            for (int i = 0; i < table.GlyphCount; i++)
                writer.WriteUInt16(table.ClassArrayValues[i]);
        }
        private void WriteFormatTwo(IFontWriter writer, ClassDefinitionTable table)
        {
            writer.WriteUInt16(table.ClassRangeCount);
            foreach (ClassRangeRecord record in table.ClassRangeRecords.OrderBy(r => r.StartGlyphID))
                WriteClassRangeRecord(writer, record);
        }
        private void WriteClassRangeRecord(IFontWriter writer, ClassRangeRecord record)
        {
            writer.WriteUInt16(record.StartGlyphID);
            writer.WriteUInt16(record.EndGlyphID);
            writer.WriteUInt16(record.Class);
        }
    }
}
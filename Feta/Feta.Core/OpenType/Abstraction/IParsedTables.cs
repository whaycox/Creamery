using System;

namespace Feta.OpenType.Abstraction
{
    using Domain;

    public delegate IParsedTables TableParseDelegate(FontReader reader, IParsedTables parsedTables);

    public interface IParsedTables
    {
        BaseTable this[Type index] { get; }

        T Retrieve<T>() where T : BaseTable;
        void Add<T>(T table) where T : BaseTable;

        void Register(uint offset, TableParseDelegate parseDelegate);
        void ParseCurrentTable(FontReader reader);
    }
}

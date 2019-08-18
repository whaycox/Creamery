using System;
using System.Collections.Generic;

namespace Feta.OpenType.Tables.Mock
{
    using OpenType.Abstraction;
    using OpenType.Domain;

    public class IPersistorCollection : Abstraction.IPersistorCollection
    {
        public List<string> TagsRetrieved = new List<string>();
        public TableParseDelegate RetrieveParser(string tag)
        {
            TagsRetrieved.Add(tag);
            return MockParse;
        }

        public int TablesParsed = 0;
        private IParsedTables MockParse(FontReader reader, IParsedTables parsedTables)
        {
            TablesParsed++;
            return parsedTables;
        }
    }
}

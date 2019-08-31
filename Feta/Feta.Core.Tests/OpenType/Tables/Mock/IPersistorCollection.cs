using System;
using System.Collections.Generic;

namespace Feta.OpenType.Tables.Mock
{
    using OpenType.Abstraction;
    using OpenType.Domain;
    using OpenType.Implementation;

    public class IPersistorCollection : Abstraction.IPersistorCollection
    {
        public List<string> TagsRetrieved = new List<string>();
        public TableParseDelegate RetrieveParser(string tag)
        {
            TagsRetrieved.Add(tag);
            return MockParse;
        }

        public int TablesParsed = 0;
        public void MockParse(IFontReader reader)
        {
            TablesParsed++;
        }
    }
}

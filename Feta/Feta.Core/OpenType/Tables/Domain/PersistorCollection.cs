using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Feta.OpenType.Tables.Domain
{
    using OpenType.Domain;
    using OpenType.Abstraction;
    using Abstraction;

    public class PersistorCollection : IPersistorCollection
    {
        private Offset.Persistor Offset { get; }
        private Gdef.Persistor Gdef { get; } = new Gdef.Persistor();

        public PersistorCollection()
        {
            Offset = new Offset.Persistor(this);
        }

        public TableParseDelegate RetrieveParser(string tag)
        {
            switch (tag)
            {
                case Tables.Gdef.Table.Tag:
                    return Gdef.Read;
                default:
                    throw new InvalidOperationException($"Unsupported table {tag}");
            }
        }
    }
}

using System;
using System.IO;

namespace Feta.OpenType.Domain
{
    using Implementation;

    public class Font
    {
        private static PersistorCollection Persistors { get; } = new PersistorCollection();

        public static Font Read(string path)
        {
            using (FontReader reader = new FontReader(File.OpenRead(path)))
            {
                Persistors.Offset.Read(reader);
                while (!reader.IsConsumed)
                {
                    var parseDelegate = reader.Offsets.RetrieveParser(reader.CurrentOffset);
                    parseDelegate(reader);
                }
                throw new NotImplementedException();
            }
        }
    }
}

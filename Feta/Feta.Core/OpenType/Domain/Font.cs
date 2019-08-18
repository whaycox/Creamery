using System;
using System.IO;

namespace Feta.OpenType.Domain
{
    using Tables.Domain;

    public class Font
    {
        private static PersistorCollection Readers { get; } = new PersistorCollection();

        public static Font Read(string path)
        {
            using (FontReader reader = new FontReader(File.OpenRead(path)))
            {
                throw new NotImplementedException();
            }
        }
    }
}

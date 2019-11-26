using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Persistence.Domain
{
    public class PersistenceOptions
    {
        public static string Key => $"{nameof(Gouda)}:{nameof(Persistence)}";

        public string Server { get; set; }
        public string Database { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Domain.Persistence
{
    public abstract class NameValueEntity : NamedEntity
    {
        public string Value { get; set; }

        public static Dictionary<string, string> BuildArguments(IEnumerable<NameValueEntity> nameValueEntities) => nameValueEntities.ToDictionary(k => k.Name, v => v.Value);
    }
}

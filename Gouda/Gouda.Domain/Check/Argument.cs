using Curds.Domain.Persistence;
using System.Collections.Generic;
using System.Linq;
using Curds.Domain;

namespace Gouda.Domain.Check
{
    public class Argument : NamedEntity
    {
        public int DefinitionID { get; set; }
        public string Value { get; set; }

        public static Dictionary<string, string> Compile(IEnumerable<Argument> arguments) => arguments?.ToDictionary(k => k.Name, v => v.Value) ?? new Dictionary<string, string>();
    }
}

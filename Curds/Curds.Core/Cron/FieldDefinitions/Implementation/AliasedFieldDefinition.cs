using System.Collections.Generic;

namespace Curds.Cron.FieldDefinitions.Implementation
{
    internal abstract class AliasedFieldDefinition : BaseFieldDefinition
    {
        protected abstract Dictionary<string, string> Aliases { get; }

        public override string LookupAlias(string value)
        {
            if (Aliases.ContainsKey(value))
                return Aliases[value];
            else
                return base.LookupAlias(value);
        }
    }
}

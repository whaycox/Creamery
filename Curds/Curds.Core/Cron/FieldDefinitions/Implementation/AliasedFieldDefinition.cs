using System.Collections.Generic;

namespace Curds.Cron.FieldDefinitions.Implementation
{
    internal abstract class AliasedFieldDefinition : BaseFieldDefinition
    {
        protected abstract Dictionary<string, string> Aliases { get; }

        public override int Parse(string value)
        {
            if (Aliases.ContainsKey(value))
                value = Aliases[value];
            
            return base.Parse(value);
        }
    }
}

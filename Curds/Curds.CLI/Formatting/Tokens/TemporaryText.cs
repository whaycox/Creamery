using System;

namespace Curds.CLI.Formatting.Tokens
{
    public abstract class TemporaryText : FormattedText
    {
        public void Engage() => Add(EngageToken());
        public void Disengage() => Add(DisengageToken());

        protected abstract BaseTextToken EngageToken();
        protected abstract BaseTextToken DisengageToken();
    }
}

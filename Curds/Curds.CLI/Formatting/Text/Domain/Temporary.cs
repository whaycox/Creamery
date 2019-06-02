using System;

namespace Curds.CLI.Formatting.Text.Domain
{
    using Token.Abstraction;

    public abstract class Temporary : Formatted, IDisposable
    {
        private Formatted ParentText { get; }

        public Temporary(Formatted parentText)
        {
            ParentText = parentText;
        }

        protected override void AddToBuffer(IToken token)
        {
            if (Buffer.Count == 0)
                base.AddToBuffer(EngageToken());
            base.AddToBuffer(token);
        }

        public void Engage() => Add(EngageToken());
        public void Disengage() => Add(DisengageToken());

        protected abstract IToken EngageToken();
        protected abstract IToken DisengageToken();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Add(DisengageToken());
                    ParentText.Add(this);
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}

using System;

namespace Curds.CLI.Formatting.Tokens
{
    public abstract class TemporaryText : FormattedText, IDisposable
    {
        private FormattedText ParentText { get; }

        public TemporaryText(FormattedText parentText)
        {
            ParentText = parentText;
        }

        protected override void AddToBuffer(BaseTextToken token)
        {
            if (Buffer.Count == 0)
                base.AddToBuffer(EngageToken());
            base.AddToBuffer(token);
        }

        public void Engage() => Add(EngageToken());
        public void Disengage() => Add(DisengageToken());

        protected abstract BaseTextToken EngageToken();
        protected abstract BaseTextToken DisengageToken();

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

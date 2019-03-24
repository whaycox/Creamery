using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI.Formatting
{
    public class IndentedText : FormattedText, IDisposable
    {
        public IndentedText()
        {
            Add(new IncreaseIndentToken());
        }

        public override void Write(ConsoleWriter writer)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Add(new DecreaseIndentToken());
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

        private class IncreaseIndentToken : BaseTextToken
        {
            public override void Write(ConsoleWriter writer) => writer.Indents++;
        }

        private class DecreaseIndentToken : BaseTextToken
        {
            public override void Write(ConsoleWriter writer) => writer.Indents--;
        }
    }
}

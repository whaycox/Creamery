using Curds.Application.Text;
using System;
using System.Text;

namespace Curds.Infrastructure.Text
{
    public class IndentedWriter : IIndentedWriter
    {
        private const string DefaultIndentation = "\t";

        private StringBuilder Builder = new StringBuilder();

        private int _indents = default;
        public int Indents
        {
            get
            {
                return _indents;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Indents));
                _indents = value;
            }
        }

        private string _indentation = DefaultIndentation;
        public string Indentation
        {
            get
            {
                return _indentation;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(Indentation));
                _indentation = value;
            }
        }

        public string Write => Builder.ToString();

        public void AddLine(string message, params object[] args) => Add(string.Format(message, args));
        public void AddLine(string message) => Add(message);
        private void Add(string message)
        {
            AddIndents();
            Builder.AppendLine(message);
        }
        private void AddIndents()
        {
            for (int i = 0; i < Indents; i++)
                Builder.Append(Indentation);
        }

        public void Clear() => Builder.Clear();

        public IDisposable Scope() => new ScopeToken(this);

        public class ScopeToken : IDisposable
        {
            private IndentedWriter Parent { get; }

            public ScopeToken(IndentedWriter parent)
            {
                Parent = parent;
                Parent.Indents++;
            }

            #region IDisposable Support
            private bool disposedValue = false; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        Parent.Indents--;
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
}

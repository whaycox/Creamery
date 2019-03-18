using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Text
{
    public interface IIndentedWriter
    {
        string Indentation { get; set; }
        int Indents { get; set; }

        string Write { get; }

        IDisposable Scope();

        void AddLine(string message, params object[] args);
        void AddLine(string message);

        void Clear();
    }
}

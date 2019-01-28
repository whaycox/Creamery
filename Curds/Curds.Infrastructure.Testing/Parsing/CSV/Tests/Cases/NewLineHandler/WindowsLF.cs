using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Parsing.CSV.Tests.Cases.NewLineHandler
{
    public class WindowsLF : NewLineHandlerCase
    {
        public WindowsLF()
        {
            First = CSV.NewLineHandler.LF;
            Second = null;
            Handler = CSV.NewLineHandler.Windows;
            Expected = false;
        }
    }
}

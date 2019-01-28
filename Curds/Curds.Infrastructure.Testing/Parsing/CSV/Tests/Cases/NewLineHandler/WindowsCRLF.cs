using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Parsing.CSV.Tests.Cases.NewLineHandler
{
    public class WindowsCRLF : NewLineHandlerCase
    {
        public WindowsCRLF()
        {
            First = CSV.NewLineHandler.CR;
            Second = CSV.NewLineHandler.LF;
            Handler = CSV.NewLineHandler.Windows;
            Expected = true;
        }
    }
}

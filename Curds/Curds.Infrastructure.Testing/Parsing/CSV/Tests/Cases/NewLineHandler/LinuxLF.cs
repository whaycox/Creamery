using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Parsing.CSV.Tests.Cases.NewLineHandler
{
    public class LinuxLF : NewLineHandlerCase
    {
        public LinuxLF()
        {
            First = CSV.NewLineHandler.LF;
            Second = null;
            Handler = CSV.NewLineHandler.Linux;
            Expected = true;
        }
    }
}

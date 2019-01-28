using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Parsing.CSV.Tests.Cases.NewLineHandler
{
    public class LinuxCRLF : NewLineHandlerCase
    {
        public LinuxCRLF()
        {
            First = CSV.NewLineHandler.CR;
            Second = CSV.NewLineHandler.LF;
            Handler = CSV.NewLineHandler.Linux;
            Expected = false;
        }
    }
}

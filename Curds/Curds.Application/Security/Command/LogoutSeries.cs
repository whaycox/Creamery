using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Security.Command
{
    using Message.Command;

    public class LogoutSeries : BaseCommand
    {
        public string Series { get; }

        public LogoutSeries(string series)
        {
            Series = series;
        }
    }
}

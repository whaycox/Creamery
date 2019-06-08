using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Security.Command.LogoutSeries
{
    using Application.Command.Domain;

    public class Command : BaseCommand
    {
        public string Series { get; }

        public Command(string series)
        {
            Series = series;
        }
    }
}

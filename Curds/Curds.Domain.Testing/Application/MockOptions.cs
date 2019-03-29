using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;
using Curds.Application.Cron;
using Curds.Application.DateTimes;
using Curds.Infrastructure.Cron;

namespace Curds.Domain.Application
{
    public class MockOptions : CurdsOptions
    {
        public override IDateTime Time { get; } = new DateTimes.MockDateTime();

        public override ICron Cron { get; } = new CronProvider();
    }
}

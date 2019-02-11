using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Cron;

namespace Curds.Infrastructure.Cron
{
    public class CronProvider : ICron
    {
        public ICronObject Build(string cronString) => new Expression(cronString);
    }
}

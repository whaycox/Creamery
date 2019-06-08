using System;
using System.Collections.Generic;
using System.Text;
using Curds.Cron.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Curds.Cron.Tests
{
    [TestClass]
    public class CronExpression : Template.CronExpression<Implementation.CronExpression>
    {
        protected override Implementation.CronExpression BuildExpression(string expression) =>
            new Implementation.CronExpression(expression);
    }
}

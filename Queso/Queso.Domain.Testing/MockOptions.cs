using System;
using System.Collections.Generic;
using System.Text;
using Queso.Application;
using Queso.Application.Character;
using Curds.Domain.DateTimes;
using Curds.Infrastructure.Cron;
using Queso.Infrastructure.Character;
using Curds.Application.Cron;
using Curds.Application.DateTimes;

namespace Queso.Domain
{
    public class MockOptions : QuesoOptions
    {
        public override ICharacter Character => new CharacterProvider();
        public override ICron Cron => new CronProvider();
        public override IDateTime Time => new MockDateTime();
    }
}

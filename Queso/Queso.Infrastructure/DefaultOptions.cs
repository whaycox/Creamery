using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Cron;
using Curds.Application.DateTimes;
using Queso.Application;
using Queso.Application.Character;
using Curds.Infrastructure.DateTimes;
using Curds.Infrastructure.Cron;

namespace Queso.Infrastructure
{
    public class DefaultOptions : QuesoOptions
    {
        public override IDateTime Time => new Machine();
        public override ICron Cron => new CronProvider();
        public override ICharacter Character => new Character.CharacterProvider();
    }
}

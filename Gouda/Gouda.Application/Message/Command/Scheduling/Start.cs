using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.Message.Command.Scheduling
{
    public class Start : BaseCommand
    {
    }

    public class StartHandler : BaseCommandHandler<Start>
    {
        public StartHandler(Gouda application)
            : base(application)
        { }

        public override void Handle(Start command) => Application.Scheduler.Start();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.Command;
using Curds.Application.Message.Query;

namespace Gouda.Application.Message.Command
{
    public abstract class GoudaCommand<T, U> : BaseCommandDefinition<GoudaApplication, T, U>
        where T : BaseCommand
        where U : BaseQuery
    {
        public GoudaCommand(GoudaApplication application)
            : base(application)
        { }
    }
}

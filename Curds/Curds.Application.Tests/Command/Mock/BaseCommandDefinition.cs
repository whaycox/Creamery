using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Command.Mock
{
    using Application.Mock;

    public class BaseCommandDefinition : Domain.BaseCommandDefinition<CurdsApplication, BaseCommand>
    {
        public BaseCommandDefinition(CurdsApplication mockApplication)
            : base(mockApplication)
        { }

        public override Task Execute(BaseCommand message)
        {
            throw new NotImplementedException();
        }
    }
}

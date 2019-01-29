using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.Message.Command
{
    public class Dispatch : ReferencingObject
    {
        public Dispatch(Gouda application)
            : base(application)
        { }

        public void StartScheduling(Scheduling.Start command) => new Scheduling.StartHandler(Application).Handle(command);
    }
}

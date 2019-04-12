using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Queso.Application.Message.Query
{
    public class Dispatch : BaseDispatch<QuesoApplication>
    {
        public Character.ScanDefinition Scan { get; }

        public Dispatch(QuesoApplication application)
            : base(application)
        {
            Scan = new Character.ScanDefinition(Application);
        }
    }
}

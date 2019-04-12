using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message
{
    public abstract class BaseMessageHandler<T> : ReferencingObject<T> where T : CurdsApplication
    {
        public BaseMessageHandler(T application)
            : base(application)
        { }
    }
}

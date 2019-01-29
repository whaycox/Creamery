using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.Message
{
    public abstract class BaseMessageHandler<T> : ReferencingObject where T : BaseMessage
    {
        public BaseMessageHandler(Gouda application)
            : base(application)
        { }

        public abstract void Handle(T command);
    }
}

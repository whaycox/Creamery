using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message
{
    public abstract class DispatchResponse<T, U>
        where T : CurdsApplication
        where U : BaseMessageHandler<T>
    {
        public U Handler { get; }
        public BaseViewModel Replacement { get; }

        public DispatchResponse(U handler)
        {
            Handler = handler;
        }

        public DispatchResponse(BaseViewModel replacement)
        {
            Replacement = replacement;
        }
    }
}

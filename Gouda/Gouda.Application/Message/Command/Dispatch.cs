using Curds.Application.Message;
using System;
using System.Collections.Generic;

namespace Gouda.Application.Message.Command
{
    public class Dispatch : GoudaDispatch
    {
        public Dispatch(GoudaApplication application)
            : base(application)
        { }

        protected override Dictionary<Type, BaseMessageDefinition<GoudaApplication>> BuildMapping(Dictionary<Type, BaseMessageDefinition<GoudaApplication>> requestMap)
        {
            throw new NotImplementedException();
        }
    }
}

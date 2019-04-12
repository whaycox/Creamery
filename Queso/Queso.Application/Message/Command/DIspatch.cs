using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;
using Curds.Application.Message;
using Curds.Application.Message.Dispatch;

namespace Queso.Application.Message.Command
{
    public class Dispatch : SimpleDispatch<QuesoApplication>
    {
        public Dispatch(QuesoApplication application)
            : base(application)
        { }

        protected override Dictionary<Type, BaseMessageDefinition<QuesoApplication>> BuildMapping(Dictionary<Type, BaseMessageDefinition<QuesoApplication>> requestMap)
        {
            requestMap.Add(typeof(Character.ResurrectDefinition), new Character.ResurrectDefinition(Application));

            return requestMap;
        }
    }
}

using Curds.Application.Message;
using Curds.Application.Message.Dispatch;
using System;
using System.Collections.Generic;

namespace Queso.Application.Message.Query
{
    public class Dispatch : BaseDispatch<QuesoApplication>
    {
        public Character.ScanDefinition Scan => Lookup<Character.ScanDefinition>();

        public Dispatch(QuesoApplication application)
            : base(application)
        { }

        protected override Dictionary<Type, BaseMessageDefinition<QuesoApplication>> BuildMapping(Dictionary<Type, BaseMessageDefinition<QuesoApplication>> requestMap)
        {
            requestMap.Add(typeof(Character.ScanDefinition), new Character.ScanDefinition(Application));
            return requestMap;
        }
    }
}

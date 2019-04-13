using Curds.Application.Message;
using System;
using System.Collections.Generic;

namespace Queso.Application.Message.Command
{
    public class Dispatch : BaseDispatch<QuesoApplication>
    {
        public Character.ResurrectDefinition Resurrect => Lookup<Character.ResurrectDefinition>();

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

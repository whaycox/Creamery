using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;

namespace Gouda.Application.Message.Command
{
    public class Dispatch : ReferencingObject<GoudaApplication>
    {
        public Security.LoginDefinition Login { get; }
        public Security.CreateInitialUserDefinition CreateInitialUser { get; }

        public Dispatch(GoudaApplication application)
            : base(application)
        {
            Login = new Security.LoginDefinition(application);
            CreateInitialUser = new Security.CreateInitialUserDefinition(application);
        }


    }
}

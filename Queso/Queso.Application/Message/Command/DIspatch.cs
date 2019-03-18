using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;

namespace Queso.Application.Message.Command
{
    public class Dispatch : ReferencingObject<QuesoApplication>
    {
        public Character.ResurrectDefinition Resurrect { get; }

        public Dispatch(QuesoApplication application)
            : base(application)
        {
            Resurrect = new Character.ResurrectDefinition(application);
        }
    }
}

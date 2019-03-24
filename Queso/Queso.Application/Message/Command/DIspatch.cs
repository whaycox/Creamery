﻿using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;
using Curds.Application.Message;

namespace Queso.Application.Message.Command
{
    public class Dispatch : BaseDispatch<QuesoApplication>
    {
        public Character.ResurrectDefinition Resurrect { get; }

        public Dispatch(QuesoApplication application)
            : base(application)
        {
            Resurrect = new Character.ResurrectDefinition(application);
        }
    }
}

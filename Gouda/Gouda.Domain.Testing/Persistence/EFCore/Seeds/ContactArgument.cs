﻿using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;

namespace Gouda.Domain.Persistence.EFCore.Seeds
{
    public static class ContactArgument
    {
        public static Communication.ContactArgument[] Data => MockContactArgument.SampleArguments();
    }
}
﻿using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Gouda.Domain.Communication;

namespace Gouda.Domain.Communication
{
    public class MockContactTwo : Contact
    {
        public override Entity Clone()
        {
            throw new NotImplementedException();
        }
    }
}

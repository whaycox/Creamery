using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application
{
    public abstract class ReferencingObject
    {
        protected Gouda Application { get; }

        public ReferencingObject(Gouda application)
        {
            Application = application;
        }
    }
}

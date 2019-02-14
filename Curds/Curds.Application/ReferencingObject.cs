using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application
{
    public abstract class ReferencingObject<T> where T : CurdsApplication
    {
        protected T Application { get; }

        public ReferencingObject(T application)
        {
            Application = application;
        }
    }
}

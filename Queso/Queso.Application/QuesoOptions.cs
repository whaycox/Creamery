using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application;

namespace Queso.Application
{
    public abstract class QuesoOptions : CurdsOptions
    {
        public abstract Character.ICharacter Character { get; }
    }
}

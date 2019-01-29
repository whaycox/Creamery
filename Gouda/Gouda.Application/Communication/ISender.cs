using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;

namespace Gouda.Application.Communication
{
    public interface ISender
    {
        Response Send(Definition definition);
    }
}

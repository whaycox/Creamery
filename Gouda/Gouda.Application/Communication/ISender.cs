using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;

namespace Gouda.Application.Communication
{
    public interface ISender
    {
        Persistence.IPersistence Persistence { get; set; }

        BaseResponse Send(Definition definition);
    }
}

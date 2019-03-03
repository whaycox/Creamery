using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using System.Threading.Tasks;

namespace Gouda.Application.Communication
{
    using Persistence;

    public interface ISender
    {
        IPersistence Persistence { get; }

        Task<BaseResponse> Send(Definition definition);
    }
}

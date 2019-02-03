using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;

namespace Gouda.Application.Check
{
    public interface IExecutor
    {
        BaseResponse Perform(Request request);
    }
}

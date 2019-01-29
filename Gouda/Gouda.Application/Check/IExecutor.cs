using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;

namespace Gouda.Application.Check
{
    public interface IExecutor
    {
        Response Perform(Request request);
    }
}

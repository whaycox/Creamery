using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Check;
using Gouda.Domain;
using Gouda.Domain.Check;

namespace Gouda.Infrastructure.Check
{
    public abstract class BaseExecutor : IExecutor
    {
        public abstract Response Perform(Request request);
    }
}

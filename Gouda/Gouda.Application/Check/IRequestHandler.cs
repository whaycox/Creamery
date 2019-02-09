using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain.Check.Responses;

namespace Gouda.Application.Check
{
    public interface IRequestHandler
    {
        Guid ID { get; }

        Success Perform(Request request);
    }
}

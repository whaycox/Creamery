using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Gouda.Application.Commands.ExecuteCheck.Domain
{
    using Gouda.Domain;

    public class ExecuteCheckCommand : IRequest
    {
        public CheckDefinition Check { get; set; }
    }
}

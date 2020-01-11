using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gouda.Communication.Implementation
{
    using Abstraction;
    using Gouda.Domain;

    internal class Communicator : ICommunicator
    {
        public Task<List<DiagnosticData>> SendCheck(CheckDefinition check)
        {
            throw new NotImplementedException();
        }
    }
}

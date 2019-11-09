using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Communication.Abstraction
{
    using Gouda.Domain;

    public interface ICommunicator
    {
        Task<List<DiagnosticData>> SendCheck(Satellite satellite, Check check);
    }
}

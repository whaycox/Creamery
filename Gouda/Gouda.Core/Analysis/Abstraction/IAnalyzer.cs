using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Analysis.Abstraction
{
    using Gouda.Domain;

    public interface IAnalyzer
    {
        Task AnalyzeResult(CheckDefinition check, List<DiagnosticData> result);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gouda.Analysis.Implementation
{
    using Abstraction;
    using Gouda.Domain;

    internal class Analyzer : IAnalyzer
    {
        public Task AnalyzeResult(CheckDefinition check, List<DiagnosticData> result)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Checks.Abstraction
{
    using Gouda.Domain;
    using Gouda.Abstraction;

    public interface ICheckLibrary
    {
        List<ICheck> RegisteredChecks { get; }
    }
}

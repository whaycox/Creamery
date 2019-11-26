using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Inputs.Abstraction
{
    using Application.Abstraction;

    public interface IInputViewModel : IViewModel
    {
        string Label { get; set; }
    }
}

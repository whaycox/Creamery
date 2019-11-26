using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Inputs.Domain
{
    using Abstraction;

    public abstract class BaseInputCollection : IInputViewModel
    {
        public abstract string ViewName { get; }

        public string Label { get; set; }
        public List<IInputViewModel> Inputs { get; set; } = new List<IInputViewModel>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Inputs.Domain
{
    using Abstraction;

    public abstract class BaseInput : IInputViewModel
    {
        public abstract string ViewName { get; }

        public string Label { get; set; }
        public bool Required { get; set; }
        public string Name { get; set; }
    }
}

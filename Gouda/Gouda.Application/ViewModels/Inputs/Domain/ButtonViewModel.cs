using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Inputs.Domain
{
    using Abstraction;

    public class ButtonViewModel : IInputViewModel
    {
        public string ViewName => nameof(ButtonViewModel);

        public string Label { get; set; }
    }
}

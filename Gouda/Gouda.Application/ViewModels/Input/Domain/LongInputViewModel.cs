using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Input.Domain
{
    using Abstraction;

    public class LongInputViewModel : BaseInputViewModel, IFormInput
    {
        public override string ViewName => nameof(LongInputViewModel);

        public string Name { get; set; }
        public bool Required { get; set; }
        public long? Value { get; set; }
    }
}

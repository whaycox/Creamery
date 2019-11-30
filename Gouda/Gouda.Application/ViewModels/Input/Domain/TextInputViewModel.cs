using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Input.Domain
{
    using Abstraction;

    public class TextInputViewModel : BaseInputViewModel
    {
        public override string ViewName => nameof(TextInputViewModel);
        public virtual string TypeName => string.Empty;

        public string Name { get; set; }
        public bool Required { get; set; }
        public string ValuePattern { get; set; }
        public string Value { get; set; }
    }
}

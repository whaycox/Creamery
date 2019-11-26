using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Inputs.Domain
{
    using Abstraction;

    public class TextInputViewModel : BaseInput
    {
        public override string ViewName => nameof(TextInputViewModel);
        public virtual string TypeName => string.Empty;

        public string ValuePattern { get; set; }
        public string Value { get; set; }
    }
}

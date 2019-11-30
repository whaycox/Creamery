using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Common.Domain
{
    public class TextFieldViewModel : BaseFieldViewModel
    {
        public override string ViewName => nameof(TextFieldViewModel);

        public string Value { get; set; }
    }
}

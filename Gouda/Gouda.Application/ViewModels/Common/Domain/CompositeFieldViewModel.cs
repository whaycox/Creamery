using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Common.Domain
{
    using Application.Abstraction;

    public class CompositeFieldViewModel : BaseFieldViewModel
    {
        public override string ViewName => nameof(CompositeFieldViewModel);

        public IViewModel ViewModel { get; set; }
    }
}

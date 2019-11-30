using System.Collections.Generic;

namespace Gouda.Application.ViewModels.Input.Domain
{
    using DeferredValues.Domain;
    using Abstraction;

    public class FormViewModel : BaseInputViewModel
    {
        public override string ViewName => nameof(FormViewModel);

        public DestinationDeferredKey Destination { get; set; }
        public List<IInputViewModel> Inputs { get; set; } = new List<IInputViewModel>();
    }
}

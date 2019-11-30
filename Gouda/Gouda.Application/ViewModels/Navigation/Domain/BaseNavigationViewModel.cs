using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Navigation.Domain
{
    using Abstraction;
    using DeferredValues.Domain;

    public abstract class BaseNavigationViewModel : INavigationViewModel
    {
        public string ViewConcept => nameof(Navigation);
        public abstract string ViewName { get; }

        public LabelDeferredKey Label { get; set; }
    }
}

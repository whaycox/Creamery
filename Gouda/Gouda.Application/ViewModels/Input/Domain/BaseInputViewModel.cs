using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Input.Domain
{
    using Abstraction;
    using DeferredValues.Domain;

    public abstract class BaseInputViewModel : IInputViewModel
    {
        public string ViewConcept => nameof(Input);
        public abstract string ViewName { get; }

        public LabelDeferredKey Label { get; set; }
        public bool Editable { get; set; }
    }
}

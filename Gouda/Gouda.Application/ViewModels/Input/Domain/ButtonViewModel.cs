﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Input.Domain
{
    using Abstraction;
    using DeferredValues.Domain;

    public class ButtonViewModel : BaseInputViewModel, IEditableInput
    {
        public override string ViewName => nameof(ButtonViewModel);

        public DestinationDeferredKey Destination { get; set; }
        public bool Editable { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Input.Domain;
    using DeferredValues.Domain;

    public class CheckControlsViewModel : BaseSatelliteViewModel
    {
        public override string ViewName => nameof(CheckControlsViewModel);

        public ButtonViewModel AddCheckButton { get; set; } = new ButtonViewModel
        { 
            Label = LabelDeferredKey.AddButton, 
            Editable = true,
            Destination = DestinationDeferredKey.AddCheck,
        };
        public ButtonViewModel DeleteCheckButton { get; set; } = new ButtonViewModel
        { 
            Label = LabelDeferredKey.DeleteButton, 
            Destination = DestinationDeferredKey.DeleteCheck,
        };
    }
}

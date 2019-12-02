using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Abstraction;
    using Input.Domain;
    using DeferredValues.Domain;
    using Commands.AddSatellite.Domain;

    public class AddSatelliteViewModel : BaseSatelliteViewModel
    {
        public override string ViewName => nameof(AddSatelliteViewModel);

        public LabelDeferredKey Label { get; } = LabelDeferredKey.AddSatellite;
        public TextInputViewModel SatelliteNameInput { get; } = new TextInputViewModel
        {
            Label = LabelDeferredKey.SatelliteName,
            Required = true,
            Name = nameof(AddSatelliteCommand.SatelliteName)
        };
        public IPAddressTextInputViewModel SatelliteIPInput { get; } = new IPAddressTextInputViewModel
        {
            Label = LabelDeferredKey.SatelliteIP,
            Required = true,
            Name = nameof(AddSatelliteCommand.SatelliteIP)
        };
        public ButtonViewModel AddSatelliteButton { get; } = new ButtonViewModel
        {
            Label = LabelDeferredKey.AddButton,
            Editable = true,
            Destination = DestinationDeferredKey.AddSatellite,
        };
    }
}

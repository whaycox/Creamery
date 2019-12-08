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
        public TextInputViewModel SatelliteIPInput { get; }
        public ButtonViewModel AddSatelliteButton { get; } = new ButtonViewModel
        {
            Label = LabelDeferredKey.AddButton,
            Editable = true,
            Destination = DestinationDeferredKey.AddSatellite,
        };

        public AddSatelliteViewModel()
        {
            SatelliteIPInput = BuildIPInput();
        }
        private TextInputViewModel BuildIPInput()
        {
            TextInputViewModel ipInput = TextInputViewModel.IPAddress;
            ipInput.Label = LabelDeferredKey.SatelliteIP;
            ipInput.Required = true;
            ipInput.Name = nameof(AddSatelliteCommand.SatelliteIP);

            return ipInput;
        }
    }
}

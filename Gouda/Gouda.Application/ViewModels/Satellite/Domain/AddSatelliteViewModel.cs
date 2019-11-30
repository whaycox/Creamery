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

        public FormViewModel AddForm { get; set; } = new AddFormViewModel();

        private class AddFormViewModel : FormViewModel
        {
            public AddFormViewModel()
            {
                Label = LabelDeferredKey.AddSatelliteForm;
                Destination = DestinationDeferredKey.AddSatellite;

                Inputs.Add(new TextInputViewModel
                {
                    Label = LabelDeferredKey.SatelliteName,
                    Required = true,
                    Name = nameof(AddSatelliteCommand.SatelliteName)
                });
                Inputs.Add(new IPAddressTextInputViewModel
                {
                    Label = LabelDeferredKey.SatelliteIP,
                    Required = true,
                    Name = nameof(AddSatelliteCommand.SatelliteIP)
                });
                Inputs.Add(new ButtonViewModel
                {
                    Label = LabelDeferredKey.AddButton,
                });
            }
        }
    }
}

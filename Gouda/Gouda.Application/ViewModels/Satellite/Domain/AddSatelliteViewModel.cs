using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Satellite.Domain
{
    using Abstraction;
    using Inputs.Domain;
    using Destinations.Domain;
    using Commands.AddSatellite.Domain;

    public class AddSatelliteViewModel : ISatelliteViewModel
    {
        public string ViewName => nameof(AddSatelliteViewModel);

        public FormViewModel AddForm { get; set; } = new AddFormViewModel();

        private class AddFormViewModel : FormViewModel
        {
            private const string FormTitle = "Add a new Satellite";

            public AddFormViewModel()
            {
                Title = FormTitle;
                Destination = new DestinationViewModel { Name = nameof(Commands.AddSatellite) };

                Inputs.Add(new AddFormInputs());
            }
        }

        private class AddFormInputs : StackedInputsViewModel
        {
            private const string NameLabel = "Satellite Name";
            private const string IPLabel = "IP Address";
            private const string AddLabel = "Add";

            public AddFormInputs()
            {
                Inputs.Add(new TextInputViewModel 
                { 
                    Label = NameLabel, 
                    Required = true, 
                    Name = nameof(AddSatelliteCommand.SatelliteName) 
                });
                Inputs.Add(new IPAddressTextInputViewModel 
                { 
                    Label = IPLabel, 
                    Required = true, 
                    Name = nameof(AddSatelliteCommand.SatelliteIP) 
                });
                Inputs.Add(new ButtonViewModel 
                { 
                    Label = AddLabel 
                });
            }
        }
    }
}

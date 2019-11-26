using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Adapters.Implementation
{
    using Abstraction;
    using Domain;
    using Controllers.Implementation;

    public class DestinationCollection : IDestinationCollection
    {
        private Dictionary<string, DestinationItem> Destinations { get; } = new Dictionary<string, DestinationItem>();

        public DestinationItem this[string destination] => Destinations[destination];

        public DestinationCollection()
        {
            Destinations.Add(nameof(Application.Commands.AddSatellite), BuildItem(SatelliteController.Name, nameof(SatelliteController.Add)));
        }
        private DestinationItem BuildItem(string controller, string action) => new DestinationItem
        {
            Controller = controller,
            Action = action,
        };
    }
}

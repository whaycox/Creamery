using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.DeferredValues.Implementation
{
    using Abstraction;
    using Application.DeferredValues.Domain;
    using Domain;
    using Controllers.Implementation;

    public class DestinationDeferredValue : IDestinationDeferredValue
    {
        private Dictionary<DestinationDeferredKey, DestinationItem> Destinations { get; } = new Dictionary<DestinationDeferredKey, DestinationItem>();

        public DestinationItem this[DestinationDeferredKey key] => Destinations[key];

        public DestinationDeferredValue()
        {
            RegisterValue(DestinationDeferredKey.ListSatellites, BuildItem(SatelliteController.Name, nameof(SatelliteController.List)));
            RegisterValue(DestinationDeferredKey.AddSatellite, BuildItem(SatelliteController.Name, nameof(SatelliteController.Add)));
        }
        private void RegisterValue(DestinationDeferredKey key, DestinationItem item) => Destinations.Add(key, item);
        private DestinationItem BuildItem(string controller, string action) => new DestinationItem
        {
            Controller = controller,
            Action = action,
        };
    }
}

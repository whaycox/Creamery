using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace Gouda.WebApp.DeferredValues.Implementation
{
    using Abstraction;
    using Application.DeferredValues.Domain;
    using Domain;
    using Controllers.Implementation;

    public class DestinationDeferredValue : IDestinationDeferredValue
    {
        private Dictionary<DestinationDeferredKey, DestinationItem> Destinations { get; } = new Dictionary<DestinationDeferredKey, DestinationItem>();

        public DestinationItem this[DestinationDeferredKey key]
        {
            get
            {
                if (Destinations.TryGetValue(key, out DestinationItem item))
                    return item;
                else
                    return null;
            }
        }

        public DestinationDeferredValue()
        {
            RegisterValue(DestinationDeferredKey.ListSatellites, BuildItem(SatelliteController.Name, nameof(SatelliteController.List), HttpMethod.Get));
            RegisterValue(DestinationDeferredKey.AddSatellite, BuildItem(SatelliteController.Name, nameof(SatelliteController.AddSatellite), HttpMethod.Post));
            RegisterValue(DestinationDeferredKey.GetAddCheck, BuildItem(SatelliteController.Name, nameof(SatelliteController.GetAddCheck), HttpMethod.Get));
            RegisterValue(DestinationDeferredKey.AddCheck, BuildItem(SatelliteController.Name, nameof(SatelliteController.AddCheck), HttpMethod.Post));
            RegisterValue(DestinationDeferredKey.DeleteCheck, BuildItem(SatelliteController.Name, nameof(SatelliteController.DeleteCheck), HttpMethod.Delete));
        }
        private void RegisterValue(DestinationDeferredKey key, DestinationItem item) => Destinations.Add(key, item);
        private DestinationItem BuildItem(string controller, string action, HttpMethod method) => new DestinationItem
        {
            Controller = controller,
            Action = action,
            Method = method,
        };
    }
}

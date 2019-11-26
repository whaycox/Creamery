using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Gouda.WebApp.Adapters.Implementation
{
    using Abstraction;
    using Application.ViewModels.Destinations.Abstraction;
    using Domain;

    public class DestinationAdapter : IDestinationAdapter
    {
        private IUrlHelper UrlHelper { get; }
        private IDestinationCollection DestinationCollection { get; }

        public DestinationAdapter(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IDestinationCollection destinationCollection)
        {
            UrlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            DestinationCollection = destinationCollection;
        }

        public string Adapt(IDestinationViewModel destination)
        {
            DestinationItem item = DestinationCollection[destination.Name];
            return UrlHelper.Action(item.Action, item.Controller);
        }
    }
}

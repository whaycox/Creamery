using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Gouda.WebApp.Adapters.Implementation
{
    using Abstraction;
    using Application.DeferredValues.Domain;
    using DeferredValues.Abstraction;
    using DeferredValues.Domain;

    public class DestinationAdapter : IDestinationAdapter
    {
        private IUrlHelper UrlHelper { get; }
        private IDestinationDeferredValue DestinationDeferredValue { get; }

        public DestinationAdapter(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IDestinationDeferredValue destinationDeferredValue)
        {
            UrlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            DestinationDeferredValue = destinationDeferredValue;
        }

        public string Adapt(DestinationDeferredKey destination)
        {
            DestinationItem item = DestinationDeferredValue[destination];
            return UrlHelper.Action(item.Action, item.Controller);
        }
    }
}

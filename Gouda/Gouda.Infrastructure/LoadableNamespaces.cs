using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain;

namespace Gouda.Infrastructure
{
    public static class LoadableItems
    {
        public static IEnumerable<string> IContactAdapterNamespaces => new List<string>
        {
            $"{nameof(Gouda)}.{nameof(Domain)}.{nameof(Domain.Communication)}.Contacts.Adapters", //MUST come back to use nameofs
        };

        public static IEnumerable<string> IRequestHandlerNamespaces => new List<string>
        {
            $"{nameof(Gouda)}.{nameof(Domain)}.{nameof(Domain.Check)}", 
        };
    }
}

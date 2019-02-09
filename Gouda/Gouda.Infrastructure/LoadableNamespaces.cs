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
            "Gouda.Domain.Communication.Contacts.Adapters",
        };

        public static IEnumerable<string> CheckNamespaces => new List<string>
        {
            "Gouda.Domain.Check",
            "Gouda.Check.Basic",
        };
    }
}

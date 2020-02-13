using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Configuration
{
    using Abstraction;
    using Model.Domain;

    internal static class ConfigurationExtensions
    {
        public static bool TryGetIdentity(this IEntityConfiguration configuration, out Column identityColumn)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Parmesan.Server.Abstraction
{
    using Parmesan.Domain;

    public interface IOidcProviderMetadataFactory
    {
        OidcProviderMetadata Build();
    }
}

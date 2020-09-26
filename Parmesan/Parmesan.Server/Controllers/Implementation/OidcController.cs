using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Parmesan.Server.Controllers.Implementation
{
    using Parmesan.Domain;
    using Server.Abstraction;

    [ApiController]
    public class OidcController : ControllerBase
    {
        private IOidcProviderMetadataFactory MetadataFactory { get; }

        public OidcController(IOidcProviderMetadataFactory metadataFactory)
        {
            MetadataFactory = metadataFactory;
        }

        [HttpGet]
        [Route(OidcProviderMetadata.ConfigurationRoute)]
        public OidcProviderMetadata Configuration() => MetadataFactory.Build();
    }
}

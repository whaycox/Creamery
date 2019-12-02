using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Gouda.WebApp.DeferredValues.Domain
{
    public class DestinationItem
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public HttpMethod Method { get; set; }
    }
}

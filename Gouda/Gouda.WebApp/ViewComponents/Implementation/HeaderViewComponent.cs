using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gouda.WebApp.ViewComponents.Implementation
{
    public class HeaderViewComponent : ViewComponent
    {
        public const string Name = "Header";

        public IViewComponentResult Invoke() => View();
    }
}

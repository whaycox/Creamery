using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gouda.WebApp.ViewComponents.Implementation
{
    public class FooterViewComponent : ViewComponent
    {
        public const string Name = "Footer";

        public IViewComponentResult Invoke() => View();
    }
}

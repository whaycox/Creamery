using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gouda.WebApp.ViewComponents.Implementation
{
    using Glyphs.Abstraction;

    public class GlyphViewComponent : ViewComponent
    {
        public const string Name = "Glyph";

        public IViewComponentResult Invoke(IGlyph glyph) => View(glyph.ViewName, glyph);
    }
}

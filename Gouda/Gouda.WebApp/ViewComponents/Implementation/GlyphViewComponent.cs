using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gouda.WebApp.ViewComponents.Implementation
{
    using Abstraction;
    using WebApp.Implementation;

    public class GlyphViewComponent : ViewComponent
    {
        public const string Name = "Glyph";

        public IViewComponentResult Invoke(IGlyph glyph)
        {
            switch (glyph)
            {
                case MaterialIconGlyph materialIconGlyph:
                    return View(nameof(MaterialIconGlyph), materialIconGlyph);
                default:
                    throw new InvalidOperationException($"Unsupported glyph type {glyph.GetType()}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Glyphs.Implementation
{
    using Abstraction;

    public class MaterialIconGlyph : IGlyph
    {
        public string ViewName => nameof(MaterialIconGlyph);

        public string Name { get; set; }

        public static IGlyph Glyph(string name) => new MaterialIconGlyph { Name = name };
    }
}

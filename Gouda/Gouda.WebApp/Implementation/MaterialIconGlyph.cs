using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Implementation
{
    using Abstraction;

    public class MaterialIconGlyph : IGlyph
    {
        public string Name { get; set; }

        public static IGlyph Glyph(string name) => new MaterialIconGlyph { Name = name };
    }
}

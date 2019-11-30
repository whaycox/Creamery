using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Glyph.Domain
{
    using Abstraction;

    public abstract class BaseGlyphViewModel : IGlyphViewModel
    {
        public string ViewConcept => nameof(Glyph);
        public abstract string ViewName { get; }
    }
}

namespace Cheddar.UI.Web.Glyphs.Implementation
{
    using Web.Abstraction;
    using Web.Domain;

    public abstract class BaseGlyph : IGlyph
    {
        public abstract GlyphProvider Provider { get; }
    }
}

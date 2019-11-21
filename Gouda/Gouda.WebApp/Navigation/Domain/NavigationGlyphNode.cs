namespace Gouda.WebApp.Navigation.Domain
{
    using Glyphs.Abstraction;

    public abstract class NavigationGlyphNode : NavigationNode
    {
        public IGlyph Glyph { get; set; }
    }
}

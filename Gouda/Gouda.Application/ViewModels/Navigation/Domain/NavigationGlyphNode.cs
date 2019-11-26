namespace Gouda.Application.ViewModels.Navigation.Domain
{
    using Application.ViewModels.Glyphs.Abstraction;

    public abstract class NavigationGlyphNode : NavigationNode
    {
        public IGlyph Glyph { get; set; }
    }
}

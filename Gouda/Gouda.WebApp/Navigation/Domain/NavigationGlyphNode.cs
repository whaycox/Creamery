namespace Gouda.WebApp.Navigation.Domain
{
    using Application.ViewModels.Glyphs.Abstraction;

    public abstract class NavigationGlyphNode : NavigationNode
    {
        public IGlyph Glyph { get; set; }
    }
}

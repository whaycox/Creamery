namespace Gouda.WebApp.Navigation.Domain
{
    using WebApp.Abstraction;

    public abstract class NavigationGlyphNode : NavigationNode
    {
        public IGlyph Glyph { get; set; }
    }
}

using System;

namespace Cheddar.UI.Web.Glyphs.Implementation
{
    using Web.Domain;

    public class MaterialIconGlyph : BaseGlyph
    {
        public static MaterialIconGlyph Create(string ligature) => new MaterialIconGlyph(ligature);

        public override GlyphProvider Provider => GlyphProvider.MaterialIcons;

        public string Ligature { get; }

        private MaterialIconGlyph(string ligature)
        {
            if (string.IsNullOrWhiteSpace(ligature))
                throw new ArgumentNullException(nameof(ligature));

            Ligature = ligature;
        }
    }
}

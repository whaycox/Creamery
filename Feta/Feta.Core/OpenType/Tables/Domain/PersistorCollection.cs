using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Feta.OpenType.Tables.Domain
{
    using OpenType.Domain;
    using OpenType.Abstraction;
    using Abstraction;

    public class PersistorCollection : IPersistorCollection
    {
        public Offset.Persistor Offset { get; }

        private DSIG.Persistor Dsig { get; } = new DSIG.Persistor();
        private GDEF.Persistor Gdef { get; } = new GDEF.Persistor();
        private GPOS.Persistor Gpos { get; } = new GPOS.Persistor();
        private GSUB.Persistor Gsub { get; } = new GSUB.Persistor();
        private OSTwo.Persistor OsTwo { get; } = new OSTwo.Persistor();
        private cmap.Persistor Cmap { get; } = new cmap.Persistor();
        private cvt.Persistor Cvt { get; } = new cvt.Persistor();
        private fpgm.Persistor Fpgm { get; } = new fpgm.Persistor();
        private gasp.Persistor Gasp { get; } = new gasp.Persistor();
        private glyf.Persistor Glyf { get; } = new glyf.Persistor();
        private head.Persistor Head { get; } = new head.Persistor();
        private hhea.Persistor Hhea { get; } = new hhea.Persistor();
        private hmtx.Persistor Hmtx { get; } = new hmtx.Persistor();
        private loca.Persistor Loca { get; } = new loca.Persistor();
        private maxp.Persistor Maxp { get; } = new maxp.Persistor();
        private name.Persistor Name { get; } = new name.Persistor();
        private post.Persistor Post { get; } = new post.Persistor();
        private prep.Persistor Prep { get; } = new prep.Persistor();

        public PersistorCollection()
        {
            Offset = new Offset.Persistor(this);
        }

        public TableParseDelegate RetrieveParser(string tag)
        {
            switch (tag)
            {
                case DSIG.Table.Tag:
                    return Dsig.Read;
                case GDEF.Table.GdefTag:
                    return Gdef.Read;
                case GPOS.Table.Tag:
                    return Gpos.Read;
                case GSUB.Table.Tag:
                    return Gsub.Read;
                case OSTwo.Table.Tag:
                    return OsTwo.Read;
                case cmap.Table.Tag:
                    return Cmap.Read;
                case cvt.Table.Tag:
                    return Cvt.Read;
                case fpgm.Table.Tag:
                    return Fpgm.Read;
                case gasp.Table.Tag:
                    return Gasp.Read;
                case glyf.Table.Tag:
                    return Glyf.Read;
                case head.Table.Tag:
                    return Head.Read;
                case hhea.Table.Tag:
                    return Hhea.Read;
                case hmtx.Table.Tag:
                    return Hmtx.Read;
                case loca.Table.Tag:
                    return Loca.Read;
                case maxp.Table.Tag:
                    return Maxp.Read;
                case name.Table.Tag:
                    return Name.Read;
                case post.Table.Tag:
                    return Post.Read;
                case prep.Table.Tag:
                    return Prep.Read;
                default:
                    throw new InvalidOperationException($"Unsupported table {tag}");
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Feta.OpenType.Implementation
{
    using Abstraction;
    using Tables.cmap.Implementation;
    using Tables.cvt.Implementation;
    using Tables.DSIG.Implementation;
    using Tables.fpgm.Implementation;
    using Tables.gasp.Implementation;
    using Tables.GDEF.Implementation;
    using Tables.glyf.Implementation;
    using Tables.GPOS.Implementation;
    using Tables.GSUB.Implementation;
    using Tables.head.Implementation;
    using Tables.hhea.Implementation;
    using Tables.hmtx.Implementation;
    using Tables.loca.Implementation;
    using Tables.maxp.Implementation;
    using Tables.name.Implementation;
    using Tables.Offset.Implementation;
    using Tables.OSTwo.Implementation;
    using Tables.post.Implementation;
    using Tables.prep.Implementation;

    public class PersistorCollection : IPersistorCollection
    {
        private IEnumerable<PrimaryTablePersistor> KnownPersistors { get; } = new List<PrimaryTablePersistor>
        {
            new DsigPersistor(),
            new GdefPersistor(),
            new GposPersistor(),
            new GsubPersistor(),
            new OsTwoPersistor(),
            new CmapPersistor(),
            new CvtPersistor(),
            new FpgmPersistor(),
            new GaspPersistor(),
            new GlyfPersistor(),
            new HeadPersistor(),
            new HheaPersistor(),
            new HmtxPersistor(),
            new LocaPersistor(),
            new MaxpPersistor(),
            new NamePersistor(),
            new PostPersistor(),
            new PrepPersistor(),
        };

        private Dictionary<string, PrimaryTablePersistor> TagMap { get; }

        public OffsetPersistor Offset { get; }

        public PersistorCollection()
        {
            Offset = new OffsetPersistor(this);
            TagMap = KnownPersistors.ToDictionary(persistor => persistor.Tag);
        }

        public ITablePersistor RetrievePersistor(string tag) => TagMap[tag];
    }
}

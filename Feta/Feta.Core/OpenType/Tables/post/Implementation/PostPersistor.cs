using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.post.Implementation
{
    using OpenType.Abstraction;
    using OpenType.Implementation;
    using Domain;

    public class PostPersistor : PrimaryTablePersistor<PostTable>
    {
        public override string Tag => nameof(post);

        protected override PostTable ReadTable(uint startingOffset, IFontReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void Write(IFontWriter writer, PostTable table)
        {
            throw new NotImplementedException();
        }
    }
}

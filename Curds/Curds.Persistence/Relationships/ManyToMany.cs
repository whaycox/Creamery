using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Relationships
{
    public class ManyToMany : CachedRelationship
    {
        public override void AddRelationship(int keyID, int valueID)
        {
            throw new NotImplementedException();
        }

        public override void SeverRelationship(int keyID, int valueID)
        {
            throw new NotImplementedException();
        }
    }
}

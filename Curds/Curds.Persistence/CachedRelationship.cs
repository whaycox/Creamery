using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence
{
    public abstract class CachedRelationship
    {
        public abstract void AddRelationship(int keyID, int relatedID);
        public abstract void SeverRelationship(int keyID, int relatedID);
    }
}

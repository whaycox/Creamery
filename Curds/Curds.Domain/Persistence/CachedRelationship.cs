using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Persistence
{
    public abstract class CachedRelationship
    {
        public abstract void AddRelationship(int keyID, int valueID);
        public abstract void SeverRelationship(int keyID, int valueID);
    }
}

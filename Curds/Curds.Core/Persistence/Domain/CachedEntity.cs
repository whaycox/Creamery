using System;

namespace Curds.Persistence.Domain
{
    public class CachedEntity<TEntity>
    {
        public string Key { get; set; }
        public DateTimeOffset Expiration { get; set; }
        public TEntity Entity { get; set; }
    }
}

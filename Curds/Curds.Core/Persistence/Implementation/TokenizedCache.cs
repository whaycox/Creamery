using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Collections.Abstraction;
    using Collections.Implementation;
    using Domain;
    using Time.Abstraction;

    internal class TokenizedCache<TEntity> : ITokenizedCache<TEntity>
        where TEntity : class
    {
        private ITime Time { get; }
        private IKeyGenerator<string> TokenGenerator { get; }
        private CacheOptions Options { get; }

        private object Locker { get; } = new object();
        private TrimmableLinkedList<CachedEntity<TEntity>> EntityExpirations { get; } = new TrimmableLinkedList<CachedEntity<TEntity>>();
        private Dictionary<string, IDualLinkNode<CachedEntity<TEntity>>> Entities { get; } = new Dictionary<string, IDualLinkNode<CachedEntity<TEntity>>>();
        private Task EntityExpirationTask { get; set; }

        public int Count => Entities.Count;

        private DateTimeOffset CurrentExpiration => Time.Current.AddMinutes(Options.ExpirationInMinutes);

        public TokenizedCache(
            ITime time,
            IKeyGenerator<string> tokenGenerator,
            IOptions<CacheOptions> cacheOptions)
        {
            Time = time;
            TokenGenerator = tokenGenerator;
            Options = cacheOptions.Value;
        }

        public string Store(TEntity entity)
        {
            string token = TokenGenerator.Next;
            CachedEntity<TEntity> cachedEntity = new CachedEntity<TEntity>
            {
                Key = token,
                Entity = entity,
            };
            StoreCachedEntity(cachedEntity);
            return token;
        }
        private void StoreCachedEntity(CachedEntity<TEntity> cachedEntity)
        {
            lock (Locker)
            {
                cachedEntity.Expiration = CurrentExpiration;
                IDualLinkNode<CachedEntity<TEntity>> node = EntityExpirations.AddLast(cachedEntity);
                Entities.Add(cachedEntity.Key, node);
                if (EntityExpirationTask == null)
                    EntityExpirationTask = EntityExpirationLoop();
            }
        }
        private async Task EntityExpirationLoop()
        {
            await Task.Delay(Options.TimeToSleepInMs);
            while (Count > 0)
            {
                RemoveExpiredEntities();
                await Task.Delay(Options.TimeToSleepInMs);
            }
            EntityExpirationTask = null;
        }
        private void RemoveExpiredEntities()
        {
            lock (Locker)
            {
                if (Count > 0)
                {
                    DateTimeOffset current = Time.Current;
                    IDualLinkNode<CachedEntity<TEntity>> trimmingNode = FindTrimmingNode(current);
                    RemoveFromTrimmingNode(trimmingNode, current);
                }
            }
        }
        private IDualLinkNode<CachedEntity<TEntity>> FindTrimmingNode(DateTimeOffset expireTime)
        {
            IDualLinkNode<CachedEntity<TEntity>> currentNode = EntityExpirations.First;
            while (currentNode?.Value.Expiration < expireTime && currentNode.Next != null)
                currentNode = currentNode.Next;
            return currentNode;
        }
        private void RemoveFromTrimmingNode(IDualLinkNode<CachedEntity<TEntity>> trimmingNode, DateTimeOffset current)
        {
            if (trimmingNode?.Previous != null)
                foreach (CachedEntity<TEntity> trimmedEntity in EntityExpirations.TrimBefore(trimmingNode))
                    Entities.Remove(trimmedEntity.Key);
            if (trimmingNode.Value.Expiration < current)
            {
                CachedEntity<TEntity> trimmedEntity = EntityExpirations.Remove(trimmingNode);
                Entities.Remove(trimmedEntity.Key);
            }
        }

        public TEntity Consume(string token)
        {
            if (!Entities.TryGetValue(token, out IDualLinkNode<CachedEntity<TEntity>> cachedNode))
                return null;
            lock (Locker)
            {
                CachedEntity<TEntity> consumed = null;
                try
                {
                    Entities.Remove(token);
                    consumed = EntityExpirations.Remove(cachedNode);
                }
                catch (InvalidOperationException)
                {
                    consumed = cachedNode.Value;
                }
                if (consumed.Expiration < Time.Current)
                    return null;
                return consumed.Entity;
            }
        }
    }
}

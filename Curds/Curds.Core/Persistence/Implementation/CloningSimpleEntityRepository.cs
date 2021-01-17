using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Clone.Abstraction;
    using Expressions.Abstraction;

    internal class CloningSimpleEntityRepository<TEntity> : ISimpleRepository<TEntity>
        where TEntity : class, ISimpleEntity
    {
        public const int StartingIdentity = 1;

        private ICloneFactory CloneFactory { get; }
        private IExpressionParser ExpressionParser { get; }
        private IEntityUpdateDelegateFactory UpdateDelegateFactory { get; }

        private object Locker { get; } = new object();
        private int CurrentIdentity { get; set; } = StartingIdentity;
        private Dictionary<int, TEntity> Repository { get; } = new Dictionary<int, TEntity>();

        public CloningSimpleEntityRepository(
            ICloneFactory cloneFactory,
            IExpressionParser expressionParser,
            IEntityUpdateDelegateFactory updateDelegateFactory)
        {
            CloneFactory = cloneFactory;
            ExpressionParser = expressionParser;
            UpdateDelegateFactory = updateDelegateFactory;
        }

        private int ParseKeys(object[] keys)
        {
            if (keys == null || keys.Length == 0)
                throw new ArgumentException(nameof(keys));
            if (keys.Length != 1)
                throw new ArgumentException("Expected only a single key");
            if (!(keys[0] is int))
                throw new ArgumentException("Key is expected to be an integer");
            return (int)keys[0];
        }

        public Task Insert(IEnumerable<TEntity> entities)
        {
            lock (Locker)
            {
                foreach (TEntity entity in entities)
                    Insert(entity)
                        .GetAwaiter()
                        .GetResult();
                return Task.CompletedTask;
            }
        }
        public Task Insert(TEntity entity)
        {
            lock (Locker)
            {
                if (entity.ID != 0)
                    throw new InvalidOperationException("Cannot insert an entity with a populated identity");
                entity.ID = CurrentIdentity++;
                Repository.Add(entity.ID, CloneFactory.Clone(entity));
                return Task.CompletedTask;
            }
        }

        public Task<TEntity> Fetch(params object[] keys) => Fetch(ParseKeys(keys));
        public Task<TEntity> Fetch(int id)
        {
            if (!Repository.TryGetValue(id, out TEntity storedEntity))
                throw new KeyNotFoundException($"No entity with ID {id} was found");
            return Task.FromResult(
                CloneFactory.Clone(storedEntity));
        }

        public Task<List<TEntity>> FetchAll() => Task.FromResult(
            Repository
                .Values
                .ToList());

        public IEntityUpdate<TEntity> Update(params object[] keys) => Update(ParseKeys(keys));
        public IEntityUpdate<TEntity> Update(int id)
        {
            if (!Repository.TryGetValue(id, out TEntity storedEntity))
                throw new KeyNotFoundException($"No entity with ID {id} was found");

            return new SimpleEntityUpdate<TEntity>(
                storedEntity,
                ExpressionParser,
                UpdateDelegateFactory);
        }

        public Task Delete(params object[] keys) => Delete(ParseKeys(keys));
        public Task Delete(int id)
        {
            lock (Locker)
            {
                if (!Repository.ContainsKey(id))
                    throw new KeyNotFoundException($"No entity with ID {id} was found");
                Repository.Remove(id);
                return Task.CompletedTask;
            }
        }
    }
}

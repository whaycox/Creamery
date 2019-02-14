using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Persistence;
using Curds.Domain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Memory.Tests
{
    [TestClass]
    public class CachedPersistorMockNamedEntity : IPersistorTemplate<CachedPersistor<MockNamedEntity>, MockNamedEntity>
    {
        protected override MockNamedEntity Sample => new MockNamedEntity() { Name = nameof(Sample) };

        protected override Func<MockNamedEntity, MockNamedEntity> Modifier => Modify;
        private MockNamedEntity Modify(MockNamedEntity entity)
        {
            entity.Name = nameof(Modify);
            return entity;
        }

        private CachedPersistor<MockNamedEntity> _obj = new CachedPersistor<MockNamedEntity>();
        protected override CachedPersistor<MockNamedEntity> TestObject => _obj;

        protected override MockNamedEntity SampleInLoop(int iteration) => new MockNamedEntity() { Name = iteration.ToString() };

        protected override void VerifySampleInLoop(MockNamedEntity sample, int iteration) => Assert.AreEqual(iteration.ToString(), sample.Name);
    }
}

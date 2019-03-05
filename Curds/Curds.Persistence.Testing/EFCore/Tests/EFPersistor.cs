using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.Persistence.EFCore.Persistors;
using Curds.Domain.Persistence;
using Curds.Domain.Persistence.EFCore;
using System.Threading;

namespace Curds.Persistence.EFCore.Tests
{
    [TestClass]
    public class EFPersistor : EFPersistorTemplate<MockNameValueEntityPersistor, MockNameValueEntity, MockContext>
    {
        private MockProvider Provider = null;

        private MockNameValueEntityPersistor _obj = null;
        protected override MockNameValueEntityPersistor TestObject => _obj;

        [TestInitialize]
        public void Init()
        {
            Provider = new MockProvider();
            Provider.Reset();

            _obj = new MockNameValueEntityPersistor(Provider);
        }

        protected override MockNameValueEntity Sample => MockNameValueEntity.Sample;

        protected override Func<MockNameValueEntity, MockNameValueEntity> Modifier => Modify;
        private MockNameValueEntity Modify(MockNameValueEntity sample)
        {
            sample.Name = nameof(Modify);
            sample.Value = nameof(Modifier);
            return sample;
        }
    }
}

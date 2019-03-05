using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Persistence
{
    public class MockNameValueEntity : NameValueEntity
    {
        public static MockNameValueEntity Sample => new MockNameValueEntity(1);

        public MockNameValueEntity()
        { }

        private MockNameValueEntity(int id)
        {
            Name = id.ToString();
            Value = id.ToString();
        }

        public override bool Equals(object obj)
        {
            MockNameValueEntity toTest = obj as MockNameValueEntity;
            if (toTest == null)
                return false;
            if (toTest.ID != ID)
                return false;
            if (!toTest.Name.CompareWithNull(Name))
                return false;
            if (!toTest.Value.CompareWithNull(Value))
                return false;
            return true;
        }
    }
}

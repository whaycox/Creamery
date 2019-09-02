using System.Collections.Generic;

namespace Feta.OpenType.Mock
{
    using Abstraction;

    public class MockPersistorCollection : IPersistorCollection
    {
        public ITablePersistor PersistorToReturn = null;
        public List<string> TagsRetrieved = new List<string>();
        public ITablePersistor RetrievePersistor(string tag)
        {
            TagsRetrieved.Add(tag);
            return PersistorToReturn;
        }
    }
}

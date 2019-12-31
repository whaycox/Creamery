using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeLinkFactories.Tests
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Implementation;
    using Template;

    [TestClass]
    public class MonthRangeLinkFactoryTest : BaseRangeLinkFactoryTemplate
    {
        private MonthFieldDefinition TestFieldDefinition = new MonthFieldDefinition();

        private MonthRangeLinkFactory _testObject = null;
        protected override ICronRangeLinkFactory TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new MonthRangeLinkFactory(TestFieldDefinition);
        }

        [TestMethod]
        public void HasDefaultChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyDefaultChain<MonthFieldDefinition>();
        }
    }
}

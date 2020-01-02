using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeFactories.Chains.Tests
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Implementation;
    using Template;
    using RangeFactories.Abstraction;

    [TestClass]
    public class MonthRangeLinkFactoryTest : BaseRangeLinkFactoryTemplate
    {
        private MonthFieldDefinition TestFieldDefinition = new MonthFieldDefinition();

        private MonthRangeLinkFactory _testObject = null;
        protected override IRangeFactoryChain TestObject => _testObject;

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

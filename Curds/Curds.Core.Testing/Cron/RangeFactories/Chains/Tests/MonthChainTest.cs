using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeFactories.Chains.Tests
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Implementation;
    using Template;
    using RangeFactories.Abstraction;

    [TestClass]
    public class MonthChainTest : BaseChainTemplate
    {
        private MonthFieldDefinition TestFieldDefinition = new MonthFieldDefinition();

        private MonthChain _testObject = null;
        protected override IRangeFactoryChain TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new MonthChain(TestFieldDefinition);
        }

        [TestMethod]
        public void HasDefaultChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyDefaultChain<MonthFieldDefinition>();
        }
    }
}

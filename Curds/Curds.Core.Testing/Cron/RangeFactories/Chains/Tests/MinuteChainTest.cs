using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeFactories.Chains.Tests
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Implementation;
    using Template;
    using RangeFactories.Abstraction;

    [TestClass]
    public class MinuteChainTest : BaseChainTemplate
    {
        private MinuteFieldDefinition TestFieldDefinition = new MinuteFieldDefinition();

        private MinuteChain _testObject = null;
        protected override IRangeFactoryChain TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new MinuteChain(TestFieldDefinition);
        }

        [TestMethod]
        public void HasDefaultChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyDefaultChain<MinuteFieldDefinition>();
        }
    }
}

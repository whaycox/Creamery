using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeFactories.Chains.Tests
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Implementation;
    using Template;
    using RangeFactories.Abstraction;

    [TestClass]
    public class MinuteRangeLinkFactoryTest : BaseRangeLinkFactoryTemplate
    {
        private MinuteFieldDefinition TestFieldDefinition = new MinuteFieldDefinition();

        private MinuteRangeLinkFactory _testObject = null;
        protected override IRangeFactoryChain TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new MinuteRangeLinkFactory(TestFieldDefinition);
        }

        [TestMethod]
        public void HasDefaultChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyDefaultChain<MinuteFieldDefinition>();
        }
    }
}

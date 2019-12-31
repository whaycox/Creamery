using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeLinkFactories.Tests
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Implementation;
    using Template;

    [TestClass]
    public class MinuteRangeLinkFactoryTest : BaseRangeLinkFactoryTemplate
    {
        private MinuteFieldDefinition TestFieldDefinition = new MinuteFieldDefinition();

        private MinuteRangeLinkFactory _testObject = null;
        protected override ICronRangeLinkFactory TestObject => _testObject;

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

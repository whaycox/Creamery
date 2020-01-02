using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeFactories.Chains.Tests
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Implementation;
    using Template;
    using Links.Implementation;
    using RangeFactories.Abstraction;

    [TestClass]
    public class DayOfWeekChainTest : BaseChainTemplate
    {
        private DayOfWeekFieldDefinition TestFieldDefinition = new DayOfWeekFieldDefinition();

        private DayOfWeekChain _testObject = null;
        protected override IRangeFactoryChain TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new DayOfWeekChain(TestFieldDefinition);
        }

        [TestMethod]
        public void HasExpectedChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyAndIncrementTestRangeLink(typeof(LastDayOfWeekLink));
            VerifyAndIncrementTestRangeLink(typeof(NthDayOfWeekLink));
            VerifyDefaultChain<DayOfWeekFieldDefinition>();
        }
    }
}

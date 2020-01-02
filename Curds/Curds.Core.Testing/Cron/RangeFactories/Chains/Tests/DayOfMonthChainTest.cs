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
    public class DayOfMonthChainTest : BaseChainTemplate
    {
        private DayOfMonthFieldDefinition TestFieldDefinition = new DayOfMonthFieldDefinition();

        private DayOfMonthChain _testObject = null;
        protected override IRangeFactoryChain TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new DayOfMonthChain(TestFieldDefinition);
        }

        [TestMethod]
        public void HasExpectedChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyAndIncrementTestRangeLink(typeof(LastDayOfMonthLink));
            VerifyAndIncrementTestRangeLink(typeof(NearestWeekdayLink));
            VerifyDefaultChain<DayOfMonthFieldDefinition>();
        }
    }
}

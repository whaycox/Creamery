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
    public class DayOfMonthRangeLinkFactoryTest : BaseRangeLinkFactoryTemplate
    {
        private DayOfMonthFieldDefinition TestFieldDefinition = new DayOfMonthFieldDefinition();

        private DayOfMonthRangeLinkFactory _testObject = null;
        protected override IRangeFactoryChain TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new DayOfMonthRangeLinkFactory(TestFieldDefinition);
        }

        [TestMethod]
        public void HasExpectedChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyAndIncrementTestRangeLink(typeof(LastDayOfMonthRangeLink));
            VerifyAndIncrementTestRangeLink(typeof(NearestWeekdayRangeLink));
            VerifyDefaultChain<DayOfMonthFieldDefinition>();
        }
    }
}

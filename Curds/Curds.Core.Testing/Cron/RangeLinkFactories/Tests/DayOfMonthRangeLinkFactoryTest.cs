using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeLinkFactories.Tests
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Implementation;
    using Template;
    using RangeLinks.Implementation;

    [TestClass]
    public class DayOfMonthRangeLinkFactoryTest : BaseRangeLinkFactoryTemplate
    {
        private DayOfMonthFieldDefinition TestFieldDefinition = new DayOfMonthFieldDefinition();

        private DayOfMonthRangeLinkFactory _testObject = null;
        protected override ICronRangeLinkFactory TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new DayOfMonthRangeLinkFactory(TestFieldDefinition);
        }

        [TestMethod]
        public void HasExpectedChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyAndIncrementTestRangeLink(typeof(NearestWeekdayRangeLink));
            VerifyDefaultChain<DayOfMonthFieldDefinition>();
        }
    }
}

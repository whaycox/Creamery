using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.RangeLinkFactories.Tests
{
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using Implementation;
    using Template;
    using RangeLinks.Implementation;

    [TestClass]
    public class DayOfWeekRangeLinkFactoryTest : BaseRangeLinkFactoryTemplate
    {
        private DayOfWeekFieldDefinition TestFieldDefinition = new DayOfWeekFieldDefinition();

        private DayOfWeekRangeLinkFactory _testObject = null;
        protected override ICronRangeLinkFactory TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new DayOfWeekRangeLinkFactory(TestFieldDefinition);
        }

        [TestMethod]
        public void HasExpectedChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyAndIncrementTestRangeLink(typeof(NthDayOfWeekRangeLink));
            VerifyDefaultChain<DayOfWeekFieldDefinition>();
        }
    }
}

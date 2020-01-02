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
    public class DayOfWeekRangeLinkFactoryTest : BaseRangeLinkFactoryTemplate
    {
        private DayOfWeekFieldDefinition TestFieldDefinition = new DayOfWeekFieldDefinition();

        private DayOfWeekRangeLinkFactory _testObject = null;
        protected override IRangeFactoryChain TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new DayOfWeekRangeLinkFactory(TestFieldDefinition);
        }

        [TestMethod]
        public void HasExpectedChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyAndIncrementTestRangeLink(typeof(LastDayOfWeekRangeLink));
            VerifyAndIncrementTestRangeLink(typeof(NthDayOfWeekRangeLink));
            VerifyDefaultChain<DayOfWeekFieldDefinition>();
        }
    }
}

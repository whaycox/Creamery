using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Cron.RangeFactories.Chains.Tests
{
    using Template;
    using Implementation;
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;
    using RangeFactories.Abstraction;

    [TestClass]
    public class HourChainTest : BaseChainTemplate
    {
        private HourFieldDefinition TestFieldDefinition = new HourFieldDefinition();

        private HourChain _testObject = null;
        protected override IRangeFactoryChain TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new HourChain(TestFieldDefinition);
        }

        [TestMethod]
        public void HasDefaultChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyDefaultChain<HourFieldDefinition>();
        }
    }
}

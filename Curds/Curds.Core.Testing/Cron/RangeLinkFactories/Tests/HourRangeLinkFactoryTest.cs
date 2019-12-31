﻿using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Cron.RangeLinkFactories.Tests
{
    using Template;
    using Implementation;
    using Cron.Abstraction;
    using FieldDefinitions.Implementation;

    [TestClass]
    public class HourRangeLinkFactoryTest : BaseRangeLinkFactoryTemplate
    {
        private HourFieldDefinition TestFieldDefinition = new HourFieldDefinition();

        private HourRangeLinkFactory _testObject = null;
        protected override ICronRangeLinkFactory TestObject => _testObject;

        [TestInitialize]
        public void Init()
        {
            _testObject = new HourRangeLinkFactory(TestFieldDefinition);
        }

        [TestMethod]
        public void HasDefaultChain()
        {
            SetTestRangeLinkToStartOfChain();

            VerifyDefaultChain<HourFieldDefinition>();
        }
    }
}

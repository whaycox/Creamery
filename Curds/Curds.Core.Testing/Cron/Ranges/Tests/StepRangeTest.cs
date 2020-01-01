using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Cron.Ranges.Tests
{
    using Cron.Template;
    using Implementation;
    using Cron.Abstraction;

    [TestClass]
    public class StepRangeTest : FieldDefinitionTemplate
    {
        private int TestStep = 3;
        private DateTime TestTime = DateTime.MinValue;

        private StepRange<ICronFieldDefinition> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestAbsoluteMin = 0;
            TestAbsoluteMax = 10;
            SetupFieldDefinitionTemplate();

            TestObject = new StepRange<ICronFieldDefinition>(MockFieldDefinition.Object, TestStep);
        }

        [TestMethod]
        public void IsActiveOnMinimum()
        {
            MockFieldDefinition
                .Setup(field => field.SelectDatePart(TestTime))
                .Returns(TestAbsoluteMin);

            Assert.IsTrue(TestObject.IsActive(TestTime));
        }

        [TestMethod]
        public void IsActiveWhenExpected()
        {
            for (int i = 0; i < 15; i++)
            {
                MockFieldDefinition
                    .Setup(field => field.SelectDatePart(TestTime))
                    .Returns(i);

                if (i % TestStep == 0)
                    Assert.IsTrue(TestObject.IsActive(TestTime));
                else
                    Assert.IsFalse(TestObject.IsActive(TestTime));
            }
        }
    }
}

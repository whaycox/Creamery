using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.Template
{
    using Abstraction;

    public abstract class FieldDefinitionTemplate
    {
        protected int TestAbsoluteMin = 3;
        protected int TestAbsoluteMax = 5;

        protected Mock<ICronFieldDefinition> MockFieldDefinition = new Mock<ICronFieldDefinition>();

        [TestInitialize]
        public void SetupFieldDefinitionTemplate()
        {
            MockFieldDefinition
                .Setup(field => field.AbsoluteMin)
                .Returns(TestAbsoluteMin);
            MockFieldDefinition
                .Setup(field => field.AbsoluteMax)
                .Returns(TestAbsoluteMax);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Model.Configuration.Tests
{
    using Abstraction;
    using Domain;
    using Persistence.Abstraction;

    [TestClass]
    public class CompiledColumnConfigurationTest
    {
        private CompiledColumnConfiguration<ITestDataModel> TestObject = null;

        [TestMethod]
        public void ValueNameIsSuppliedInConstructor()
        {
            TestObject = new CompiledColumnConfiguration<ITestDataModel>(nameof(ValueNameIsSuppliedInConstructor));

            Assert.AreEqual(nameof(ValueNameIsSuppliedInConstructor), TestObject.ValueName);
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void InterfaceIdentityIsSameAsProperty(bool testIdentity)
        {
            TestObject = new CompiledColumnConfiguration<ITestDataModel>(nameof(testIdentity));
            TestObject.IsIdentity = testIdentity;

            IColumnConfiguration interfaceConfiguration = TestObject;

            Assert.AreEqual(testIdentity, interfaceConfiguration.IsIdentity);
        }
    }
}

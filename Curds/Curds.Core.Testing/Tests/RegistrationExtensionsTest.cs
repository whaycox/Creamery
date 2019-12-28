using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whey.Template;

namespace Curds.Tests
{
    using Time.Abstraction;
    using Time.Implementation;

    [TestClass]
    public class RegistrationExtensionsTest : RegistrationExtensionsTemplate
    {
        protected void AddCurdsCore() => TestServiceCollection.AddCurdsCore();

        [TestMethod]
        public void CurdsCoreAddsTime()
        {
            AddCurdsCore();

            VerifyServiceWasRegistered(typeof(ITime), typeof(MachineTime), ServiceLifetime.Transient);
        }
    }
}

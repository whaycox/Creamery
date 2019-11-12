using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gouda.Tests
{
    using Analysis.Abstraction;
    using Analysis.Implementation;
    using Communication.Abstraction;
    using Communication.Implementation;
    using Persistence.Abstraction;
    using Persistence.Implementation;
    using Scheduling.Abstraction;
    using Scheduling.Implementation;
    using Time.Abstraction;
    using Time.Implementation;

    [TestClass]
    public class RegistrationExtensionsTest
    {
        private Mock<IServiceCollection> MockServiceCollection = new Mock<IServiceCollection>();

        private List<ServiceDescriptor> RegisteredDescriptors = new List<ServiceDescriptor>();

        private void VerifyServiceWasRegistered(Type expectedInterface, Type expectedImplementation, ServiceLifetime expectedLifetime)
        {
            Assert.IsTrue(RegisteredDescriptors.Any(service =>
                service.ServiceType == expectedInterface &&
                service.ImplementationType == expectedImplementation &&
                service.Lifetime == expectedLifetime
            ));
        }

        [TestInitialize]
        public void Init()
        {
            MockServiceCollection
                .Setup(services => services.Add(It.IsAny<ServiceDescriptor>()))
                .Callback<ServiceDescriptor>(service => RegisteredDescriptors.Add(service));
        }

        [TestMethod]
        public void GoudaCoreAddsTimeTransient()
        {
            MockServiceCollection.Object.AddGoudaCore();

            VerifyServiceWasRegistered(typeof(ITime), typeof(MachineTime), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsScheduleFactorySingleton()
        {
            MockServiceCollection.Object.AddGoudaCore();

            VerifyServiceWasRegistered(typeof(IScheduleFactory), typeof(ScheduleFactory), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void GoudaCoreAddsSchedulerSingleton()
        {
            MockServiceCollection.Object.AddGoudaCore();

            VerifyServiceWasRegistered(typeof(IScheduler), typeof(Scheduler), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void GoudaCoreAddsCommunicatorTransient()
        {
            MockServiceCollection.Object.AddGoudaCore();

            VerifyServiceWasRegistered(typeof(ICommunicator), typeof(Communicator), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsAnalyzerTransient()
        {
            MockServiceCollection.Object.AddGoudaCore();

            VerifyServiceWasRegistered(typeof(IAnalyzer), typeof(Analyzer), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsGoudaDatabaseTransient()
        {
            MockServiceCollection.Object.AddGoudaCore();

            VerifyServiceWasRegistered(typeof(IGoudaDatabase), typeof(EFGoudaDatabase), ServiceLifetime.Transient);
        }
    }
}

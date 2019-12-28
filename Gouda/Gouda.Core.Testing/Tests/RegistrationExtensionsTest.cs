using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

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
    using Persistence.Domain;

    [TestClass]
    public class RegistrationExtensionsTest
    {
        private IServiceCollection TestServiceCollection = new ServiceCollection();

        private Mock<IConfiguration> MockConfiguration = new Mock<IConfiguration>();

        private void TestAddGoudaCore() => TestServiceCollection.AddGoudaCore(MockConfiguration.Object);

        private void VerifyServiceWasRegistered(Type expectedInterface, Type expectedImplementation, ServiceLifetime expectedLifetime)
        {
            Assert.IsTrue(TestServiceCollection.Any(service =>
                service.ServiceType == expectedInterface &&
                service.ImplementationType == expectedImplementation &&
                service.Lifetime == expectedLifetime
            ));
        }

        [TestMethod]
        public void GoudaCoreAddsTime()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(ITime), typeof(MachineTime), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsScheduleFactory()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(IScheduleFactory), typeof(ScheduleFactory), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void GoudaCoreAddsScheduler()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(IScheduler), typeof(Scheduler), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void GoudaCoreAddsCommunicator()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(ICommunicator), typeof(Communicator), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsAnalyzer()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(IAnalyzer), typeof(Analyzer), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsGoudaContext()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(GoudaContext), typeof(GoudaContext), ServiceLifetime.Scoped);
        }

        [TestMethod]
        public void GoudaCoreAddsAllRepositories()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(IRepository<>), typeof(EFRepository<>), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsGoudaDatabase()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(IGoudaDatabase), typeof(EFGoudaDatabase), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsCheckInheritor()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(ICheckInheritor), typeof(CheckInheritor), ServiceLifetime.Singleton);
        }
    }
}

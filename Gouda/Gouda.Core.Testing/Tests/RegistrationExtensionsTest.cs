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
    using Persistence.Domain;

    [TestClass]
    public class RegistrationExtensionsTest
    {
        private IServiceCollection TestServiceCollection = new ServiceCollection();

        private void TestAddGoudaCore() => TestServiceCollection.AddGoudaCore();

        private void VerifyServiceWasRegistered(Type expectedInterface, Type expectedImplementation, ServiceLifetime expectedLifetime)
        {
            Assert.IsTrue(TestServiceCollection.Any(service =>
                service.ServiceType == expectedInterface &&
                service.ImplementationType == expectedImplementation &&
                service.Lifetime == expectedLifetime
            ));
        }

        [TestMethod]
        public void GoudaCoreAddsTimeTransient()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(ITime), typeof(MachineTime), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsScheduleFactorySingleton()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(IScheduleFactory), typeof(ScheduleFactory), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void GoudaCoreAddsSchedulerSingleton()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(IScheduler), typeof(Scheduler), ServiceLifetime.Singleton);
        }

        [TestMethod]
        public void GoudaCoreAddsCommunicatorTransient()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(ICommunicator), typeof(Communicator), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsAnalyzerTransient()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(IAnalyzer), typeof(Analyzer), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsGoudaContextScoped()
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
        public void GoudaCoreAddsGoudaDatabaseTransient()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(IGoudaDatabase), typeof(EFGoudaDatabase), ServiceLifetime.Transient);
        }

        [TestMethod]
        public void GoudaCoreAddsCheckInheritorSingleton()
        {
            TestAddGoudaCore();

            VerifyServiceWasRegistered(typeof(ICheckInheritor), typeof(CheckInheritor), ServiceLifetime.Singleton);
        }
    }
}

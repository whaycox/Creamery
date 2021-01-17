using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Microsoft.Extensions.Options;
    using Time.Abstraction;

    [TestClass]
    public class TokenizedCacheTest
    {
        private TestCacheEntity TestEntity = new TestCacheEntity();
        private string TestToken = nameof(TestToken);
        private DateTimeOffset TestTime = DateTimeOffset.UtcNow;
        private CacheOptions TestOptions = new CacheOptions();
        private int TestSleepTime = 500;
        private int TestExpiration = 3;

        private Mock<ITime> MockTime = new Mock<ITime>();
        private Mock<IKeyGenerator<string>> MockTokenGenerator = new Mock<IKeyGenerator<string>>();
        private Mock<IOptions<CacheOptions>> MockOptions = new Mock<IOptions<CacheOptions>>();

        private TokenizedCache<TestCacheEntity> TestObject = null;

        private int TimeToWaitForSleepInMs => TestSleepTime + 75;

        [TestInitialize]
        public void Init()
        {
            TestOptions.TimeToSleepInMs = TestSleepTime;
            TestOptions.ExpirationInMinutes = TestExpiration;

            MockTokenGenerator
                .Setup(generator => generator.Next)
                .Returns(TestToken);
            MockTime
                .Setup(time => time.Current)
                .Returns(TestTime);
            MockOptions
                .Setup(options => options.Value)
                .Returns(TestOptions);

            TestObject = new TokenizedCache<TestCacheEntity>(
                MockTime.Object,
                MockTokenGenerator.Object,
                MockOptions.Object);
        }

        [TestMethod]
        public void StoreGeneratesToken()
        {
            TestObject.Store(TestEntity);

            MockTokenGenerator.Verify(generator => generator.Next, Times.Once);
        }

        [TestMethod]
        public void StoreGetsCurrentTime()
        {
            TestObject.Store(TestEntity);

            MockTime.Verify(time => time.Current, Times.AtLeastOnce);
        }

        [TestMethod]
        public void StoreReturnsGeneratedToken()
        {
            Assert.AreEqual(TestToken, TestObject.Store(TestEntity));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateTokensGeneratedThrows()
        {
            TestObject.Store(TestEntity);

            TestObject.Store(TestEntity);
        }

        [TestMethod]
        public void ConsumeReturnsStoredEntity()
        {
            TestObject.Store(TestEntity);

            Assert.AreSame(TestEntity, TestObject.Consume(TestToken));
        }

        [TestMethod]
        public void ConsumingTokenRemovesEntity()
        {
            TestObject.Store(TestEntity);

            TestObject.Consume(TestToken);

            Assert.AreEqual(0, TestObject.Count);
        }

        [TestMethod]
        public void ConsumeWithBadKeyReturnsNull()
        {
            Assert.IsNull(TestObject.Consume(TestToken));
        }

        [TestMethod]
        public void ConsumeAfterExpirationReturnsNull()
        {
            TestObject.Store(TestEntity);
            DateTimeOffset pastExpiration = TestTime
                .AddMinutes(TestExpiration)
                .AddTicks(1);
            MockTime
                .Setup(time => time.Current)
                .Returns(pastExpiration);

            Assert.IsNull(TestObject.Consume(TestToken));
        }

        [TestMethod]
        public async Task WaitingAfterSleepRemovesExpiredEntities()
        {
            TestObject.Store(TestEntity);
            DateTimeOffset pastExpiration = TestTime
                .AddMinutes(TestExpiration)
                .AddTicks(1);
            MockTime
                .Setup(time => time.Current)
                .Returns(pastExpiration);

            await Task.Delay(TimeToWaitForSleepInMs);

            Assert.AreEqual(0, TestObject.Count);
        }

        [DataTestMethod]
        [DataRow(1, 0)]
        [DataRow(1, 1)]
        [DataRow(100, 1)]
        [DataRow(100, 10)]
        [DataRow(100, 50)]
        [DataRow(100, 75)]
        [DataRow(100, 99)]
        [DataRow(100, 100)]
        public async Task ExpiresOnlyCorrectEntities(int entitiesToAdd, int entitiesToExpire)
        {
            for (int i = 0; i < entitiesToAdd; i++)
            {
                MockTokenGenerator
                    .Setup(generator => generator.Next)
                    .Returns(i.ToString());
                DateTimeOffset entityStoreTime = TestTime.AddTicks(i);
                MockTime
                    .Setup(time => time.Current)
                    .Returns(entityStoreTime);
                TestObject.Store(TestEntity);
            }
            DateTimeOffset expirationTime = TestTime
                .AddMinutes(TestExpiration)
                .AddTicks(entitiesToExpire);
            MockTime
                .Setup(time => time.Current)
                .Returns(expirationTime);

            await Task.Delay(TimeToWaitForSleepInMs);

            Assert.AreEqual(entitiesToAdd - entitiesToExpire, TestObject.Count);
        }
    }
}

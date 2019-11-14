using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Persistence.Tests
{
    using Implementation;
    using Abstraction;
    using Gouda.Domain;

    [TestClass]
    public class CheckInheritorTest
    {
        private Check TestCheckOne = new Check { ID = 1, RescheduleSecondInterval = 10 };
        private Check TestCheckTwo = new Check { ID = 2, ParentCheckID = 1 };
        private Check TestCheckThree = new Check { ID = 3, ParentCheckID = 1, RescheduleSecondInterval = 25 };
        private Check TestCheckFour = new Check { ID = 4, ParentCheckID = 2 };
        private List<Check> TestChecks = null;

        private CheckInheritor TestObject = new CheckInheritor();

        [TestInitialize]
        public void Init()
        {
            TestChecks = new List<Check>
            {
                TestCheckOne,
                TestCheckTwo,
                TestCheckThree,
                TestCheckFour,
            };
        }

        [TestMethod]
        public async Task CanSeedWithChecks()
        {
            await TestObject.Seed(TestChecks);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task MultipleSeedThrows()
        {
            await TestObject.Seed(TestChecks);
            await TestObject.Seed(new List<Check>());
        }

        [TestMethod]
        public async Task CanRetrieveASeededCheck()
        {
            await TestObject.Seed(TestChecks);

            Check built = await TestObject.Build(TestCheckOne.ID);

            VerifyTestCheckOne(built);
        }
        private void VerifyTestCheckOne(Check actual)
        {
            Assert.AreNotSame(TestCheckOne, actual);
            Assert.AreEqual(TestCheckOne.ID, actual.ID);
            Assert.AreEqual(TestCheckOne.ParentCheckID, actual.ParentCheckID);
            Assert.AreEqual(TestCheckOne.RescheduleSecondInterval, actual.RescheduleSecondInterval);
        }

        [TestMethod]
        public async Task CanInheritTestCheckTwo()
        {
            await TestObject.Seed(TestChecks);

            Check built = await TestObject.Build(TestCheckTwo.ID);

            VerifyTestCheckTwo(built);
        }
        private void VerifyTestCheckTwo(Check actual)
        {
            Assert.AreNotSame(TestCheckTwo, actual);
            Assert.AreEqual(TestCheckTwo.ID, actual.ID);
            Assert.AreEqual(TestCheckTwo.ParentCheckID, actual.ParentCheckID);
            Assert.AreEqual(TestCheckOne.RescheduleSecondInterval, actual.RescheduleSecondInterval);
        }

        [TestMethod]
        public async Task CanInheritTestCheckThree()
        {
            await TestObject.Seed(TestChecks);

            Check built = await TestObject.Build(TestCheckThree.ID);

            VerifyTestCheckThree(built);
        }
        private void VerifyTestCheckThree(Check actual)
        {
            Assert.AreNotSame(TestCheckThree, actual);
            Assert.AreEqual(TestCheckThree.ID, actual.ID);
            Assert.AreEqual(TestCheckThree.ParentCheckID, actual.ParentCheckID);
            Assert.AreEqual(TestCheckThree.RescheduleSecondInterval, actual.RescheduleSecondInterval);
        }

        [TestMethod]
        public async Task CanInheritTestCheckFour()
        {
            await TestObject.Seed(TestChecks);

            Check built = await TestObject.Build(TestCheckFour.ID);

            VerifyTestCheckFour(built);
        }
        private void VerifyTestCheckFour(Check actual)
        {
            Assert.AreNotSame(TestCheckFour, actual);
            Assert.AreEqual(TestCheckFour.ID, actual.ID);
            Assert.AreEqual(TestCheckFour.ParentCheckID, actual.ParentCheckID);
            Assert.AreEqual(TestCheckOne.RescheduleSecondInterval, actual.RescheduleSecondInterval);
        }

        [TestMethod]
        public async Task CanBuildManyChecks()
        {
            await TestObject.Seed(TestChecks);
            List<int> checkIDs = TestChecks.Select(check => check.ID).ToList();

            List<Check> built = await TestObject.Build(checkIDs);
            VerifyTestCheckOne(built[0]);
            VerifyTestCheckTwo(built[1]);
            VerifyTestCheckThree(built[2]);
            VerifyTestCheckFour(built[3]);
        }
    }
}

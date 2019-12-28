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
        //private CheckDefinition TestCheckOne = new CheckDefinition { ID = 1, RescheduleSecondInterval = 10 };
        //private CheckDefinition TestCheckTwo = new CheckDefinition { ID = 2, ParentDefinitionID = 1 };
        //private CheckDefinition TestCheckThree = new CheckDefinition { ID = 3, ParentDefinitionID = 1, RescheduleSecondInterval = 25 };
        //private CheckDefinition TestCheckFour = new CheckDefinition { ID = 4, ParentDefinitionID = 2 };
        //private List<CheckDefinition> TestChecks = null;

        private CheckInheritor TestObject = new CheckInheritor();

        [TestInitialize]
        public void Init()
        {
            //TestChecks = new List<CheckDefinition>
            //{
            //    TestCheckOne,
            //    TestCheckTwo,
            //    TestCheckThree,
            //    TestCheckFour,
            //};
        }

        [TestMethod]
        public async Task CanSeedWithChecks()
        {
            throw new NotImplementedException();
            //await TestObject.Seed(TestChecks);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task MultipleSeedThrows()
        {
            throw new NotImplementedException();
            //await TestObject.Seed(TestChecks);
            //await TestObject.Seed(new List<CheckDefinition>());
        }

        [TestMethod]
        public async Task CanRetrieveASeededCheck()
        {
            throw new NotImplementedException();
            //await TestObject.Seed(TestChecks);

            //CheckDefinition built = await TestObject.Build(TestCheckOne.ID);

            //VerifyTestCheckOne(built);
        }
        //private void VerifyTestCheckOne(CheckDefinition actual)
        //{
        //    Assert.AreNotSame(TestCheckOne, actual);
        //    Assert.AreEqual(TestCheckOne.ID, actual.ID);
        //    Assert.AreEqual(TestCheckOne.ParentDefinitionID, actual.ParentDefinitionID);
        //    Assert.AreEqual(TestCheckOne.RescheduleSecondInterval, actual.RescheduleSecondInterval);
        //}

        [TestMethod]
        public async Task CanInheritTestCheckTwo()
        {
            throw new NotImplementedException();
            //await TestObject.Seed(TestChecks);

            //CheckDefinition built = await TestObject.Build(TestCheckTwo.ID);

            //VerifyTestCheckTwo(built);
        }
        //private void VerifyTestCheckTwo(CheckDefinition actual)
        //{
        //    Assert.AreNotSame(TestCheckTwo, actual);
        //    Assert.AreEqual(TestCheckTwo.ID, actual.ID);
        //    Assert.AreEqual(TestCheckTwo.ParentDefinitionID, actual.ParentDefinitionID);
        //    Assert.AreEqual(TestCheckOne.RescheduleSecondInterval, actual.RescheduleSecondInterval);
        //}

        [TestMethod]
        public async Task CanInheritTestCheckThree()
        {
            throw new NotImplementedException();
            //await TestObject.Seed(TestChecks);

            //CheckDefinition built = await TestObject.Build(TestCheckThree.ID);

            //VerifyTestCheckThree(built);
        }
        //private void VerifyTestCheckThree(CheckDefinition actual)
        //{
        //    Assert.AreNotSame(TestCheckThree, actual);
        //    Assert.AreEqual(TestCheckThree.ID, actual.ID);
        //    Assert.AreEqual(TestCheckThree.ParentDefinitionID, actual.ParentDefinitionID);
        //    Assert.AreEqual(TestCheckThree.RescheduleSecondInterval, actual.RescheduleSecondInterval);
        //}

        [TestMethod]
        public async Task CanInheritTestCheckFour()
        {
            throw new NotImplementedException();
            //await TestObject.Seed(TestChecks);

            //CheckDefinition built = await TestObject.Build(TestCheckFour.ID);

            //VerifyTestCheckFour(built);
        }
        //private void VerifyTestCheckFour(CheckDefinition actual)
        //{
        //    Assert.AreNotSame(TestCheckFour, actual);
        //    Assert.AreEqual(TestCheckFour.ID, actual.ID);
        //    Assert.AreEqual(TestCheckFour.ParentDefinitionID, actual.ParentDefinitionID);
        //    Assert.AreEqual(TestCheckOne.RescheduleSecondInterval, actual.RescheduleSecondInterval);
        //}

        [TestMethod]
        public async Task CanBuildManyChecks()
        {
            throw new NotImplementedException();
            //await TestObject.Seed(TestChecks);
            //List<int> checkIDs = TestChecks.Select(check => check.ID).ToList();

            //List<CheckDefinition> built = await TestObject.Build(checkIDs);
            //VerifyTestCheckOne(built[0]);
            //VerifyTestCheckTwo(built[1]);
            //VerifyTestCheckThree(built[2]);
            //VerifyTestCheckFour(built[3]);
        }
    }
}

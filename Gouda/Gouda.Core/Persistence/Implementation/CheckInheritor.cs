using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;

namespace Gouda.Persistence.Implementation
{
    using Abstraction;
    using Gouda.Domain;
    using Exceptions;

    internal class CheckInheritor : ICheckInheritor
    {
        private const int MaxConcurrentAttempts = 3;
        private const int ConcurrentRetryDelayInMs = 10;

        private ConcurrentDictionary<int, CheckDefinition> Checks = new ConcurrentDictionary<int, CheckDefinition>();
        private bool SeedAllowed = true;

        private async Task ConcurrentOperation(Func<bool> concurrentDelegate)
        {
            int attempts = 0;
            while (++attempts <= MaxConcurrentAttempts && !concurrentDelegate())
                await Task.Delay(ConcurrentRetryDelayInMs);
            if (attempts > MaxConcurrentAttempts)
                throw new ConcurrentOperationException($"Failed to operate on {nameof(CheckInheritor)}'s {nameof(Checks)}");
        }

        public async Task<List<CheckDefinition>> Build(List<int> checkIDs)
        {
            IEnumerable<Task<CheckDefinition>> checkTasks = checkIDs.Select(checkID => Build(checkID));
            return (await Task.WhenAll(checkTasks)).ToList();
        }

        public async Task<CheckDefinition> Build(int checkID)
        {
            throw new NotImplementedException();
            //CheckDefinition currentLevel = await RetrieveCheck(checkID);
            //if (currentLevel.ParentDefinitionID == null)
            //    return currentLevel;
            //else
            //    return InheritChecks(currentLevel, await Build(currentLevel.ParentDefinitionID.Value));
        }
        //private CheckDefinition InheritChecks(CheckDefinition current, CheckDefinition ancestor) => new CheckDefinition
        //{
        //    ID = current.ID,
        //    ParentDefinitionID = current.ParentDefinitionID,
        //    RescheduleSecondInterval = current.RescheduleSecondInterval ?? ancestor.RescheduleSecondInterval,
        //};
        private async Task<CheckDefinition> RetrieveCheck(int checkID)
        {
            CheckDefinition retrieved = null;
            await ConcurrentOperation(() => Checks.TryGetValue(checkID, out retrieved));
            return retrieved;
        }

        public async Task Seed(List<CheckDefinition> seedChecks)
        {
            throw new NotImplementedException();
            //if (!SeedAllowed)
            //    throw new InvalidOperationException($"Seeding {nameof(CheckInheritor)} is no longer allowed");

            //foreach (CheckDefinition check in seedChecks)
            //    await AddCheck(check);
            //SeedAllowed = false;
        }
        //private Task AddCheck(CheckDefinition check) => ConcurrentOperation(() => Checks.TryAdd(check.ID, CloneCheck(check)));
        //private CheckDefinition CloneCheck(CheckDefinition inputCheck) => new CheckDefinition
        //{
        //    ID = inputCheck.ID,
        //    ParentDefinitionID = inputCheck.ParentDefinitionID,
        //    RescheduleSecondInterval = inputCheck.RescheduleSecondInterval,
        //};

        public Task Add(CheckDefinition checkDefinition)
        {
            throw new NotImplementedException();
        }
    }
}

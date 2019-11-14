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

    public class CheckInheritor : ICheckInheritor
    {
        private const int MaxConcurrentAttempts = 3;
        private const int ConcurrentRetryDelayInMs = 10;

        private ConcurrentDictionary<int, Check> Checks = new ConcurrentDictionary<int, Check>();
        private bool SeedAllowed = true;

        private async Task ConcurrentOperation(Func<bool> concurrentDelegate)
        {
            int attempts = 0;
            while (++attempts <= MaxConcurrentAttempts && !concurrentDelegate())
                await Task.Delay(ConcurrentRetryDelayInMs);
            if (attempts > MaxConcurrentAttempts)
                throw new ConcurrentOperationException($"Failed to operate on {nameof(CheckInheritor)}'s {nameof(Checks)}");
        }

        public async Task<List<Check>> Build(List<int> checkIDs)
        {
            IEnumerable<Task<Check>> checkTasks = checkIDs.Select(checkID => Build(checkID));
            return (await Task.WhenAll(checkTasks)).ToList();
        }

        public async Task<Check> Build(int checkID)
        {
            Check currentLevel = await RetrieveCheck(checkID);
            if (currentLevel.ParentCheckID == null)
                return currentLevel;
            else
                return InheritChecks(currentLevel, await Build(currentLevel.ParentCheckID.Value));
        }
        private Check InheritChecks(Check current, Check ancestor) => new Check
        {
            ID = current.ID,
            ParentCheckID = current.ParentCheckID,
            RescheduleSecondInterval = current.RescheduleSecondInterval ?? ancestor.RescheduleSecondInterval,
        };
        private async Task<Check> RetrieveCheck(int checkID)
        {
            Check retrieved = null;
            await ConcurrentOperation(() => Checks.TryGetValue(checkID, out retrieved));
            return retrieved;
        }

        public async Task Seed(List<Check> seedChecks)
        {
            if (!SeedAllowed)
                throw new InvalidOperationException($"Seeding {nameof(CheckInheritor)} is no longer allowed");

            foreach (Check check in seedChecks)
                await AddCheck(check);
            SeedAllowed = false;
        }
        private Task AddCheck(Check check) => ConcurrentOperation(() => Checks.TryAdd(check.ID, CloneCheck(check)));
        private Check CloneCheck(Check inputCheck) => new Check
        {
            ID = inputCheck.ID,
            ParentCheckID = inputCheck.ParentCheckID,
            RescheduleSecondInterval = inputCheck.RescheduleSecondInterval,
        };
    }
}

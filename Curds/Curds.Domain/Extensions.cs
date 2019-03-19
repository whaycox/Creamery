using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Curds
{
    public static class Extensions
    {
        public static void AwaitResult(this Task task) => task.GetAwaiter().GetResult();
        public static T AwaitResult<T>(this Task<T> task) => task.GetAwaiter().GetResult();

        private const int ConcurrentAttempts = 3;
        private const int ConcurrentFailureSleepInMs = 15;

        public static void AddConcurrently<T, U>(this ConcurrentDictionary<T, U> dictionary, T key, U value)
        {
            int attempts = 0;
            while (attempts++ < ConcurrentAttempts && !dictionary.TryAdd(key, value))
                Thread.Sleep(ConcurrentFailureSleepInMs);
            if (attempts >= ConcurrentAttempts)
                throw new Exception($"Failed to add after {ConcurrentAttempts} attempts");
        }

        public static void RemoveConcurrently<T, U>(this ConcurrentDictionary<T, U> dictionary, T key)
        {
            int attempts = 0;
            while (attempts++ < ConcurrentAttempts && !dictionary.TryRemove(key, out U deleted))
                Thread.Sleep(ConcurrentFailureSleepInMs);
            if (attempts >= ConcurrentAttempts)
                throw new Exception($"Failed to remove after {ConcurrentAttempts} attempts");
        }
    }
}

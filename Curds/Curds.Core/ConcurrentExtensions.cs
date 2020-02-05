using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Curds
{
    using Domain;

    public static class ConcurrentExtensions
    {
        public static int MaxConcurrentAttempts { get; set; } = 3;
        public static int ConcurrentRetryDelayInMs { get; set; } = 15;

        public static async Task TryAddOrFail<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            int attemptsMade = 0;
            while (attemptsMade++ < MaxConcurrentAttempts && !dictionary.TryAdd(key, value))
                await Task.Delay(ConcurrentRetryDelayInMs);
            if (attemptsMade > MaxConcurrentAttempts)
                throw new ConcurrentException($"The maximum number of attempts {MaxConcurrentAttempts} were made without a success");
        }

    }
}

namespace Curds.Persistence.Domain
{
    public class CacheOptions
    {
        public const int DefaultSleepTime = 368;
        public const int DefaultExpiration = 5;

        public int TimeToSleepInMs { get; set; } = DefaultSleepTime;
        public int ExpirationInMinutes { get; set; } = DefaultExpiration;
    }
}

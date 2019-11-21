namespace Gouda.WebApp.Domain
{
    public class CheckExecutionServiceOptions
    {
        public const int DefaultSleepTimeInMs = 500;

        public int SleepTimeInMs { get; set; } = DefaultSleepTimeInMs;
    }
}

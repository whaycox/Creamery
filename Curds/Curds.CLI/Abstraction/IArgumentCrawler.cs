namespace Curds.CLI.Abstraction
{
    public interface IArgumentCrawler
    {
        bool FullyConsumed { get; }

        string Consume();
        void StepBackwards();
    }
}

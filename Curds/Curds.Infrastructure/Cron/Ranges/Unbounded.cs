namespace Curds.Infrastructure.Cron.Ranges
{
    internal class Unbounded : Basic
    {
        public Unbounded(int min, int max)
            : base(min, max)
        { }

        public override bool Probe(int testValue) => true;
    }
}

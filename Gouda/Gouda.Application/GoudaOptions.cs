using Curds.Application;

namespace Gouda.Application
{
    public abstract class GoudaOptions : CurdsOptions
    {
        public abstract Persistence.IProvider Persistence { get; }

        public abstract Check.IExecutor Executor { get; }
        public abstract Check.IEvaluator Evaluator { get; }
        public abstract Check.IScheduler Scheduler { get; }

        public abstract Communication.IListener Listener { get; }
        public abstract Communication.ISender Sender { get; }
        public abstract Communication.INotifier Notifier { get; }
    }
}

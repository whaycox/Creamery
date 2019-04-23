using Curds.Application;
using Curds.Application.Security;

namespace Gouda.Application
{
    public abstract class GoudaOptions : CurdsOptions
    {
        public abstract ISecurity Security { get; }

        public abstract Persistence.IPersistence Persistence { get; }

        public abstract Check.IExecutor Executor { get; }
        public abstract Check.IEvaluator Evaluator { get; }
        public abstract Check.IScheduler Scheduler { get; }

        public abstract Communication.IListener Listener { get; }
        public abstract Communication.ISender Sender { get; }
        public abstract Communication.INotifier Notifier { get; }
    }
}

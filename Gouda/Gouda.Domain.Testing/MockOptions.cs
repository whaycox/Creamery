using Curds.Domain.DateTimes;
using Gouda.Infrastructure.Check;
using Gouda.Infrastructure.Communication;
using Gouda.Persistence;
using Gouda.Application.Check;
using Gouda.Application.Communication;
using Gouda.Application;

namespace Gouda.Domain
{
    using Check;
    using Communication;
    using Persistence;

    public class MockOptions : GoudaOptions
    {
        public MockExecutor MockExecutor { get; }
        public MockNotifier MockNotifier { get; }

        public override IExecutor Executor => MockExecutor;
        public override INotifier Notifier => MockNotifier;

        public override Gouda.Application.Persistence.IProvider Persistence => new MockProvider();
        public override IListener Listener => new Listener(Testing.TestEndpoint);
        public override IEvaluator Evaluator => new Evaluator();
        public override ISender Sender => new Sender();
        public override Curds.Application.DateTimes.IProvider Time => new MockDateTime();
        public override Curds.Application.Cron.IProvider Cron => new Curds.Infrastructure.Cron.Provider();
        public override IScheduler Scheduler => new Scheduler();

        public MockOptions()
        {
            MockExecutor = new MockExecutor();
            MockNotifier = new MockNotifier();
        }

    }
}

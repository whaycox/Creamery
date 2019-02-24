using Curds.Domain.DateTimes;
using Gouda.Infrastructure.Check;
using Gouda.Infrastructure.Communication;
using Gouda.Application.Check;
using Gouda.Application.Communication;
using Gouda.Application;
using Gouda.Application.Persistence;
using Curds.Application.Cron;
using Curds.Application.DateTimes;
using Curds.Infrastructure.Cron;
using Gouda.Application.Security;

namespace Gouda.Domain
{
    using Check;
    using Communication;
    using Persistence;
    using Security;

    public class MockOptions : GoudaOptions
    {
        public MockDateTime MockDateTime { get; }
        public CronProvider CronProvider { get; }

        public override IDateTime Time => MockDateTime;
        public override ICron Cron => CronProvider;

        public MockScheduler MockScheduler { get; }
        public MockExecutor MockExecutor { get; }
        public MockEvaluator MockEvaluator { get; }

        public override IScheduler Scheduler => MockScheduler;
        public override IExecutor Executor => MockExecutor;
        public override IEvaluator Evaluator => MockEvaluator;

        public MockPersistence MockPersistence { get; }

        public override IPersistence Persistence => MockPersistence;

        public MockListener MockListener { get; }
        public MockSender MockSender { get; }
        public MockNotifier MockNotifier { get; }

        public override IListener Listener => MockListener;
        public override ISender Sender => MockSender;
        public override INotifier Notifier => MockNotifier;

        public MockSecurityProvider MockSecurity { get; }

        public override ISecurity Security => MockSecurity;

        public MockOptions()
        {
            MockDateTime = new MockDateTime();
            CronProvider = new CronProvider();

            MockScheduler = new MockScheduler();
            MockExecutor = new MockExecutor();

            MockPersistence = new MockPersistence();

            MockSecurity = new MockSecurityProvider();

            MockListener = new MockListener();
            MockSender = new MockSender();
            MockNotifier = new MockNotifier();

            MockEvaluator = new MockEvaluator(MockNotifier, MockPersistence);
        }

    }
}

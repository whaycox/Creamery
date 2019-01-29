using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Communication;
using Gouda.Domain.Communication;
using Gouda.Infrastructure.Communication;

namespace Gouda.Domain.Communication
{
    public class MockContactOneAdapter : BaseContactAdapter<MockContactOne>
    {
        private MockNotifier Notifier { get; }

        protected override void Notify(MockContactOne contact)
        {
            Notifier.UsersNotified.Add(contact.UserID);
            Notifier.UsersContactedByOne.Add(contact.UserID);
        }

        public MockContactOneAdapter(MockNotifier notifier)
        {
            Notifier = notifier;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Infrastructure.Communication;

namespace Gouda.Domain.Communication.Contacts.Adapters
{
    public class MockContactTwoAdapter : BaseContactAdapter<MockContactTwo>
    {
        private MockNotifier Notifier { get; }

        protected override void Notify(MockContactTwo contact)
        {
            Notifier.UsersNotified.Add(contact.UserID);
            Notifier.UsersContactedByTwo.Add(contact.UserID);
        }

        public MockContactTwoAdapter(MockNotifier notifier)
        {
            Notifier = notifier;
        }
    }
}

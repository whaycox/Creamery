using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.EventArgs;
using Gouda.Infrastructure.Communication;

namespace Gouda.Domain.Communication
{
    using Contacts;
    using Contacts.Adapters;

    public class MockNotifier : Notifier
    {
        public List<int> UsersNotified = new List<int>();

        public List<int> UsersContactedByOne = new List<int>();
        public List<int> UsersContactedByTwo = new List<int>();
    }
}

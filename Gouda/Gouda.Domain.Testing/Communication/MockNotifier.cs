using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.EventArgs;
using Gouda.Infrastructure.Communication;

namespace Gouda.Domain.Communication
{
    public class MockNotifier : BaseNotifier
    {
        public List<int> UsersNotified = new List<int>();

        public List<int> UsersContactedByOne = new List<int>();
        public List<int> UsersContactedByTwo = new List<int>();

        protected override void LoadAdapters()
        {
            AddAdapter(typeof(MockContactOne), new MockContactOneAdapter(this));
            AddAdapter(typeof(MockContactTwo), new MockContactTwoAdapter(this));
        }
    }
}

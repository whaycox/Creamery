using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Communication;
using Gouda.Domain.Communication;
using Gouda.Infrastructure.Communication;
using Gouda.Domain.Check;

namespace Gouda.Domain.Communication.Contacts.Adapters
{
    public class MockContactOneAdapter : BaseContactAdapter<MockContactOne>
    {
        public static List<(int userNotified, StatusChange changeInformation)> Notifications = new List<(int userNotified, StatusChange changeInformation)>();
        public static void Reset() => Notifications.Clear();

        protected override void Notify(MockContactOne contact, StatusChange changeInformation) => Notifications.Add((contact.UserID, changeInformation));
    }
}

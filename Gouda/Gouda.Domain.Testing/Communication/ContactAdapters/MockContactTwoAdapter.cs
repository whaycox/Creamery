using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Infrastructure.Communication;
using Gouda.Domain.Check;
using Gouda.Domain.Enumerations;

namespace Gouda.Domain.Communication.ContactAdapters
{
    public class MockContactTwoAdapter : BaseContactAdapter
    {
        public static List<(int userNotified, StatusChange changeInformation)> Notifications = new List<(int userNotified, StatusChange changeInformation)>();
        public static void Reset() => Notifications.Clear();

        public override ContactType HandledType => ContactType.Email;

        public override void Notify(Contact contact, StatusChange changeInformation) => Notifications.Add((contact.UserID, changeInformation));
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Communication
{
    using Enumerations;
    using Security;

    public static class MockContact
    {
        private const int Users = 3;
        private static int AbsoluteID(int userID, int contact) => ((userID - 1) * Users) + contact;

        public static Contact One(int userID) => new Contact() { ID = AbsoluteID(userID, 1), Name = nameof(One), Type = ContactType.Browser, UserID = userID };
        public static Contact Two(int userID) => new Contact() { ID = AbsoluteID(userID, 2), Name = nameof(Two), Type = ContactType.Email, UserID = userID };
        public static Contact Three(int userID) => new Contact() { ID = AbsoluteID(userID, 3), Name = nameof(Three), Type = ContactType.Testing, UserID = userID };

        private static IEnumerable<Contact> ContactsForUser(int userID) => new List<Contact>
        {
            One(userID),
            Two(userID),
            Three(userID),
        };

        public static Contact[] Samples
        {
            get
            {
                List<Contact> toReturn = new List<Contact>();
                toReturn.AddRange(ContactsForUser(MockUser.One.ID));
                toReturn.AddRange(ContactsForUser(MockUser.Two.ID));
                toReturn.AddRange(ContactsForUser(MockUser.Three.ID));
                return toReturn.ToArray();
            }
        }
    }
}

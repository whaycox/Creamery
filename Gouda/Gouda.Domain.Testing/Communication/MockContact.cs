using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Communication
{
    using Enumerations;
    using Security;

    public static class MockContact
    {
        public static Contact One => new Contact() { ID = 1, Name = nameof(One), Type = ContactType.Browser, UserID = MockUser.One.ID };
        public static Contact Two => new Contact() { ID = 2, Name = nameof(Two), Type = ContactType.Email, UserID = MockUser.Two.ID };
        public static Contact Three => new Contact() { ID = 3, Name = nameof(Three), Type = ContactType.Testing, UserID = MockUser.Three.ID };

        public static Contact[] Samples => new Contact[]
        {
            One,
            Two,
            Three,
        };
    }
}

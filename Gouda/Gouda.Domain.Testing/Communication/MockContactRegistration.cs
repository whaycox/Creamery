using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Security;

namespace Gouda.Domain.Communication
{
    public class MockContactRegistration
    {
        private static int Users = MockUser.Samples.Length;
        private static int ContactID(int userID, int contactNum) => ((userID - 1) * Users) + contactNum;

        private static ContactRegistration Sample(int userID, int contactNum) => new ContactRegistration
        {
            ID = ContactID(userID, contactNum),
            ContactID = ContactID(userID, contactNum),
            UserID = userID,
            CronString = Testing.AlwaysCronString,
        };

        public static ContactRegistration[] Samples()
        {
            List<ContactRegistration> samples = new List<ContactRegistration>();
            foreach(User user in MockUser.Samples)
            {
                samples.Add(Sample(user.ID, 1));
                samples.Add(Sample(user.ID, 2));
                samples.Add(Sample(user.ID, 3));
            }
            return samples.ToArray();
        }
    }
}

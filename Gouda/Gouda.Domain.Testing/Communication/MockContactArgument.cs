using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Communication
{
    public class MockContactArgument
    {
        private const int Contacts = 9;
        private static int AbsoluteID(int contactID, int argument) => ((contactID - 1) * Contacts) + argument;

        private static ContactArgument One(int contactID) => new ContactArgument() { ID = AbsoluteID(contactID, 1), Name = nameof(One), Value = nameof(One), ContactID = contactID };
        private static ContactArgument Two(int contactID) => new ContactArgument() { ID = AbsoluteID(contactID, 2), Name = nameof(Two), Value = nameof(Two), ContactID = contactID };
        private static ContactArgument Three(int contactID) => new ContactArgument() { ID = AbsoluteID(contactID, 3), Name = nameof(Three), Value = nameof(Three), ContactID = contactID };
        private static ContactArgument Four(int contactID) => new ContactArgument() { ID = AbsoluteID(contactID, 4), Name = nameof(Four), Value = nameof(Four), ContactID = contactID };

        private static ContactArgument[] ArgumentsForContact(int contactID) => new ContactArgument[]
        {
            One(contactID),
            Two(contactID),
            Three(contactID),
            Four(contactID),
        };

        public static ContactArgument[] SampleArguments()
        {
            List<ContactArgument> samples = new List<ContactArgument>();
            for (int i = 1; i <= MockContact.Samples.Length; i++)
                samples.AddRange(ArgumentsForContact(i));
            return samples.ToArray();
        }
    }
}

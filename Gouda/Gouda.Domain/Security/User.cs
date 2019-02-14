using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Curds.Domain;

namespace Gouda.Domain.Security
{
    public class User : NamedEntity
    {
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }

        public override Entity Clone() => CloneInternal(new User());
        protected User CloneInternal(User clone)
        {
            clone.Email = Email;
            clone.Salt = Salt;
            clone.Password = Password;
            base.CloneInternal(clone);
            return clone;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int toReturn = StartHashCode();

                toReturn = PrimeHashCode(toReturn);
                toReturn = IncrementHashCode(toReturn, base.GetHashCode());

                toReturn = PrimeHashCode(toReturn);
                if (Email != null)
                    toReturn = IncrementHashCode(toReturn, Email.GetHashCode());

                toReturn = PrimeHashCode(toReturn);
                if (Salt != null)
                    toReturn = IncrementHashCode(toReturn, Salt.GetHashCode());

                toReturn = PrimeHashCode(toReturn);
                if (Password != null)
                    toReturn = IncrementHashCode(toReturn, Password.GetHashCode());

                return toReturn;
            }
        }
        public override bool Equals(object obj)
        {
            User toTest = obj as User;
            if (toTest == null)
                return false;
            if (!toTest.Email.CompareWithNull(Email))
                return false;
            if (!toTest.Salt.CompareWithNull(Salt))
                return false;
            if (!toTest.Password.CompareWithNull(Password))
                return false;
            return base.Equals(obj);
        }
    }
}

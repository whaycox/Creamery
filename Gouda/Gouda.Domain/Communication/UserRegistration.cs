using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Curds.Domain;

namespace Gouda.Domain.Communication
{
    public class UserRegistration : Entity, ICronEntity
    {
        public int DefinitionID { get; set; }
        public int UserID { get; set; }
        public string CronString { get; set; }

        public override Entity Clone() => CloneInternal(new UserRegistration());
        protected UserRegistration CloneInternal(UserRegistration clone)
        {
            clone.DefinitionID = DefinitionID;
            clone.UserID = UserID;
            clone.CronString = CronString;
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
                toReturn = IncrementHashCode(toReturn, DefinitionID.GetHashCode());

                toReturn = PrimeHashCode(toReturn);
                toReturn = IncrementHashCode(toReturn, UserID.GetHashCode());

                toReturn = PrimeHashCode(toReturn);
                if (CronString != null)
                    toReturn = IncrementHashCode(toReturn, CronString.GetHashCode());

                return toReturn;
            }
        }

        public override bool Equals(object obj)
        {
            UserRegistration toTest = obj as UserRegistration;
            if (toTest == null)
                return false;
            if (toTest.DefinitionID != DefinitionID)
                return false;
            if (toTest.UserID != UserID)
                return false;
            if (!toTest.CronString.CompareWithNull(CronString))
                return false;
            return base.Equals(obj);
        }
    }
}

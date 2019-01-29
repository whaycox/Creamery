using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;

namespace Gouda.Domain.Communication
{
    public class UserRegistration : Entity, ICronEntity
    {
        public int DefinitionID { get; set; }
        public int UserID { get; set; }
        public string CronString { get; set; }

        public override Entity Clone()
        {
            UserRegistration clone = new UserRegistration();
            clone.ID = ID;
            clone.DefinitionID = DefinitionID;
            clone.UserID = UserID;
            clone.CronString = CronString;

            return clone;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int toReturn = HashPrime1;

                toReturn *= HashPrime2;
                toReturn += base.GetHashCode();

                toReturn *= HashPrime2;
                toReturn += DefinitionID.GetHashCode();

                toReturn *= HashPrime2;
                toReturn += UserID.GetHashCode();

                toReturn *= HashPrime2;
                if (CronString != null)
                    toReturn += CronString.GetHashCode();

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

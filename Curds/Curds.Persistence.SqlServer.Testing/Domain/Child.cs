using System;

namespace Curds.Persistence.Domain
{
    public class Child : NamedEntity
    {
        public int ParentID { get; set; }

        public override int GetHashCode() => HashCode.Combine(
            ID,
            Name,
            ParentID);

        public override bool Equals(object obj) =>
            obj is Child child &&
            Equals(child);
        private bool Equals(Child child) =>
            child.ID == ID &&
            child.Name == Name &&
            child.ParentID == ParentID;
    }
}

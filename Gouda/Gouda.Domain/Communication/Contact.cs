using Curds.Domain.Persistence;
using Curds.Domain;
using System.Collections.Generic;

namespace Gouda.Domain.Communication
{
    using Enumerations;

    public class Contact : NamedEntity
    {
        public int UserID { get; set; }
        public ContactType Type { get; set; }

        public IEnumerable<ContactArgument> ContactArguments { get; set; }
    }
}

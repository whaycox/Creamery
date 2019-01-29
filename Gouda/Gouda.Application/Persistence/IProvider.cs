using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;
using Gouda.Domain;

namespace Gouda.Application.Persistence
{
    public interface IProvider
    {
        EntityCollection Load();
        IEnumerable<UserRegistration> LoadRegistrations(IEnumerable<Definition> definitions);
        IEnumerable<Contact> LoadContacts(IEnumerable<User> users);
    }
}

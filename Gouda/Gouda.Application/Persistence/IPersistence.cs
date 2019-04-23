using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;
using Gouda.Domain;
using Curds.Application.Persistence;
using Curds.Application.Cron;
using Gouda.Domain.Security;
using System.Threading.Tasks;
using Curds.Domain.Security;
using Curds.Application.Persistence.Persistor;

namespace Gouda.Application.Persistence
{
    public interface IPersistence : ISecurityPersistence
    {
        ICron Cron { get; }

        IEntityPersistor<Satellite> Satellites { get; }
        IEntityPersistor<Definition> Definitions { get; }
        IEntityPersistor<DefinitionRegistration> DefinitionRegistrations { get; }
        IEntityPersistor<DefinitionArgument> DefinitionArguments { get; }
        IEntityPersistor<Contact> Contacts { get; }
        IEntityPersistor<ContactRegistration> ContactRegistrations { get; }

        void Initialize();

        Task<User> FindByEmail(string email);
        Task AddSession(Session session);

        Task<List<Contact>> FilterContacts(int definitionID, DateTime eventTime);
        Task<List<DefinitionArgument>> GenerateArguments(int definitionID);
    }
}

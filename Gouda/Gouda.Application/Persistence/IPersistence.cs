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

namespace Gouda.Application.Persistence
{
    public interface IPersistence
    {
        ICron Cron { get; }

        IPersistor<Satellite> Satellites { get; }
        IPersistor<Definition> Definitions { get; }
        IPersistor<DefinitionArgument> DefinitionArguments { get; }
        IPersistor<Contact> Contacts { get; }
        IPersistor<User> Users { get; }

        Task<User> FindByEmail(string email);
        Task AddSession(Session session);

        Task<List<Contact>> FilterContacts(int definitionID, DateTime eventTime);
        Task<List<DefinitionArgument>> GenerateArguments(int definitionID);
    }
}

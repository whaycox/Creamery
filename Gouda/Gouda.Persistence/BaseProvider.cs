using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Cron;
using Curds.Application.Persistence;
using Gouda.Application.Persistence;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Security;
using System.Threading.Tasks;

namespace Gouda.Persistence
{
    public abstract class BaseProvider : IPersistence
    {
        public ICron Cron { get; set; }

        public abstract IPersistor<Satellite> Satellites { get; }
        public abstract IPersistor<Definition> Definitions { get; }
        public abstract IPersistor<DefinitionArgument> DefinitionArguments { get; }
        public abstract IPersistor<Contact> Contacts { get; }
        public abstract IPersistor<User> Users { get; }

        public BaseProvider(ICron cronProvider)
        {
            Cron = cronProvider;
        }

        public abstract Task<User> FindByEmail(string email);
        public abstract Task AddSession(Session session);

        public abstract Task<List<Contact>> FilterContacts(int definitionID, DateTime eventTime);
        public abstract Task<List<DefinitionArgument>> GenerateArguments(int definitionID);
    }
}

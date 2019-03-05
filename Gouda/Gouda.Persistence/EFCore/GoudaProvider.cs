using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Gouda.Application.Persistence;
using Curds.Application.Cron;
using Curds.Application.Persistence;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Security;
using System.Linq;
using System.Threading.Tasks;
using Curds;
using Curds.Persistence.EFCore;

namespace Gouda.Persistence.EFCore
{
    public abstract class GoudaProvider : EFProvider<GoudaContext>, IPersistence
    {
        public ICron Cron { get; }

        public IPersistor<Satellite> Satellites { get; }
        public IPersistor<Definition> Definitions { get; }
        public IPersistor<DefinitionArgument> DefinitionArguments { get; }
        public IPersistor<Contact> Contacts { get; }
        public IPersistor<User> Users { get; }

        public override GoudaContext Context => new GoudaContext(this);

        public GoudaProvider(ICron cronProvider)
        {
            Cron = cronProvider;

            Satellites = new Persistors.Satellite(this);
            Definitions = new Persistors.Definition(this);
            DefinitionArguments = new Persistors.DefinitionArgument(this);
            Contacts = new Persistors.Contact(this);
            Users = new Persistors.User(this);
        }

        public Task<User> FindByEmail(string email)
        {
            using (GoudaContext context = Context)
                return context.Users
                    .Where(u => u.Email == email)
                    .SingleAsync();
        }

        public async Task AddSession(Session session)
        {
            using (GoudaContext context = Context)
            {
                await context.Sessions.AddAsync(session);
                await context.SaveChangesAsync();
            }
        }

        public Task<List<DefinitionArgument>> GenerateArguments(int definitionID)
        {
            using (GoudaContext context = Context)
                return context.DefinitionArguments
                    .Where(d => d.DefinitionID == definitionID)
                    .ToListAsync();
        }

        public async Task<List<Contact>> FilterContacts(int definitionID, DateTime eventTime)
        {
            using (GoudaContext context = Context)
            {
                HashSet<int> interestedUsers = new HashSet<int>(await InterestedUsers(context, definitionID, eventTime));
                return await ActiveContacts(context, interestedUsers, eventTime);
            }
        }
        private Task<List<int>> InterestedUsers(GoudaContext context, int definitionID, DateTime eventTime) => 
            context.DefinitionRegistrations
                .Where(r => r.DefinitionID == definitionID)
                .Select(u => u.UserID)
                .ToListAsync();
        private Task<List<Contact>> ActiveContacts(GoudaContext context, HashSet<int> interestedUsers, DateTime eventTime) =>
            context.Contacts
                .Where(c => interestedUsers.Contains(c.UserID))
                .ToListAsync();
    }
}

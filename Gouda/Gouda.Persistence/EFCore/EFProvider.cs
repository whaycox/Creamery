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

namespace Gouda.Persistence.EFCore
{
    public abstract class EFProvider : BaseProvider
    {
        public override IPersistor<Satellite> Satellites { get; }
        public override IPersistor<Definition> Definitions { get; }
        public override IPersistor<DefinitionArgument> DefinitionArguments { get; }
        public override IPersistor<Contact> Contacts { get; }
        public override IPersistor<User> Users { get; }

        internal GoudaContext Context => new GoudaContext(this);

        public EFProvider(ICron cronProvider)
            : base(cronProvider)
        {
            Satellites = new Persistors.Satellite(this);
            Definitions = new Persistors.Definition(this);
            DefinitionArguments = new Persistors.DefinitionArgument(this);
            Contacts = new Persistors.Contact(this);
            Users = new Persistors.User(this);
        }

        public override Task<User> FindByEmail(string email)
        {
            using (GoudaContext context = Context)
                return context.Users
                    .Where(u => u.Email == email)
                    .SingleAsync();
        }

        public async override Task AddSession(Session session)
        {
            using (GoudaContext context = Context)
            {
                await context.Sessions.AddAsync(session);
                await context.SaveChangesAsync();
            }
        }

        public override Task<List<DefinitionArgument>> GenerateArguments(int definitionID)
        {
            using (GoudaContext context = Context)
                return context.DefinitionArguments
                    .Where(d => d.DefinitionID == definitionID)
                    .ToListAsync();
        }

        public async override Task<List<Contact>> FilterContacts(int definitionID, DateTime eventTime)
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

        internal virtual void SeedData(ModelBuilder modelBuilder) { }

        internal abstract void ConfigureContext(DbContextOptionsBuilder optionsBuilder);
    }
}

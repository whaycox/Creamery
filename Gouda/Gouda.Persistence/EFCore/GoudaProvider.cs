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

        private Persistors.CronPersistor<DefinitionRegistration> _definitionRegistrations = null;
        public IPersistor<DefinitionRegistration> DefinitionRegistrations => _definitionRegistrations;
        public IPersistor<DefinitionArgument> DefinitionArguments { get; }
        public IPersistor<Contact> Contacts { get; }

        private Persistors.CronPersistor<ContactRegistration> _contactRegistrations = null;
        public IPersistor<ContactRegistration> ContactRegistrations => _contactRegistrations;
        public IPersistor<User> Users { get; }

        public override GoudaContext Context => new GoudaContext(this);

        public GoudaProvider(ICron cronProvider)
        {
            Cron = cronProvider;

            Satellites = new Persistors.Satellite(this);
            Definitions = new Persistors.Definition(this);
            _definitionRegistrations = new Persistors.DefinitionRegistration(this);
            DefinitionArguments = new Persistors.DefinitionArgument(this);
            Contacts = new Persistors.Contact(this);
            _contactRegistrations = new Persistors.ContactRegistration(this);
            Users = new Persistors.User(this);
        }

        public void Initialize()
        {
            _definitionRegistrations.LoadEntities();
            _contactRegistrations.LoadEntities();
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
            var interestedUsers = _definitionRegistrations.Filter(definitionID, eventTime);
            var activeContacts = _contactRegistrations.FilterMany(interestedUsers, eventTime);
            return await Contacts.LookupMany(activeContacts);
        }
    }
}

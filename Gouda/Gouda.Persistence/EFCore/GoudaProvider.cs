using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Gouda.Application.Persistence;
using Curds.Application.Cron;
using Curds.Application.Persistence.Persistor;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Security;
using System.Linq;
using System.Threading.Tasks;
using Curds;
using Curds.Persistence.EFCore;
using Curds.Domain.Security;

namespace Gouda.Persistence.EFCore
{
    public abstract class GoudaProvider : EFProvider<GoudaContext>, IPersistence
    {
        public ICron Cron { get; }

        public IEntityPersistor<Satellite> Satellites { get; }
        public IEntityPersistor<Definition> Definitions { get; }

        private Persistor.CronPersistor<DefinitionRegistration> _definitionRegistrations = null;
        public IEntityPersistor<DefinitionRegistration> DefinitionRegistrations => _definitionRegistrations;
        public IEntityPersistor<DefinitionArgument> DefinitionArguments { get; }
        public IEntityPersistor<Contact> Contacts { get; }

        private Persistor.CronPersistor<ContactRegistration> _contactRegistrations = null;
        public IEntityPersistor<ContactRegistration> ContactRegistrations => _contactRegistrations;
        public IUserPersistor<User> Users { get; }
        public ISessionPersistor<Session> Sessions { get; }
        public IReAuthPersistor<ReAuth> ReAuthentications { get; }

        protected override GoudaContext ContextInternal => new GoudaContext(this);

        public GoudaProvider(ICron cronProvider)
        {
            Cron = cronProvider;

            Satellites = new Persistor.Satellite(this);
            Definitions = new Persistor.Definition(this);
            _definitionRegistrations = new Persistor.DefinitionRegistration(this);
            DefinitionArguments = new Persistor.DefinitionArgument(this);
            Contacts = new Persistor.Contact(this);
            _contactRegistrations = new Persistor.ContactRegistration(this);
            Users = new Persistor.User(this);
        }

        public void Initialize()
        {
            _definitionRegistrations.LoadEntities();
            _contactRegistrations.LoadEntities();
        }

        public Task<User> FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task AddSession(Session session)
        {
            throw new NotImplementedException();
            //using (GoudaContext context = Context)
            //{
            //    await context.Sessions.AddAsync(session);
            //    await context.SaveChangesAsync();
            //}
        }

        public Task<List<DefinitionArgument>> GenerateArguments(int definitionID)
        {
            using (GoudaContext context = ContextInternal)
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

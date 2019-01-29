using Gouda.Application.Communication;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using Gouda.Application;
using Curds.Application;

namespace Gouda.Infrastructure.Communication
{
    public abstract class BaseNotifier : INotifier
    {
        private Dictionary<Type, IContactAdapter> Adapters = new Dictionary<Type, IContactAdapter>();
        private Dictionary<int, List<CronUser>> RegistrationsByDefinition = new Dictionary<int, List<CronUser>>();
        private Dictionary<int, List<CronContact>> ContactsByUser = new Dictionary<int, List<CronContact>>();

        public Curds.Application.Cron.IProvider Cron { get; set; }
        public Curds.Application.DateTimes.IProvider Time { get; set; }

        private DateTime FetchTime => Time.Fetch.LocalDateTime;

        public BaseNotifier()
        {
            LoadAdapters();
        }
        protected virtual void LoadAdapters() { }

        protected void AddAdapter(Type type, IContactAdapter adapter) => Adapters.Add(type, adapter);

        public void AddContact(Contact contact)
        {
            CronContact cronContact = new CronContact(Cron, contact);
            if (!ContactsByUser.ContainsKey(cronContact.UserID))
                ContactsByUser.Add(cronContact.UserID, new List<CronContact>());
            ContactsByUser[cronContact.UserID].Add(cronContact);
        }

        public void AddRegistration(UserRegistration registration)
        {
            CronUser userRegistration = new CronUser(Cron, registration);
            if (!RegistrationsByDefinition.ContainsKey(userRegistration.DefinitionID))
                RegistrationsByDefinition.Add(userRegistration.DefinitionID, new List<CronUser>());
            RegistrationsByDefinition[userRegistration.DefinitionID].Add(userRegistration);
        }

        public void NotifyUsers(object sender, StatusChanged eventArgs)
        {
            DateTime testTime = FetchTime;
            NotifyContacts(RelevantContacts(InterestedUsers(eventArgs.Definition.ID, testTime), testTime));
        }
        private List<int> InterestedUsers(int definitionID, DateTime testTime)
        {
            if (!RegistrationsByDefinition.ContainsKey(definitionID))
                return new List<int>();
            else
                return RegistrationsByDefinition[definitionID]
                    .Where(r => r.Expression.Test(testTime))
                    .Select(r => r.UserID)
                    .ToList();
        }
        private List<Contact> RelevantContacts(List<int> users, DateTime testTime) => users.SelectMany(u => UserContacts(u, testTime)).ToList();
        private IEnumerable<Contact> UserContacts(int userID, DateTime testTime)
        {
            if (!ContactsByUser.ContainsKey(userID))
                return new List<Contact>();
            else
                return ContactsByUser[userID]
                    .Where(c => c.Expression.Test(testTime))
                    .Select(c => c.Contact)
                    .ToList();
        }

        private void NotifyContacts(List<Contact> contacts)
        {
            foreach(Contact contact in contacts)
            {
                Type contactType = contact.GetType();
                Adapters[contactType].Notify(contact);
            }
        }

        private class CronUser : CronPair<UserRegistration>
        {
            public int DefinitionID => Value.DefinitionID;
            public int UserID => Value.UserID;

            public CronUser(Curds.Application.Cron.IProvider provider, UserRegistration registration)
                : base(provider, registration)
            { }
        }

        private class CronContact : CronPair<Contact>
        {
            public int UserID => Value.UserID;
            public Contact Contact => Value;

            public CronContact(Curds.Application.Cron.IProvider provider, Contact contact)
                : base(provider, contact)
            { }
        }
    }
}

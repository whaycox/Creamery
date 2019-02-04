using Gouda.Application.Communication;
using Gouda.Domain.Communication;
using Gouda.Domain.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gouda.Infrastructure.Communication
{
    public abstract class Notifier : INotifier
    {
        private Dictionary<Type, IContactAdapter> Adapters = new Dictionary<Type, IContactAdapter>();

        public Curds.Application.DateTimes.IProvider Time { get; set; }
        public Application.Persistence.IProvider Persistence { get; set; }

        private DateTime FetchTime => Time.Fetch.LocalDateTime;

        public Notifier()
        {
            LoadAdapters();
        }
        protected virtual void LoadAdapters() { }

        protected void AddAdapter(Type type, IContactAdapter adapter) => Adapters.Add(type, adapter);

        public void NotifyUsers(object sender, StatusChanged eventArgs) => NotifyContacts(Persistence.FilterContacts(eventArgs.Definition.ID, FetchTime));
        private void NotifyContacts(IEnumerable<Contact> contacts)
        {
            foreach(Contact contact in contacts)
            {
                Type contactType = contact.GetType();
                Adapters[contactType].Notify(contact);
            }
        }
    }
}

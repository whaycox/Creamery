using Gouda.Application.Communication;
using Gouda.Domain.Communication;
using Gouda.Domain.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gouda.Infrastructure.Communication
{
    public class Notifier : INotifier
    {
        private Dictionary<Type, IContactAdapter> Adapters = new Dictionary<Type, IContactAdapter>();

        public Curds.Application.DateTimes.IProvider Time { get; set; }
        public Application.Persistence.IProvider Persistence { get; set; }

        private DateTime FetchTime => Time.Fetch.LocalDateTime;

        public Notifier()
        {
            LoadAdapters();
        }
        private void LoadAdapters()
        {
            foreach(string nameSpace in LoadableItems.IContactAdapterNamespaces)
                foreach (var adapterPair in AppDomain.CurrentDomain.LoadKeyInstancePairs<Type, BaseContactAdapter>(nameSpace, KeySelector))
                    AddAdapter(adapterPair.key, adapterPair.instance);
        }
        private Type KeySelector(BaseContactAdapter adapter) => adapter.GetType().BaseType.GetGenericArguments().First();
        private void AddAdapter(Type type, BaseContactAdapter adapter) => Adapters.Add(type, adapter);

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

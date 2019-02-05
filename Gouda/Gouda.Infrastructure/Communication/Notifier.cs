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
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                LoadNamespaceFromAssembly(assembly);
        }
        private void LoadNamespaceFromAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes().Where(t => typeof(IContactAdapter).IsAssignableFrom(t)))
                LoadType(type);
        }
        private void LoadType(Type type)
        {
            var ctor = type.GetConstructor(new Type[0]);
            if (ctor != null && type.BaseType.IsGenericType)
            {
                Type keyType = type.BaseType.GetGenericArguments()[0];
                AddAdapter(keyType, ctor.Invoke(null) as IContactAdapter);
            }
        }
        private void AddAdapter(Type type, IContactAdapter adapter) => Adapters.Add(type, adapter);

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

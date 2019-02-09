using Gouda.Application.Communication;
using Gouda.Domain.Communication;
using Gouda.Domain.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gouda.Infrastructure.Communication
{
    public class Notifier : ReflectionLoader<Type, BaseContactAdapter>, INotifier
    {
        public Curds.Application.DateTimes.IProvider Time { get; set; }
        public Application.Persistence.IProvider Persistence { get; set; }

        private DateTime FetchTime => Time.Fetch.LocalDateTime;

        protected override IEnumerable<string> NamespacesToSearch => LoadableItems.IContactAdapterNamespaces;

        public void NotifyUsers(object sender, StatusChanged eventArgs) => NotifyContacts(Persistence.FilterContacts(eventArgs.Definition.ID, FetchTime));
        private void NotifyContacts(IEnumerable<Contact> contacts)
        {
            foreach(Contact contact in contacts)
            {
                Type contactType = contact.GetType();
                Loaded[contactType].Notify(contact);
            }
        }

        protected override Type KeySelector(BaseContactAdapter instance) => instance.GetType().BaseType.GetGenericArguments().First();
    }
}

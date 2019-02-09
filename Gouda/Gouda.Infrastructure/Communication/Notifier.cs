using Gouda.Application.Communication;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gouda.Infrastructure.Communication
{
    public class Notifier : ReflectionLoader<Type, BaseContactAdapter>, INotifier
    {
        public Curds.Application.DateTimes.IDateTime Time { get; set; }
        public Application.Persistence.IPersistence Persistence { get; set; }

        private DateTime FetchTime => Time.Fetch.LocalDateTime;

        protected override IEnumerable<string> NamespacesToSearch => LoadableItems.IContactAdapterNamespaces;

        public void NotifyUsers(StatusChange changeInformation) => NotifyContacts(Persistence.FilterContacts(changeInformation.Definition.ID, FetchTime), changeInformation);
        private void NotifyContacts(IEnumerable<Contact> contacts, StatusChange changeInformation)
        {
            foreach(Contact contact in contacts)
            {
                Type contactType = contact.GetType();
                Loaded[contactType].Notify(contact, changeInformation);
            }
        }

        protected override Type KeySelector(BaseContactAdapter instance) => instance.GetType().BaseType.GetGenericArguments().First();
    }
}

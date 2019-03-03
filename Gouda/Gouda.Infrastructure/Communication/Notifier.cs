using Gouda.Application.Communication;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Curds.Application.DateTimes;
using Gouda.Application.Persistence;
using System.Threading.Tasks;
using Gouda.Domain.Enumerations;

namespace Gouda.Infrastructure.Communication
{
    public class Notifier : ReflectionLoader<ContactType, BaseContactAdapter>, INotifier
    {
        public IDateTime Time { get; set; }
        public IPersistence Persistence { get; set; }

        private DateTime FetchTime => Time.Fetch.LocalDateTime;

        protected override IEnumerable<string> NamespacesToSearch => LoadableItems.IContactAdapterNamespaces;

        public Notifier(IDateTime time, IPersistence persistence)
        {
            Time = time;
            Persistence = persistence;
        }

        public async Task NotifyUsers(StatusChange changeInformation) => NotifyContacts(await Persistence.FilterContacts(changeInformation.Definition.ID, FetchTime), changeInformation);
        private void NotifyContacts(IEnumerable<Contact> contacts, StatusChange changeInformation)
        {
            foreach (Contact contact in contacts)
                Loaded[contact.Type].Notify(contact, changeInformation);
        }

        protected override ContactType KeySelector(BaseContactAdapter instance) => instance.HandledType;
    }
}

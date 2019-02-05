using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;
using Gouda.Domain;
using Curds.Application.Persistence;

namespace Gouda.Application.Persistence
{
    public interface IProvider
    {
        Curds.Application.Cron.IProvider Cron { get; set; }

        IPersistor<Satellite> Satellites { get; }
        IPersistor<Definition> Definitions { get; }
        IPersistor<Argument> Arguments { get; }
        IPersistor<Contact> Contacts { get; }
        IPersistor<User> Users { get; }

        IEnumerable<Contact> FilterContacts(int definitionID, DateTime eventTime);
    }
}

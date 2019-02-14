using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;
using Gouda.Domain;
using Curds.Application.Persistence;
using Curds.Application.Cron;
using Gouda.Domain.Security;

namespace Gouda.Application.Persistence
{
    public interface IPersistence
    {
        ICron Cron { get; set; }

        IPersistor<Satellite> Satellites { get; }
        IPersistor<Definition> Definitions { get; }
        IPersistor<Argument> Arguments { get; }
        IPersistor<Contact> Contacts { get; }
        IPersistor<User> Users { get; }

        IEnumerable<Contact> FilterContacts(int definitionID, DateTime eventTime);
        IEnumerable<Argument> GenerateArguments(int definitionID);
    }
}

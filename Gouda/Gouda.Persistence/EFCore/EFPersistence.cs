using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Gouda.Application.Persistence;
using Curds.Application.Cron;
using Curds.Application.Persistence;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Security;

namespace Gouda.Persistence.EFCore
{
    public class EFPersistence : IPersistence
    {
        private GoudaContext Context { get; }

        public ICron Cron { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IPersistor<Satellite> Satellites => throw new NotImplementedException();

        public IPersistor<Definition> Definitions => throw new NotImplementedException();

        public IPersistor<Argument> Arguments => throw new NotImplementedException();

        public IPersistor<Contact> Contacts => throw new NotImplementedException();

        public IPersistor<User> Users => throw new NotImplementedException();

        public EFPersistence()
        {
            Context = new GoudaContext();
        }

        public IEnumerable<Contact> FilterContacts(int definitionID, DateTime eventTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Argument> GenerateArguments(int definitionID)
        {
            throw new NotImplementedException();
        }
    }
}

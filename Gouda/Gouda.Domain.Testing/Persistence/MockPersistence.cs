﻿using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Persistence;
using Curds.Application.Cron;
using Curds.Application.Persistence;

namespace Gouda.Domain.Persistence
{
    using Communication;
    using Check;
    using Security;

    public class MockPersistence : IPersistence
    {
        public ICron Cron { get; set; }

        public IPersistor<Satellite> Satellites => throw new NotImplementedException();

        public IPersistor<Definition> Definitions => throw new NotImplementedException();

        public IPersistor<Argument> Arguments => throw new NotImplementedException();

        public IPersistor<Contact> Contacts => throw new NotImplementedException();

        public IPersistor<User> Users => throw new NotImplementedException();

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

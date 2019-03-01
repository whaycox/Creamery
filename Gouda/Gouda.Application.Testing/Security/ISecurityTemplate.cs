﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain;
using Gouda.Domain.Persistence;
using Curds.Application.Security;

namespace Gouda.Application.Security
{
    public abstract class ISecurityTemplate<T> : IAuthenticatorTemplate<T> where T : ISecurity
    {
        MockPersistence Persistence = null;

        [TestInitialize]
        public void Init()
        {
            Persistence = new MockPersistence(Cron);
        }
    }
}

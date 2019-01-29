using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Domain.Tests
{
    [TestClass]
    public class User : NamedEntityTemplate<Domain.User>
    {
        protected override Domain.User Sample => MockUser.One;
    }
}

using Curds.Cron.Parser.Handler.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.Parser.Handler.Tests
{
    [TestClass]
    public class Definite : Template.Definite<Implementation.Definite>
    {
        protected override Implementation.Definite TestObject { get; } = new Implementation.Definite(null);

        protected override Implementation.Definite Build(ParsingHandler successor) => new Implementation.Definite(successor);
    }
}

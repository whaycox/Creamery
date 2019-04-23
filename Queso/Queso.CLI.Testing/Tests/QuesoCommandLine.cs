using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.CLI;
using Queso.Application;
using Queso.Domain;
using Queso.Domain.TestCharacters;
using Curds.CLI.Formatting;

namespace Queso.CLI.Tests
{
    [TestClass]
    public class QuesoCommandLine : FormattingTemplate<CLI.QuesoCommandLine>
    {
        private QuesoOptions Options = new MockOptions();
        private QuesoApplication Application => new QuesoApplication(Options);
        private MockConsoleWriter Writer = new MockConsoleWriter();

        private CLI.QuesoCommandLine _obj = null;
        protected override CLI.QuesoCommandLine TestObject => _obj;

        [TestInitialize]
        public void Init()
        {
            _obj = new CLI.QuesoCommandLine(Application, Writer);
        }

        [TestMethod]
        public void ResurrectShowsAScan()
        {
            using (DisposableCharacter tempCharacter = new DisposableCharacter(Files.Dead))
            {
                string[] resurrectArgs = new string[] { "-r", tempCharacter.Path };
                TestObject.Execute(resurrectArgs);

                Assert.AreEqual(13, Writer.Writes.Count);
                Writer.StartsWith(NewLine(true))
                    .ThenHas(NewLine(false))
                    .ThenHas("Name: Dead")
                    .ThenHas(Environment.NewLine)
                    .ThenHas(NewLine(true))
                    .ThenHas(NewLine(false))
                    .ThenHas("Class: Necromancer")
                    .ThenHas(Environment.NewLine)
                    .ThenHas(NewLine(true))
                    .ThenHas(NewLine(false))
                    .ThenHas("IsAlive? True")
                    .ThenHas(Environment.NewLine)
                    .ThenHas(NewLine(true));
            }
        }

    }
}

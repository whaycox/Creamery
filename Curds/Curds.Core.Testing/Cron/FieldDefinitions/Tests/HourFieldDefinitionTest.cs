using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.FieldDefinitions.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Template;

    [TestClass]
    public class HourFieldDefinitionTest : BaseFieldDefinitionTemplate
    {
        protected override ICronFieldDefinition InterfaceTestObject { get; } = new HourFieldDefinition();

        protected override int ExpectedMin => 0;
        protected override int ExpectedMax => 23;

        protected override Func<DateTime, int> ExpectedDatePart => (time) => time.Hour;
    }
}

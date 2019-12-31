using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.FieldDefinitions.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Template;

    [TestClass]
    public class MonthFieldDefinitionTest : BaseFieldDefinitionTemplate
    {
        protected override ICronFieldDefinition InterfaceTestObject { get; } = new MonthFieldDefinition();

        protected override int ExpectedMin => 1;
        protected override int ExpectedMax => 12;

        protected override Func<DateTime, int> ExpectedDatePart => (time) => time.Month;
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.FieldDefinitions.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Template;

    [TestClass]
    public class DayOfMonthFieldDefinitionTest : BaseFieldDefinitionTemplate
    {
        protected override ICronFieldDefinition InterfaceTestObject { get; } = new DayOfMonthFieldDefinition();

        protected override int ExpectedMin => 1;
        protected override int ExpectedMax => 31;

        protected override Func<DateTime, int> ExpectedDatePart => (time) => time.Day;
    }
}

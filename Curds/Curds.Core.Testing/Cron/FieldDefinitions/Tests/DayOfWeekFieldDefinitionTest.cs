using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.FieldDefinitions.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Template;

    [TestClass]
    public class DayOfWeekFieldDefinitionTest : BaseFieldDefinitionTemplate
    {
        protected override ICronFieldDefinition InterfaceTestObject { get; } = new DayOfWeekFieldDefinition();

        protected override int ExpectedMin => 0;
        protected override int ExpectedMax => 6;

        protected override Func<DateTime, int> ExpectedDatePart => (time) => (int)time.DayOfWeek;
    }
}

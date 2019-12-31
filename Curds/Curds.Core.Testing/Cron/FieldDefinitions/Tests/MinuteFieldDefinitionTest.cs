using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Cron.FieldDefinitions.Tests
{
    using Cron.Abstraction;
    using Implementation;
    using Template;

    [TestClass]
    public class MinuteFieldDefinitionTest : BaseFieldDefinitionTemplate
    {
        protected override ICronFieldDefinition InterfaceTestObject { get; } = new MinuteFieldDefinition();

        protected override int ExpectedMin => 0;
        protected override int ExpectedMax => 59;

        protected override Func<DateTime, int> ExpectedDatePart => (time) => time.Minute;
    }
}

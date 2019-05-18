using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.Parser.Tests
{
    [TestClass]
    public class Basic : Template.Basic<Domain.Basic>
    {
        protected override Domain.Basic TestObject { get; } = new Domain.Basic();
    }
}

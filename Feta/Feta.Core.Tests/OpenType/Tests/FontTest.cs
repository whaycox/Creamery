using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds;
using System.IO;

namespace Feta.OpenType.Tests
{
    [TestClass]
    public class FontTest : Test
    {

        [TestMethod]
        public void General()
        {
            foreach (var file in Directory.EnumerateFiles(@"E:\Source\TestFonts", "*", SearchOption.AllDirectories))
                Domain.Font.Read(file);
            throw new NotImplementedException();
        }

    }
}

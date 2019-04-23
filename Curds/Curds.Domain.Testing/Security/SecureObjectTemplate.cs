using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Curds.Domain.Security
{
    public abstract class SecureObjectTemplate : TestTemplate
    {
        protected const string Base64RegexPattern = "^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$";

        protected int Base64Length(int byteLength)
        {
            int expanded = byteLength * 4 / 3;
            int remainder = expanded % 4;
            int paddedChars = remainder == 0 ? 0 : 4 - remainder;
            return expanded + paddedChars;
        }

        protected void VerifyBase64(string base64String, int byteLength)
        {
            Assert.AreEqual(Base64Length(byteLength), base64String.Length);
            Assert.IsTrue(Regex.IsMatch(base64String, Base64RegexPattern));
            byte[] converted = Convert.FromBase64String(base64String);
            Assert.AreEqual(byteLength, converted.Length);
        }
    }
}

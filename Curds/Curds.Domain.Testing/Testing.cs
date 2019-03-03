using System;
using System.Collections.Generic;
using System.Text;

namespace Curds
{
    public static class Testing
    {
        public static string TestPassword => nameof(TestPassword);
        public static string TestSalt => "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
        public static string TestEncryptedPassword => "1A09BB96947632F35E9D9D4CB3132160E343EAE9EF82396E0D2E2068007D69B8";
    }
}

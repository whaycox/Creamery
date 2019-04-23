using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Persistence.EFCore.Seeds
{
    public static class User
    {
        public static Curds.Domain.Security.User[] Data => Curds.Domain.Security.MockUser.Samples;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Persistence.Seeds
{
    public static class User
    {
        public static Security.User[] Data => Security.MockUser.Samples;
    }
}

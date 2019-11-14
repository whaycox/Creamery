using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Gouda.Persistence.Domain
{
    public class GoudaContext : DbContext
    {
        public GoudaContext(DbContextOptions options)
            : base(options)
        { }
    }
}

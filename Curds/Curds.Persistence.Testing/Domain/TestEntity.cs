using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Domain
{
    public class TestEntity : SimpleEntity
    {
        public string Name { get; set; } = nameof(TestEntity);
    }
}

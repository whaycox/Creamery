using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds
{
    public static class Extensions
    {
        public static void AwaitResult(this Task task) => task.GetAwaiter().GetResult();
        public static T AwaitResult<T>(this Task<T> task) => task.GetAwaiter().GetResult();
    }
}

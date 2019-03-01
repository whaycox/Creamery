using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using System.Net;

namespace Gouda.Persistence
{
    internal static class TypeConverters
    {
        private const char IPEndpoindSeparator = ':';

        public static string Serialize(IPEndPoint endpoint) => $"{endpoint.Address}{IPEndpoindSeparator}{endpoint.Port}";
        public static IPEndPoint Deserialize(string value)
        {
            string[] parts = value.Split(IPEndpoindSeparator);
            return new IPEndPoint(IPAddress.Parse(parts[0]), int.Parse(parts[1]));
        }
    }
}

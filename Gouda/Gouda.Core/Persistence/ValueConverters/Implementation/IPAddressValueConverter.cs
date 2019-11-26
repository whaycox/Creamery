using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net;

namespace Gouda.Persistence.ValueConverters.Implementation
{
    public class IPAddressValueConverter : ValueConverter<IPAddress, byte[]>
    {
        private static IPAddressValueConverter _instance = null;
        public static IPAddressValueConverter Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IPAddressValueConverter();
                return _instance;
            }
        }

        private IPAddressValueConverter()
            : base(address => address.GetAddressBytes(), 
                  bytes => new IPAddress(bytes))
        { }
    }
}

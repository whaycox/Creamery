using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Curds.Domain.Persistence;

namespace Gouda.Domain.Communication
{
    using Check;

    public class Satellite : NamedEntity
    {
        public const int DefaultPort = 9326;

        public IPEndPoint Endpoint { get; set; }

        public override Entity Clone()
        {
            Satellite clone = new Satellite();
            clone.ID = ID;
            clone.Name = Name;
            clone.Endpoint = Clone(Endpoint);

            return clone;
        }
        private IPEndPoint Clone(IPEndPoint starting)
        {
            IPAddress address = new IPAddress(starting.Address.GetAddressBytes());
            return new IPEndPoint(address, starting.Port);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int toReturn = HashPrime1;

                toReturn *= HashPrime2;
                toReturn += base.GetHashCode();

                toReturn *= HashPrime2;
                if (Endpoint != null)
                    toReturn += Endpoint.GetHashCode();

                return toReturn;
            }
        }
        public override bool Equals(object obj)
        {
            Satellite toTest = obj as Satellite;
            if (toTest == null)
                return false;
            if (!toTest.Endpoint.CompareWithNull(Endpoint))
                return false;
            return base.Equals(obj);
        }
    }
}

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

        public override Entity Clone() => CloneInternal(new Satellite());
        protected Satellite CloneInternal(Satellite clone)
        {
            clone.Endpoint = Clone(Endpoint);
            base.CloneInternal(clone);
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
                int toReturn = StartHashCode();

                toReturn = PrimeHashCode(toReturn);
                toReturn = IncrementHashCode(toReturn, base.GetHashCode());

                toReturn = PrimeHashCode(toReturn);
                if (Endpoint != null)
                    toReturn = IncrementHashCode(toReturn, Endpoint.GetHashCode());

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

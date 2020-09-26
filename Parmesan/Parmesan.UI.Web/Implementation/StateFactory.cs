using System;

namespace Parmesan.UI.Web.Implementation
{
    using Abstraction;

    internal class StateFactory : IStateFactory
    {
        private const int LengthInBytes = 12;

        private Random Random { get; } = new Random();

        public string Generate()
        {
            byte[] state = new byte[LengthInBytes];
            Random.NextBytes(state);
            return Convert.ToBase64String(state);
        }
    }
}

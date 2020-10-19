using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Parmesan.Implementation
{
    using Abstraction;

    internal class SecureRandom : IDisposable, ISecureRandom
    {
        private bool disposedValue;

        private RandomNumberGenerator Random { get; } = RandomNumberGenerator.Create();

        public Task<string> GenerateAsync(int bytes) => Task.FromResult(Generate(bytes));
        public string Generate(int bytes)
        {
            byte[] byteArray = new byte[bytes];
            Random.GetBytes(byteArray);
            return byteArray.Base64UrlEncode();
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Random.Dispose();
                }
                disposedValue = true;
            }
        }
    }
}

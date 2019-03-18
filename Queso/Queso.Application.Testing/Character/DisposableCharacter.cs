using System;
using System.IO;
using System.Text;

namespace Queso.Application.Character
{
    public class DisposableCharacter : IDisposable
    {
        private const string Temporary = nameof(Temporary);
        private const int NameLengthInBytes = 8;

        private static Random Rando = new Random();

        private string Source { get; }

        public string Path { get; }

        public DisposableCharacter(string sourceCharacter)
        {
            Source = sourceCharacter;

            Path = GenerateTemporaryPath();
            MakeTemporaryCharacter();            
        }

        private string GenerateTemporaryPath()
        {
            FileInfo file = new FileInfo(Source);
            return System.IO.Path.Combine(file.Directory.FullName, Temporary, RandomName(file.Extension));
        }
        private string RandomName(string extension)
        {
            byte[] buffer = new byte[NameLengthInBytes];
            Rando.NextBytes(buffer);

            StringBuilder toReturn = new StringBuilder();
            foreach (byte piece in buffer)
                toReturn.Append(piece.ToString("X2"));
            toReturn.Append(extension);
            return toReturn.ToString();
        }

        private void MakeTemporaryCharacter()
        {
            FileInfo file = new FileInfo(Path);
            if (!file.Directory.Exists)
                Directory.CreateDirectory(file.Directory.FullName);
            File.Copy(Source, Path);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    File.Delete(Path);
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using System.Threading.Tasks;

    internal class SqlQueryReader : ISqlQueryReader
    {
        private SqlDataReader DataReader { get; }

        public SqlQueryReader(SqlDataReader dataReader)
        {
            DataReader = dataReader;
        }

        public Task<bool> Advance() => DataReader.ReadAsync();

        public byte? ReadByte(int index)
        {
            throw new NotImplementedException();
        }

        public short? ReadShort(int index)
        {
            throw new NotImplementedException();
        }
        public int? ReadInt(int index)
        {
            if (DataReader.IsDBNull(index))
                return null;
            return DataReader.GetInt32(index);
        }

        public long? ReadLong(int index)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DataReader?.Dispose();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}

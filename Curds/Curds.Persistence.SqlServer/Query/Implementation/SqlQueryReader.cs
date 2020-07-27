using System;
using System.Data.SqlClient;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Domain;
    using System.Threading.Tasks;

    internal class SqlQueryReader : ISqlQueryReader
    {
        private SqlDataReader DataReader { get; }

        public SqlQueryReader(SqlDataReader dataReader)
        {
            DataReader = dataReader;
        }

        public Task<bool> Advance() => DataReader.ReadAsync();

        private int GetIndex(string columnName)
        {
            try
            {
                return DataReader.GetOrdinal(columnName);
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidColumnNameException(columnName);
            }
        }

        public string ReadString(string columnName)
        {
            int index = GetIndex(columnName);
            if (DataReader.IsDBNull(index))
                return null;
            return DataReader.GetString(index);
        }

        public bool? ReadBool(string columnName)
        {
            int index = GetIndex(columnName);
            if (DataReader.IsDBNull(index))
                return null;
            return DataReader.GetBoolean(index);
        }

        public byte? ReadByte(string columnName)
        {
            int index = GetIndex(columnName);
            if (DataReader.IsDBNull(index))
                return null;
            return DataReader.GetByte(index);
        }

        public short? ReadShort(string columnName)
        {
            int index = GetIndex(columnName);
            if (DataReader.IsDBNull(index))
                return null;
            return DataReader.GetInt16(index);
        }

        public int? ReadInt(string columnName)
        {
            int index = GetIndex(columnName);
            if (DataReader.IsDBNull(index))
                return null;
            return DataReader.GetInt32(index);
        }

        public long? ReadLong(string columnName)
        {
            int index = GetIndex(columnName);
            if (DataReader.IsDBNull(index))
                return null;
            return DataReader.GetInt64(index);
        }

        public DateTime? ReadDateTime(string columnName)
        {
            int index = GetIndex(columnName);
            if (DataReader.IsDBNull(index))
                return null;
            return DataReader.GetDateTime(index);
        }

        public DateTimeOffset? ReadDateTimeOffset(string columnName)
        {
            int index = GetIndex(columnName);
            if (DataReader.IsDBNull(index))
                return null;
            return DataReader.GetDateTimeOffset(index);
        }

        public decimal? ReadDecimal(string columnName)
        {
            int index = GetIndex(columnName);
            if (DataReader.IsDBNull(index))
                return null;
            return DataReader.GetDecimal(index);
        }

        public double? ReadDouble(string columnName)
        {
            int index = GetIndex(columnName);
            if (DataReader.IsDBNull(index))
                return null;
            return DataReader.GetDouble(index);
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

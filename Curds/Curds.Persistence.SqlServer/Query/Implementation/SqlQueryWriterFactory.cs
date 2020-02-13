namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;

    internal class SqlQueryWriterFactory : ISqlQueryWriterFactory
    {
        public ISqlQueryWriter Create() => new SqlQueryWriter(CreateParameterBuilder());
        private SqlQueryParameterBuilder CreateParameterBuilder() => new SqlQueryParameterBuilder();
    }
}

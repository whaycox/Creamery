namespace Curds.Persistence.Model.Configuration.Domain
{
    using Abstraction;

    public abstract class BaseColumnConfiguration : IColumnConfiguration
    {
        public string ValueName { get; }

        public string Name { get; set; }
        public bool? IsIdentity { get; set; }

        public BaseColumnConfiguration(string valueName)
        {
            ValueName = valueName;
        }
    }
}

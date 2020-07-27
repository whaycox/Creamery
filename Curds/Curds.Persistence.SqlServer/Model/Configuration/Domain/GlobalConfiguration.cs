namespace Curds.Persistence.Model.Configuration.Domain
{
    using Abstraction;

    public class GlobalConfiguration : IGlobalConfiguration
    {
        public string Schema { get; set; }
    }
}

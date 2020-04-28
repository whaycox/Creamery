using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Configuration.Domain
{
    using Abstraction;
    using Persistence.Abstraction;
    using Model.Abstraction;

    public class CompiledColumnConfiguration<TModel> : IColumnConfiguration
        where TModel : IDataModel
    {
        public string ValueName { get; }

        public string Name { get; set; }
        public bool IsIdentity { get; set; }
        bool? IColumnConfiguration.IsIdentity => IsIdentity;

        public CompiledColumnConfiguration(string valueName)
        {
            ValueName = valueName;
        }
    }
}

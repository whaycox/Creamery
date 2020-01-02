using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Curds.Cron.RangeFactories.Links.Implementation
{
    using Cron.Abstraction;
    using Ranges.Implementation;
    using Abstraction;

    internal class SingleValueLink<TFieldDefinition> : BaseLink<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        public SingleValueLink(
            TFieldDefinition fieldDefinition,
            IRangeFactoryLink successor)
            : base(fieldDefinition, successor)
        { }

        public override ICronRange HandleParse(string range) =>
            new SingleValueRange<TFieldDefinition>(FieldDefinition, FieldDefinition.Parse(range));
    }
}

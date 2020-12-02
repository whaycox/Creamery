using System.Linq.Expressions;

namespace Curds.Clone.Domain
{
    internal class CloneParameters
    {
        public ParameterExpression SourceEntity { get; set; }
        public ParameterExpression TargetEntity { get; set; }

        public ParameterExpression CloneFactory { get; set; }
    }
}

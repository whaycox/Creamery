using System;

namespace Curds.Cron.RangeFactories.Implementation
{
    using Cron.Abstraction;
    using Abstraction;

    internal abstract class BaseRangeFactory<TFieldDefinition> : ICronRangeFactory<TFieldDefinition>
        where TFieldDefinition : ICronFieldDefinition
    {
        private IRangeFactoryChain<TFieldDefinition> RangeFactoryChain { get; }

        public BaseRangeFactory(IRangeFactoryChain<TFieldDefinition> rangeFactoryChain)
        {
            RangeFactoryChain = rangeFactoryChain;
        }

        public ICronRange Parse(string range)
        {
            IRangeFactoryLink currentLink = RangeFactoryChain.BuildChain();
            ICronRange parsedRange = currentLink.HandleParse(range);
            while (parsedRange == null)
            {
                IRangeFactoryLink successor = currentLink.Successor;
                if (successor == null)
                    throw new FormatException($"Failed to parse {range}");

                currentLink = successor;
                parsedRange = currentLink.HandleParse(range);
            }

            return parsedRange;
        }
    }
}

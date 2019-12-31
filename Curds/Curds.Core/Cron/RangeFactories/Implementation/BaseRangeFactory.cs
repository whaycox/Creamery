using System;

namespace Curds.Cron.RangeFactories.Implementation
{
    using Cron.Abstraction;

    internal abstract class BaseRangeFactory : ICronRangeFactory
    {
        private ICronRangeLinkFactory RangeLinkFactory { get; }

        public BaseRangeFactory(ICronRangeLinkFactory rangeLinkFactory)
        {
            RangeLinkFactory = rangeLinkFactory;
        }

        public ICronRange Parse(string range)
        {
            ICronRangeLink currentLink = RangeLinkFactory.StartOfChain;
            ICronRange parsedRange = currentLink.HandleParse(range);
            while (parsedRange == null)
            {
                ICronRangeLink successor = currentLink.Successor;
                if (successor == null)
                    throw new FormatException($"Failed to parse {range}");

                currentLink = successor;
                parsedRange = currentLink.HandleParse(range);
            }

            return parsedRange;
        }
    }
}

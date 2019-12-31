﻿using System;

namespace Curds.Cron.RangeLinks.Implementation
{
    using Cron.Abstraction;

    internal abstract class BaseRangeLink<TFieldDefinition> : ICronRangeLink
        where TFieldDefinition : ICronFieldDefinition
    {
        protected TFieldDefinition FieldDefinition { get; }

        public ICronRangeLink Successor { get; }

        public BaseRangeLink(
            TFieldDefinition fieldDefinition,
            ICronRangeLink successor)
        {
            FieldDefinition = fieldDefinition;
            Successor = successor;
        }

        protected bool IsValid(int parsedValue) => FieldDefinition.AbsoluteMin <= parsedValue && parsedValue <= FieldDefinition.AbsoluteMax;

        public abstract ICronRange HandleParse(string range);
    }
}

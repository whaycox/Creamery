using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Time.Abstraction
{
    public interface IDateTimeOffsetBuilder
    {
        void ApplyYear(int year);
        void ApplyMonth(int month);
        void ApplyDay(int day);

        DateTimeOffset Build();
    }
}

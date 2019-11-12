using System;

namespace Gouda.Template
{
    public abstract class TimeTemplate
    {
        protected const int TestYear = 2001;
        protected const int TestMonth = 8;
        protected const int TestDay = 15;

        protected DateTimeOffset TestTime = new DateTimeOffset()
           .WithYear(TestYear)
           .WithMonth(TestMonth)
           .WithDay(TestDay)
           .Build();
    }
}

using System.Collections.Generic;

namespace Curds.Cron.Mock
{
    public class ICron : Abstraction.ICron
    {
        public List<string> ExpressionsBuilt = new List<string>();
        public Abstraction.ICronExpression Build(string cronExpression)
        {
            ExpressionsBuilt.Add(cronExpression);
            return new ICronExpression();
        }
    }
}

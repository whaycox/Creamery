using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Cron
{
    public interface ICron
    {
        ICronObject Build(string cronString);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Cron
{
    public interface IProvider
    {
        ICronObject Build(string cronString);
    }
}

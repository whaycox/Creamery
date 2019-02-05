using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.EventArgs;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;

namespace Gouda.Application.Communication
{
    public interface INotifier
    {
        Curds.Application.DateTimes.IProvider Time { get; set; }
        Persistence.IProvider Persistence { get; set; }

        void NotifyUsers(object sender, StatusChanged eventArgs);
    }
}

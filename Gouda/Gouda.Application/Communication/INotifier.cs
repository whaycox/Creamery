using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;

namespace Gouda.Application.Communication
{
    public interface INotifier
    {
        Curds.Application.DateTimes.IDateTime Time { get; set; }
        Persistence.IPersistence Persistence { get; set; }

        void NotifyUsers(StatusChange changeInformation);
    }
}

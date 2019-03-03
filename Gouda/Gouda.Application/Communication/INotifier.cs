using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Curds.Application.DateTimes;
using System.Threading.Tasks;

namespace Gouda.Application.Communication
{
    using Persistence;

    public interface INotifier
    {
        IDateTime Time { get; }
        IPersistence Persistence { get; }

        Task NotifyUsers(StatusChange changeInformation);
    }
}

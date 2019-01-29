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
        Curds.Application.Cron.IProvider Cron { get; set; }
        Curds.Application.DateTimes.IProvider Time { get; set; }

        void AddContact(Contact contact);
        void AddRegistration(UserRegistration registration);
        void NotifyUsers(object sender, StatusChanged eventArgs);
    }
}

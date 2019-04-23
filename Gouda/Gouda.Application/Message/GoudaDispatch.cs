using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.Dispatch;
using Curds.Application.Security.ViewModel;
using Curds.Application.Security.Command;

namespace Gouda.Application.Message
{
    public abstract class GoudaDispatch : SecureDispatch<GoudaApplication>
    {
        public GoudaDispatch(GoudaApplication application)
            : base(application, application.Security)
        { }
    }
}

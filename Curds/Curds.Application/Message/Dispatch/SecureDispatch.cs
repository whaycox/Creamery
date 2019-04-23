using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Curds.Application.Message.Dispatch
{
    using Domain.Security;

    using Security;

    public abstract class SecureDispatch<T> : BaseDispatch<T>
        where T : CurdsApplication
    {
        private ISecurity Security { get; }

        public SecureDispatch(T application, ISecurity security)
            : base(application)
        {
            Security = security;
        }

        public Task<Authentication> Login(Security.ViewModel.Login viewModel) => 
            Security.Login(new Security.Command.Login(viewModel));
        public Task<Authentication> CreateInitialUser(Security.ViewModel.CreateInitialUser viewModel) => 
            Security.CreateInitialUser(new Security.Command.CreateInitialUser(viewModel));
    }
}

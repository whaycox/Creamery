using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Application.Communication;

namespace Gouda.Infrastructure.Communication
{
    public abstract class BaseContactAdapter<T> : IContactAdapter where T : Contact
    {
        public void Notify(Contact contact) => Notify(contact as T);
        protected abstract void Notify(T contact);
    }
}

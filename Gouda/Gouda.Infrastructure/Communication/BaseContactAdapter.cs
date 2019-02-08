using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Application.Communication;

namespace Gouda.Infrastructure.Communication
{
    public abstract class BaseContactAdapter : IContactAdapter
    {
        public abstract void Notify(Contact contact);
    }

    public abstract class BaseContactAdapter<T> : BaseContactAdapter where T : Contact
    {
        public override void Notify(Contact contact) => Notify(contact as T);
        protected abstract void Notify(T contact);
    }
}

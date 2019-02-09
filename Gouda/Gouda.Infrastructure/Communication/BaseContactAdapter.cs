using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Application.Communication;
using Gouda.Domain.Check;

namespace Gouda.Infrastructure.Communication
{
    public abstract class BaseContactAdapter : IContactAdapter
    {
        public abstract void Notify(Contact contact, StatusChange changeInformation);
    }

    public abstract class BaseContactAdapter<T> : BaseContactAdapter where T : Contact
    {
        public override void Notify(Contact contact, StatusChange changeInformation) => Notify(contact as T, changeInformation);
        protected abstract void Notify(T contact, StatusChange changeInformation);
    }
}

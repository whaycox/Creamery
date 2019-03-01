using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Application.Communication;
using Gouda.Domain.Check;
using Gouda.Domain.Enumerations;

namespace Gouda.Infrastructure.Communication
{
    public abstract class BaseContactAdapter : IContactAdapter
    {
        public abstract ContactType HandledType { get; }

        public abstract void Notify(Contact contact, StatusChange changeInformation);
    }
}

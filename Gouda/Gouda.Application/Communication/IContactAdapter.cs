using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;
using Gouda.Domain.Check;

namespace Gouda.Application.Communication
{
    public interface IContactAdapter
    {
        void Notify(Contact contact, StatusChange changeInformation);
    }
}

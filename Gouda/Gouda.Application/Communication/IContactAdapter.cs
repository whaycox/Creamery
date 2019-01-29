using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Communication;

namespace Gouda.Application.Communication
{
    public interface IContactAdapter
    {
        void Notify(Contact contact);
    }
}

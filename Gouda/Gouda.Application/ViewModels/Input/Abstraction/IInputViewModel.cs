using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Input.Abstraction
{
    using Application.Abstraction;
    using DeferredValues.Domain;
    using Common.Abstraction;

    public interface IInputViewModel : IFieldViewModel
    {
        bool Editable { get; set; }
    }
}

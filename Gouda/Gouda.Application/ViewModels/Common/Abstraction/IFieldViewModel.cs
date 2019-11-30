using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Common.Abstraction
{
    using Application.Abstraction;
    using DeferredValues.Domain;

    public interface IFieldViewModel : ICommonViewModel
    {
        LabelDeferredKey Label { get; set; }
    }
}

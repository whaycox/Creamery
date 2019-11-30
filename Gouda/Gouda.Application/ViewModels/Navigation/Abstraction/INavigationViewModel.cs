using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Navigation.Abstraction
{
    using Application.Abstraction;
    using DeferredValues.Domain;
    using Glyph.Abstraction;

    public interface INavigationViewModel : IViewModel
    {
        LabelDeferredKey Label { get; set; }
    }
}

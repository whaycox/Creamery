using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.ViewModels.Abstraction
{
    using Domain;
    using Application.Abstraction;
    using Abstraction;

    public interface IWebAppViewModelWrapper
    {
        IWebAppViewModel Wrap(IViewModel viewModel);
    }
}

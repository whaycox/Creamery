using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.ViewModels.Abstraction
{
    using Domain;
    using Application.Abstraction;

    public interface IWebAppViewModelWrapper
    {
        WebAppViewModel Wrap(IViewModel viewModel);
    }
}

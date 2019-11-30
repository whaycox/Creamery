using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Common.Domain
{
    using Application.Abstraction;

    public class CompositeFieldViewModel : BaseFieldViewModel
    {
        public override string ViewName => nameof(CompositeFieldViewModel);

        public IViewModel ViewModel { get; protected set; }
    }

    public class CompositeFieldViewModel<TViewModel> : CompositeFieldViewModel
        where TViewModel : class, IViewModel, new()
    {
        public TViewModel Value
        {
            get => ViewModel as TViewModel;
            set => ViewModel = value;
        }

        public CompositeFieldViewModel()
        {
            Value = new TViewModel();
        }
    }
}

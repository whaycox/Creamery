using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Input.Domain
{
    using Abstraction;

    public class DropdownViewModel : BaseInputViewModel, IFormInput, IEditableInput
    {
        public override string ViewName => nameof(DropdownViewModel);

        public string Name { get; set; }
        public bool Required { get; set; }
        public bool Editable { get; set; }
        public int CurrentIndex { get; set; }
        public List<DropdownItemViewModel> Items { get; set; } = new List<DropdownItemViewModel>();
    }
}

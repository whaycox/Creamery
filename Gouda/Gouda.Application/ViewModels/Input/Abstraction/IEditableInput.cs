using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Input.Abstraction
{
    public interface IEditableInput
    {
        bool Editable { get; set; }
    }
}

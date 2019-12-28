using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Input.Abstraction
{
    public interface IFormInput : IInputViewModel
    {
        string Name { get; set; }
        bool Required { get; set; }
    }
}

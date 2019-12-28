using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Input.Domain
{
    using Abstraction;

    public class TextInputViewModel : BaseInputViewModel, IEditableInput, IFormInput
    {
        public override string ViewName => nameof(TextInputViewModel);
        public string TypeName { get; set; } = string.Empty;

        public string Name { get; set; }
        public bool Required { get; set; }
        public bool Editable { get; set; }
        public string ValuePattern { get; set; }
        public string Value { get; set; }

        public const string IPAddress = nameof(IPAddress);    
        public const string IPAddressValuePattern = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Input.Domain
{
    using Abstraction;

    public class TextInputViewModel : BaseInputViewModel
    {
        public override string ViewName => nameof(TextInputViewModel);
        public string TypeName { get; private set; } = string.Empty;

        public string Name { get; set; }
        public bool Required { get; set; }
        public string ValuePattern { get; private set; }
        public string Value { get; set; }

        public static TextInputViewModel IPAddress => new TextInputViewModel { TypeName = nameof(IPAddress), ValuePattern = IPAddressValuePattern };        
        private const string IPAddressValuePattern = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
    }
}

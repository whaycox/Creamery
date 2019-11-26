using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Application.ViewModels.Inputs.Domain
{
    public class IPAddressTextInputViewModel : TextInputViewModel
    {
        private const string IPAddressValuePattern = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        public override string TypeName => "ipAddress";

        public IPAddressTextInputViewModel()
        {
            ValuePattern = IPAddressValuePattern;
        }
    }
}

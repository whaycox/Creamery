using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message.ViewModel
{
    public class Error : BaseViewModel
    {
        public int ID { get; set; }
        public string Message { get; set; }
    }
}

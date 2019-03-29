using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Curds.Domain.Application.Message
{
    public class MockViewModel : BaseViewModel
    {
        public bool Boolean { get; set; }
        public int Integer { get; set; }
        public string String { get; set; }
    }
}

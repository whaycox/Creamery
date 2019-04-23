using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.ViewModel;
using Queso.Domain.Enumerations;

namespace Queso.Application.Message.ViewModels
{
    public class Character : BaseViewModel
    {
        public string Name { get; set; }
        public Class Class { get; set; }
        public bool Alive { get; set; }
    }
}

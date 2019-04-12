using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.Command;

namespace Curds.Domain.Application.Message.Command
{
    public class MockCommand : BaseCommand
    {
        public string Message { get; }

        public MockCommand(string message)
        {
            Message = message;
        }
    }
}

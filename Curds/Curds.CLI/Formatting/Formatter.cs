using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;
using Curds.Application;

namespace Curds.CLI.Formatting
{
    public class Formatter
    {
        
        public FormattedText Format<T>(BaseMessageDefinition<T> messageDefinition) where T : CurdsApplication
        {
            throw new NotImplementedException();
        }
    }
}

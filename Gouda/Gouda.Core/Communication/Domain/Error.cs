using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gouda.Communication.Domain
{
    using Abstraction;
    using Enumerations;

    public class Error : ICommunicableObject
    {
        public string ExceptionText { get; }

        public CommunicableType Type => CommunicableType.Error;

        public Error(Exception exception)
        {
            ExceptionText = exception.ToString();
        }

        public Error(Parser parser)
        {
            ExceptionText = parser.ParseString();
        }

        public Stream ObjectStream() => this
            .BuildBuffer()
            .Append(ExceptionText)
            .ConvertToStream();
    }
}

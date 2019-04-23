using Curds.Domain.Security;
using System;
using System.Threading.Tasks;

namespace Curds.Application.Security.Command
{
    using Message.Command;

    public class ReAuthenticate : BaseCommand
    {
        public string Series { get; }
        public string Token { get; }

        public ReAuthenticate(string series, string token)
        {
            Series = series;
            Token = token;
        }
    }
}

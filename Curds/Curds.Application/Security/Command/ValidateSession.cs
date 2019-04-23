namespace Curds.Application.Security.Command
{
    using Message.Command;

    public class ValidateSession : BaseCommand
    {
        public string Identifier { get; }

        public ValidateSession(string identifier)
        {
            Identifier = identifier;
        }
    }
}

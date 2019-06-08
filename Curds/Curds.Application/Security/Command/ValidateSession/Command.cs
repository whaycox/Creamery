namespace Curds.Application.Security.Command.ValidateSession
{
    using Application.Command.Domain;

    public class Command : BaseCommand
    {
        public string Identifier { get; }

        public Command(string identifier)
        {
            Identifier = identifier;
        }
    }
}

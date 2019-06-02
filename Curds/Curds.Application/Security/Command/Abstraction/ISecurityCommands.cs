namespace Curds.Application.Security.Command.Abstraction
{
    public interface ISecurityCommands
    {
        CreateInitialUser.Definition CreateInitialUser { get; }
        Login.Definition Login { get; }
        LogoutSeries.Definition LogoutSeries { get; }
        ValidateSession.Definition ValidateSession { get; }
    }
}

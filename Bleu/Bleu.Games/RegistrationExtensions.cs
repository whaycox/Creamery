using Microsoft.Extensions.DependencyInjection;

namespace Bleu.Games
{
    using TicTacToe.Abstraction;
    using TicTacToe.Implementation;

    public static class RegistrationExtensions
    {
        public static IServiceCollection AddBleuGames(this IServiceCollection services) => services
            .AddSingleton<ITicTacToeFactory, TicTacToeFactory>();
    }
}

namespace Bleu.Games.TicTacToe.Implementation
{
    using Abstraction;
    using Domain;

    internal class TicTacToeFactory : ITicTacToeFactory
    {
        public ITicTacToeBrain Brain() => new RandomTicTacToeBrain();

        public ITicTacToeGame Game(ITicTacToeBrain brain, TicTacToeGameOptions options) => new TicTacToeGame(brain, options);
    }
}

namespace Bleu.Games.TicTacToe.Abstraction
{
    using Domain;

    public interface ITicTacToeFactory
    {
        ITicTacToeBrain Brain();
        ITicTacToeGame Game(ITicTacToeBrain brain, TicTacToeGameOptions options);
    }
}

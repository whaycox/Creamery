namespace Bleu.Games.TicTacToe.Abstraction
{
    using Domain;

    public interface ITicTacToeGame
    {
        bool Active { get; }
        PlayerType? Winner { get; }
        PlayerType?[] Board { get; }

        void Select(int index);
    }
}

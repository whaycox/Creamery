using System;
using System.Collections.Generic;
using System.Text;

namespace Bleu.Games.TicTacToe.Abstraction
{
    using Domain;

    public interface ITicTacToeBrain
    {
        int Act(PlayerType?[] board);
    }
}

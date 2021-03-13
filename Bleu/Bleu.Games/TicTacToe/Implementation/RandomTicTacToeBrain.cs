using System;
using System.Collections.Generic;

namespace Bleu.Games.TicTacToe.Implementation
{
    using Abstraction;
    using Domain;

    internal class RandomTicTacToeBrain : ITicTacToeBrain
    {
        private Random Random { get; } = new Random();

        public int Act(PlayerType?[] board)
        {
            List<int> possibilities = new List<int>();
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == null)
                    possibilities.Add(i);
            }
            int chosenIndex = Random.Next(possibilities.Count);
            return possibilities[chosenIndex];
        }
    }
}

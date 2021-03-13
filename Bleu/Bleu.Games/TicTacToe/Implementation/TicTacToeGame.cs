using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bleu.Games.TicTacToe.Implementation
{
    using Abstraction;
    using Domain;

    internal class TicTacToeGame : ITicTacToeGame
    {
        private ITicTacToeBrain Brain { get; }

        private PlayerType CurrentPlayer { get; set; }
        private PlayerType NextPlayer => CurrentPlayer == PlayerType.CPU ? PlayerType.Player : PlayerType.CPU;

        public PlayerType?[] Board { get; } = new PlayerType?[9];
        public bool Active { get; private set; } = true;
        public PlayerType? Winner { get; private set; }

        public TicTacToeGame(
            ITicTacToeBrain brain,
            TicTacToeGameOptions options)
        {
            Brain = brain;
            CurrentPlayer = StartingPlayer(options.StartingPlayer);
            if (CurrentPlayer == PlayerType.CPU)
                TakeCpuTurn();
        }
        private PlayerType StartingPlayer(PlayerType startingPlayer)
        {
            if (startingPlayer == PlayerType.Random)
            {
                Random random = new Random();
                return (PlayerType)random.Next(2);
            }
            else
                return startingPlayer;
        }

        public void Select(int index) => SelectForPlayer(index, false);
        private void SelectForPlayer(int index, bool cpu)
        {
            if (Board[index] != null)
                throw new ArgumentException($"{index} is already selected");
            Board[index] = cpu ? PlayerType.CPU : PlayerType.Player;
            EndTurn();
        }
        private void EndTurn()
        {
            if (GameOver(out PlayerType winner))
            {
                Active = false;
                Winner = winner;
            }
            else
            {
                CurrentPlayer = NextPlayer;
                if (CurrentPlayer == PlayerType.CPU)
                    TakeCpuTurn();
            }
        }
        private void TakeCpuTurn()
        {
            int action = Brain.Act(Board);
            SelectForPlayer(action, true);
        }
        private bool GameOver(out PlayerType winner)
        {
            if (TopLeftDiagonalHasWinner())
            {
                winner = Board[0].Value;
                return true;
            }
            if (TopRightDiagonalHasWinner())
            {
                winner = Board[2].Value;
                return true;
            }
            for (int i = 0; i < 3; i++)
            {
                if (RowHasWinner(i))
                {
                    winner = Board[i * 3].Value;
                    return true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (ColumnHasWinner(i))
                {
                    winner = Board[i].Value;
                    return true;
                }
            }
            winner = PlayerType.Random;
            return false;
        }
        private bool RowHasWinner(int row)
        {
            int startCell = row * 3;
            return Board[startCell] != null &&
            (Board[startCell] == Board[startCell + 1]) &&
            (Board[startCell] == Board[startCell + 2]);
        }
        private bool ColumnHasWinner(int column)
        {
            int startCell = column;
            return Board[startCell] != null &&
            (Board[startCell] == Board[startCell + 3]) &&
            (Board[startCell] == Board[startCell + 6]);
        }
        private bool TopLeftDiagonalHasWinner() => Board[0] != null &&
            (Board[0] == Board[4]) &&
            (Board[0] == Board[8]);
        private bool TopRightDiagonalHasWinner() => Board[2] != null &&
            (Board[2] == Board[4]) &&
            (Board[2] == Board[6]);
    }
}

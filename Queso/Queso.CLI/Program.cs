using System;
using Queso.Application.Character;
using Queso.Infrastructure.Character;

namespace Queso.CLI
{
    class Program
    {
        private static ICharacter Character = new CharacterProvider();

        static void Main(string[] args)
        {
            Character.Resurrect(args[0]);
        }
    }
}

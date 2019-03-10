using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Queso.Domain.TestCharacters
{
    public static class Files
    {
        private const string Extension = "d2s";
        private static string FormatName(string characterClass) => Path.Combine(TestingDirectory, $"{characterClass}.{Extension}");

        private static string TestingDirectory => Path.Combine(Environment.CurrentDirectory, nameof(TestCharacters));

        public static Characters StartingCharacters { get; } = new Characters();

        public class Characters
        {
            public string Amazon => FormatName(nameof(Amazon));
            public string Sorceress => FormatName(nameof(Sorceress));
            public string Necromancer => FormatName(nameof(Necromancer));
            public string Paladin => FormatName(nameof(Paladin));
            public string Barbarian => FormatName(nameof(Barbarian));
            public string Druid => FormatName(nameof(Druid));
            public string Assassin => FormatName(nameof(Assassin));
        }
    }
}

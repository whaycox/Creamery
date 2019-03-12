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
        public static string InvalidChecksum => FormatName(nameof(InvalidChecksum));
        public static string Dead => FormatName(nameof(Dead));

        public class Characters
        {
            public int AmazonSize = 981;
            public int SorceressSize = 958;
            public int NecromancerSize = 958;
            public int PaladinSize = 980;
            public int BarbarianSize = 980;
            public int DruidSize = 980;
            public int AssassinSize = 980;

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

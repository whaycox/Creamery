using System;
using System.Collections.Generic;
using System.Text;

namespace Queso.Domain
{
    using Enumerations;

    public class Character
    {
        public static int Signature = -1437226411;

        public FileData File { get; }
        public ConfigurationData Config { get; }
        public MercData Mercenary { get; }
        public QuestData Quest { get; }
        public WaypointData Waypoint { get; }
        public NPCData NPC { get; }
        public StatsData Stats { get; }

        public int ActiveWeapon { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public byte Progression { get; set; }
        public short Unknownx38 { get; set; }
        public Class Class { get; set; }
        public short Unknownx41 { get; set; }
        public byte Level { get; set; }
        public int Unknownx44 { get; set; }
        public int Time { get; set; }
        public int Unknownx52 { get; set; }

        public byte NormalDifficulty { get; set; }
        public byte NightmareDifficulty { get; set; }
        public byte HellDifficulty { get; set; }
        public int Map { get; set; }

        public byte[] Unknownx191 { get; set; }

        public bool Hardcore => (Status & Status.Hardcore) == Status.Hardcore;
        public bool Alive => (Status & Status.Died) == Status.None;

        public Character()
        {
            File = new FileData();
            Config = new ConfigurationData();
            Mercenary = new MercData();
            Quest = new QuestData();
            Waypoint = new WaypointData();
            NPC = new NPCData();
            Stats = new StatsData();
        }

        public override string ToString() => $"{Name} ({Class})";

        public class FileData
        {
            public int Signature { get; set; }
            public VersionID VersionID { get; set; }
            public int Size { get; set; }
            public int Checksum { get; set; }
        }

        public class ConfigurationData
        {
            public int Hotkey1 { get; set; }
            public int Hotkey2 { get; set; }
            public int Hotkey3 { get; set; }
            public int Hotkey4 { get; set; }
            public int Hotkey5 { get; set; }
            public int Hotkey6 { get; set; }
            public int Hotkey7 { get; set; }
            public int Hotkey8 { get; set; }
            public int Hotkey9 { get; set; }
            public int Hotkey10 { get; set; }
            public int Hotkey11 { get; set; }
            public int Hotkey12 { get; set; }
            public int Hotkey13 { get; set; }
            public int Hotkey14 { get; set; }
            public int Hotkey15 { get; set; }
            public int Hotkey16 { get; set; }
            public int LeftMouse { get; set; }
            public int RightMouse { get; set; }
            public int LeftMouseAlternate { get; set; }
            public int RightMouseAlternate { get; set; }
            public int Appearance1 { get; set; }
            public int Appearance2 { get; set; }
            public int Appearance3 { get; set; }
            public int Appearance4 { get; set; }
            public int Appearance5 { get; set; }
            public int Appearance6 { get; set; }
            public int Appearance7 { get; set; }
            public int Appearance8 { get; set; }
        }

        public class MercData
        {
            public byte Unknownx175 { get; set; }
            public byte Unknownx176 { get; set; }
            public byte Unknownx177 { get; set; }
            public byte Unknownx178 { get; set; }
            public int Seed { get; set; }
            public short Name { get; set; }
            public short Type { get; set; }
            public int Experience { get; set; }
        }

        public class QuestData
        {
            public byte[] Raw { get; set; }
        }

        public class WaypointData
        {
            public byte[] Raw { get; set; }
        }

        public class NPCData
        {
            public byte[] Raw { get; set; }
        }

        public class StatsData
        {
            public const short Signature = 26215;

            public byte[] Raw { get; set; }

            public int Level { get; set; }

            public int GoldOnPerson { get; set; }
            public int GoldInStash { get; set; }

            public int Strength { get; set; }
            public int Dexterity { get; set; }
            public int Vitality { get; set; }
            public int Energy { get; set; }

            public decimal CurrentLife { get; set; }
            public decimal BaseLife { get; set; }
            public decimal CurrentMana { get; set; }
            public decimal BaseMana { get; set; }
            public decimal CurrentStamina { get; set; }
            public decimal BaseStamina { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Queso.Domain.Enumerations
{
    public enum Statistics
    {
        Strength = 0,
        Energy = 1,
        Dexterity = 2,
        Vitality = 3,
        StatsRemaining = 4,
        SkillsRemaining = 5,
        CurrentLife = 6,
        BaseLife = 7,
        CurrentMana = 8,
        BaseMana = 9,
        CurrentStamina = 10,
        BaseStamina = 11,
        Level = 12,
        Experience = 13,
        GoldInInventory = 14,
        GoldInStash = 15,
        End = 0x1FF,
    }
}

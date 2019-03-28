using Queso.Application.Character;
using Queso.Domain.Enumerations;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

/*
 * https://github.com/krisives/d2s-format
 * https://user.xmission.com/~trevin/DiabloIIv1.09_File_Format.shtml
 * helpful for crazy variable width encoding:
 *  https://sourceforge.net/p/gomule/git/ci/master/tree/GoMule/src/randall/d2files/D2FileReader.java
 */

namespace Queso.Infrastructure.Character
{
    using Reading;

    public class CharacterProvider : ICharacter
    {
        private const int IntLengthInBytes = 4;
        private const int ShortLengthInBytes = 2;
        private const int NameLengthInBytes = 16;

        private const int ChecksumStartIndex = 12;
        private const int ChecksumEndIndex = 15;

        private const int StatusIndex = 36;

        private static Encoding TextEncoding => Encoding.ASCII;
        private static Regex NameCleaner = new Regex("^([^\u0000]+)", RegexOptions.Compiled);

        public Domain.Character Load(string filePath) => ParseCharacter(ReadCharacterFile(filePath));

        public void Resurrect(string filePath)
        {
            byte[] character = ReadCharacterFile(filePath);
            Status status = (Status)character[StatusIndex];
            if ((status & Status.Hardcore) != Status.Hardcore)
                throw new InvalidOperationException("Can only resurrect hardcore characters");
            if ((status & Status.Died) != Status.Died)
                throw new InvalidOperationException("Cannot resurrect a character that is alive");
            status &= ~Status.Died;
            character[StatusIndex] = (byte)status;
            int newChecksum = ComputeChecksum(character);
            byte[] newChecksumBytes = BitConverter.GetBytes(newChecksum);
            for (int i = 0; i < newChecksumBytes.Length; i++)
                character[ChecksumStartIndex + i] = newChecksumBytes[i];
            File.WriteAllBytes(filePath, character);
        }

        private byte[] ReadCharacterFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        private Domain.Character ParseCharacter(byte[] rawFile)
        {
            int index = 0;
            Domain.Character toReturn = new Domain.Character();
            toReturn = ParseFileInformation(toReturn, rawFile, ref index);
            toReturn = ParseIdentifyingInformation(toReturn, rawFile, ref index);
            toReturn = ParseConfigurationData(toReturn, rawFile, ref index);
            toReturn = ParseDifficultyData(toReturn, rawFile, ref index);
            toReturn = ParseMercenaryData(toReturn, rawFile, ref index);
            toReturn = ParseUnknowns(toReturn, rawFile, ref index);
            toReturn = ParseStatsData(toReturn, rawFile, ref index);

            return toReturn;
        }
        private short ParseShort(byte[] rawFile, ref int index)
        {
            short parsed = BitConverter.ToInt16(rawFile, index);
            index += ShortLengthInBytes;
            return parsed;
        }
        private int ParseInt(byte[] rawFile, ref int index)
        {
            int parsed = BitConverter.ToInt32(rawFile, index);
            index += IntLengthInBytes;
            return parsed;
        }
        private string ParseString(byte[] rawFile, int count, ref int index)
        {
            string parsed = TextEncoding.GetString(rawFile, index, count);
            index += count;
            return NameCleaner.Match(parsed).Groups[1].Value;
        }
        private byte[] ParseRawBytes(byte[] rawFile, int count, ref int index)
        {
            byte[] parsed = rawFile.Skip(index).Take(count).ToArray();
            index += count;
            return parsed;
        }

        private Domain.Character ParseFileInformation(Domain.Character character, byte[] rawFile, ref int index)
        {
            character.File.Signature = ParseInt(rawFile, ref index);
            character.File.VersionID = (VersionID)ParseInt(rawFile, ref index);
            character.File.Size = ParseInt(rawFile, ref index);

            int computedChecksum = ComputeChecksum(rawFile);
            character.File.Checksum = ParseInt(rawFile, ref index);
            if (computedChecksum != character.File.Checksum)
                throw new FormatException("Invalid checksum");

            return character;
        }
        private int ComputeChecksum(byte[] rawFile)
        {
            int sum = 0;
            for (int i = 0; i < rawFile.Length; i++)
            {
                int read = ChecksumByte(rawFile, i);
                if (sum < 0)
                    read++;
                sum = read + (sum * 2);
            }
            return sum;
        }
        private int ChecksumByte(byte[] rawFile, int index)
        {
            if (index >= ChecksumStartIndex && index <= ChecksumEndIndex)
                return 0x00;
            else
                return rawFile[index];
        }

        private Domain.Character ParseIdentifyingInformation(Domain.Character character, byte[] rawFile, ref int index)
        {
            character.ActiveWeapon = ParseInt(rawFile, ref index);
            character.Name = ParseString(rawFile, NameLengthInBytes, ref index);
            character.Status = (Status)rawFile[index++];
            character.Progression = rawFile[index++];
            character.Unknownx38 = ParseShort(rawFile, ref index);
            character.Class = (Class)rawFile[index++];
            character.Unknownx41 = ParseShort(rawFile, ref index);
            character.Level = rawFile[index++];
            character.Unknownx44 = ParseInt(rawFile, ref index);
            character.Time = ParseInt(rawFile, ref index);
            character.Unknownx52 = ParseInt(rawFile, ref index);

            return character;
        }

        private Domain.Character ParseConfigurationData(Domain.Character character, byte[] rawFile, ref int index)
        {
            character.Config.Hotkey1 = ParseInt(rawFile, ref index);
            character.Config.Hotkey2 = ParseInt(rawFile, ref index);
            character.Config.Hotkey3 = ParseInt(rawFile, ref index);
            character.Config.Hotkey4 = ParseInt(rawFile, ref index);
            character.Config.Hotkey5 = ParseInt(rawFile, ref index);
            character.Config.Hotkey6 = ParseInt(rawFile, ref index);
            character.Config.Hotkey7 = ParseInt(rawFile, ref index);
            character.Config.Hotkey8 = ParseInt(rawFile, ref index);
            character.Config.Hotkey9 = ParseInt(rawFile, ref index);
            character.Config.Hotkey10 = ParseInt(rawFile, ref index);
            character.Config.Hotkey11 = ParseInt(rawFile, ref index);
            character.Config.Hotkey12 = ParseInt(rawFile, ref index);
            character.Config.Hotkey13 = ParseInt(rawFile, ref index);
            character.Config.Hotkey14 = ParseInt(rawFile, ref index);
            character.Config.Hotkey15 = ParseInt(rawFile, ref index);
            character.Config.Hotkey16 = ParseInt(rawFile, ref index);
            character.Config.LeftMouse = ParseInt(rawFile, ref index);
            character.Config.RightMouse = ParseInt(rawFile, ref index);
            character.Config.LeftMouseAlternate = ParseInt(rawFile, ref index);
            character.Config.RightMouseAlternate = ParseInt(rawFile, ref index);
            character.Config.Appearance1 = ParseInt(rawFile, ref index);
            character.Config.Appearance2 = ParseInt(rawFile, ref index);
            character.Config.Appearance3 = ParseInt(rawFile, ref index);
            character.Config.Appearance4 = ParseInt(rawFile, ref index);
            character.Config.Appearance5 = ParseInt(rawFile, ref index);
            character.Config.Appearance6 = ParseInt(rawFile, ref index);
            character.Config.Appearance7 = ParseInt(rawFile, ref index);
            character.Config.Appearance8 = ParseInt(rawFile, ref index);

            return character;
        }

        private Domain.Character ParseDifficultyData(Domain.Character character, byte[] rawFile, ref int index)
        {
            character.NormalDifficulty = rawFile[index++];
            character.NightmareDifficulty = rawFile[index++];
            character.HellDifficulty = rawFile[index++];
            character.Map = ParseInt(rawFile, ref index);

            return character;
        }

        private Domain.Character ParseMercenaryData(Domain.Character character, byte[] rawFile, ref int index)
        {
            character.Mercenary.Unknownx175 = rawFile[index++];
            character.Mercenary.Unknownx176 = rawFile[index++];
            character.Mercenary.Unknownx177 = rawFile[index++];
            character.Mercenary.Unknownx178 = rawFile[index++];
            character.Mercenary.Seed = ParseInt(rawFile, ref index);
            character.Mercenary.Name = ParseShort(rawFile, ref index);
            character.Mercenary.Type = ParseShort(rawFile, ref index);
            character.Mercenary.Experience = ParseInt(rawFile, ref index);

            return character;
        }

        private Domain.Character ParseUnknowns(Domain.Character character, byte[] rawFile, ref int index)
        {
            character.Unknownx191 = ParseRawBytes(rawFile, 144, ref index);
            character.Quest.Raw = ParseRawBytes(rawFile, 298, ref index);
            character.Waypoint.Raw = ParseRawBytes(rawFile, 81, ref index);
            character.NPC.Raw = ParseRawBytes(rawFile, 51, ref index);

            return character;
        }

        private Domain.Character ParseStatsData(Domain.Character character, byte[] rawFile, ref int index)
        {
            short signature = ParseShort(rawFile, ref index);
            if (signature != Domain.Character.StatsData.Signature)
                throw new FormatException("Invalid stats signature");

            LittleEndianVariableWidth reader = new LittleEndianVariableWidth(rawFile, index);

            Statistics key = ReadNextKey(reader);
            while (key != Statistics.End)
            {
                int rawValue = reader.Read(StatWidthInBits(key));
                character = AssignStatValue(character, key, rawValue);
                key = ReadNextKey(reader);
            }
            index = reader.UnconsumedIndex;
            return character;
        }
        private Statistics ReadNextKey(LittleEndianVariableWidth reader) => (Statistics)reader.Read(StatsKeyWidthInBits);
        private const int StatsKeyWidthInBits = 9;
        private int StatWidthInBits(Statistics key)
        {
            switch (key)
            {
                case Statistics.Level:
                    return 7;
                case Statistics.SkillsRemaining:
                    return 8;
                case Statistics.Strength:
                case Statistics.Dexterity:
                case Statistics.Vitality:
                case Statistics.Energy:
                case Statistics.StatsRemaining:
                    return 10;
                case Statistics.CurrentLife:
                case Statistics.BaseLife:
                case Statistics.CurrentMana:
                case Statistics.BaseMana:
                case Statistics.CurrentStamina:
                case Statistics.BaseStamina:
                    return 21;
                case Statistics.GoldInInventory:
                case Statistics.GoldInStash:
                    return 25;
                case Statistics.Experience:
                    return 32;
                default:
                    throw new InvalidOperationException($"Unexpected Statistic: {key}");
            }
        }
        private Domain.Character AssignStatValue(Domain.Character character, Statistics key, int rawValue)
        {
            switch (key)
            {
                case Statistics.Strength:
                    character.Stats.Strength = rawValue;
                    return character;
                case Statistics.Dexterity:
                    character.Stats.Dexterity = rawValue;
                    return character;
                case Statistics.Vitality:
                    character.Stats.Vitality = rawValue;
                    return character;
                case Statistics.Energy:
                    character.Stats.Energy = rawValue;
                    return character;
                case Statistics.Level:
                    character.Stats.Level = rawValue;
                    return character;
                case Statistics.CurrentLife:
                    character.Stats.CurrentLife = rawValue / 256;
                    return character;
                case Statistics.BaseLife:
                    character.Stats.BaseLife = rawValue / 256;
                    return character;
                case Statistics.CurrentMana:
                    character.Stats.CurrentMana = rawValue / 256;
                    return character;
                case Statistics.BaseMana:
                    character.Stats.BaseMana = rawValue / 256;
                    return character;
                case Statistics.CurrentStamina:
                    character.Stats.CurrentStamina = rawValue / 256;
                    return character;
                case Statistics.BaseStamina:
                    character.Stats.BaseStamina = rawValue / 256;
                    return character;
                default:
                    throw new InvalidOperationException($"Unexpected Statistic: {key}");
            }
        }
    }
}

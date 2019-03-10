using Queso.Application.Character;
using Queso.Domain.Enumerations;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Queso.Infrastructure.Character
{
    public class CharacterProvider : ICharacter
    {
        private const int IntLengthInBytes = 4;
        private const int NameLengthInBytes = 16;

        private static Encoding TextEncoding => Encoding.ASCII;
        private static Regex NameCleaner = new Regex("^([^\u0000]+)", RegexOptions.Compiled);

        public Domain.Character Load(string filePath) => ParseCharacter(ReadCharacterFile(filePath));

        private byte[] ReadCharacterFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        //https://github.com/krisives/d2s-format
        private Domain.Character ParseCharacter(byte[] rawFile)
        {
            int index = 0;
            Domain.Character toReturn = new Domain.Character();
            toReturn = ParseFileInformation(toReturn, rawFile, ref index);
            int activeWeapon = BitConverter.ToInt32(rawFile, index += IntLengthInBytes);
            toReturn = ParseIdentifyingInformation(toReturn, rawFile, ref index);

            return toReturn;
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

        private Domain.Character ParseFileInformation(Domain.Character character, byte[] rawFile, ref int index)
        {
            character.File.Signature = ParseInt(rawFile, ref index);
            character.File.VersionID = ParseInt(rawFile, ref index);
            character.File.Size = ParseInt(rawFile, ref index);
            character.File.Checksum = ParseInt(rawFile, ref index);

            return character;
        }
        private Domain.Character ParseIdentifyingInformation(Domain.Character character, byte[] rawFile, ref int index)
        {
            character.Name = ParseString(rawFile, NameLengthInBytes, ref index);
            byte status = rawFile[index++];
            byte progression = rawFile[index++];
            index += 2; //skip unknown
            character.Class = (Class)rawFile[index++];

            return character;
        }
    }
}

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

        private const int ChecksumStartIndex = 12;
        private const int ChecksumEndIndex = 15;

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
            character.Name = ParseString(rawFile, NameLengthInBytes, ref index);
            byte status = rawFile[index++];
            byte progression = rawFile[index++];
            index += 2; //skip unknown
            character.Class = (Class)rawFile[index++];

            return character;
        }
    }
}

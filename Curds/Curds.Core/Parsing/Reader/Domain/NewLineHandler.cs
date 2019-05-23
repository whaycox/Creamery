using System;
using System.Runtime.InteropServices;

namespace Curds.Parsing.Reader.Domain
{
    public abstract class NewLineHandler
    {
        public const char CR = (char)0x0D;
        public const char LF = (char)0x0A;

        public static NewLineHandler CurrentEnvironment
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return Windows;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    return Linux;
                throw new InvalidOperationException($"Unrecognized environment {RuntimeInformation.OSDescription}");
            }
        }
        public static NewLineHandler Windows => new WindowsNewLineHandler();
        public static NewLineHandler Linux => new LinuxNewLineHandler();

        public abstract int NewLineLength { get; }
        public abstract bool IsNewLine(char currentChar, char? nextChar);

        private class WindowsNewLineHandler : NewLineHandler
        {
            public override int NewLineLength => 2;
            public override bool IsNewLine(char currentChar, char? nextChar)
            {
                if (!nextChar.HasValue)
                    return false;
                return currentChar == CR && nextChar.Value == LF;
            }
        }

        private class LinuxNewLineHandler : NewLineHandler
        {
            public override int NewLineLength => 1;
            public override bool IsNewLine(char currentChar, char? nextChar) => currentChar == LF;
        }
    }
}

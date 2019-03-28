using System;

namespace Queso.Infrastructure.Reading
{
    public class LittleEndianVariableWidth
    {
        private const int NegativeIndex = -1;

        private int ByteIndex { get; set; }
        private int BitIndex { get; set; }

        private byte[] Buffer { get; }
        private int LastIndex => Buffer.Length - 1;

        private bool BitNBitsAway(int numberOfBits) => (ByteNBitsAway(numberOfBits) & (1 << (BitShiftInThatByte(numberOfBits)))) != 0;
        private byte ByteNBitsAway(int numberOfBits) => Buffer[ByteIndex + BytesAway(numberOfBits)];
        private int BytesAway(int numberOfBits) => BitsAwayFromStartOfCurrentByte(numberOfBits) / 8;
        private int BitShiftInThatByte(int numberOfBits) => BitsAwayFromStartOfCurrentByte(numberOfBits) % 8;
        private int BitsAwayFromStartOfCurrentByte(int numberOfBits) => BitIndex + numberOfBits;

        private int FreshBytesRemaining => LastIndex - ByteIndex;
        private int BitsLeftInByte => 8 - BitIndex;
        private int BitsRemaining => (FreshBytesRemaining * 8) + BitsLeftInByte;

        public int UnconsumedIndex
        {
            get
            {
                if (NoMoreBytesToConsume && HasConsumedAnyOfCurrentByte)
                    return NegativeIndex;
                else if (HasConsumedAnyOfCurrentByte)
                    return ByteIndex + 1;
                else
                    return ByteIndex;
            }
        }
        public bool HasConsumedAnyOfCurrentByte => BitIndex > 0;
        public bool NoMoreBytesToConsume => FreshBytesRemaining == 0;

        public LittleEndianVariableWidth(byte[] buffer)
            : this(buffer, default(int))
        { }

        public LittleEndianVariableWidth(byte[] buffer, int offset)
        {
            if (buffer == null || buffer.Length == 0)
                throw new ArgumentNullException(nameof(buffer));
            Buffer = buffer;

            if (offset < 0 || offset >= Buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
            ByteIndex = offset;
        }

        public int Read(int bits)
        {
            if (bits <= 0)
                throw new ArgumentOutOfRangeException(nameof(bits));
            if (bits > BitsRemaining)
                throw new InvalidOperationException("Cannot request more bits than are left");

            int bitsRead = 0;
            int toReturn = 0;
            while (bitsRead < bits)
            {
                toReturn = toReturn << 1;
                if (BitNBitsAway(bits - 1 - bitsRead))
                    toReturn++;
                bitsRead++;
            }
            ConsumeBits(bits);
            return toReturn;
        }
        private void ConsumeBits(int numberToConsume)
        {
            for (int i = 0; i < numberToConsume; i++)
                ConsumeBit();
        }
        private void ConsumeBit()
        {
            BitIndex++;
            if (BitIndex == 8)
            {
                BitIndex = 0;
                ByteIndex++;
            }
        }
    }
}

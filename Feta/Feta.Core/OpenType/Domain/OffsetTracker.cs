namespace Feta.OpenType.Domain
{
    public class OffsetTracker
    {
        private FontReader Reader { get; }
        private int StartingOffset { get; }

        public int RelativeOffset => Reader.CurrentOffset - StartingOffset;

        public OffsetTracker(FontReader reader)
        {
            Reader = reader;
            StartingOffset = reader.CurrentOffset;
        }
    }
}

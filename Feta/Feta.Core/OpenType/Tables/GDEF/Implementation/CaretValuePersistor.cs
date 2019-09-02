using System;

namespace Feta.OpenType.Tables.GDEF.Implementation
{
    using Domain;
    using OpenType.Abstraction;
    using OpenType.Implementation;

    public class CaretValuePersistor : SubtablePersistor<GdefTable, CaretValue>
    {
        protected override CaretValue ReadSubtable(IFontReader reader)
        {
            CaretValue caretValue = new CaretValue();
            caretValue.Format = reader.ReadUInt16();

            switch (caretValue.Format)
            {
                case 1:
                    caretValue.Coordinate = reader.ReadUInt16();
                    break;
                case 2:
                    ReadFormatTwo();
                    break;
                case 3:
                    ReadFormatThree();
                    break;
            }

            return caretValue;
        }
        private void ReadFormatTwo()
        {
            throw new NotImplementedException();
        }
        private void ReadFormatThree()
        {
            throw new NotImplementedException();
        }

        protected override void AttachSubtable(GdefTable parentTable, CaretValue subTable) => parentTable.AddCaretValue(subTable);

        protected override void Write(IFontWriter writer, CaretValue table)
        {
            throw new NotImplementedException();
        }
    }
}

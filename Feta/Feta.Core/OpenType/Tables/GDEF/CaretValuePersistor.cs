using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.GDEF
{
    using Abstraction;
    using OpenType.Abstraction;
    using OpenType.Implementation;
    using Domain;

    public class CaretValuePersistor : SubtablePersistor<Table, CaretValue>
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

        protected override void AttachSubtable(Table parentTable, CaretValue subTable) => parentTable.AddCaretValue(subTable);


        public override void Write(IFontWriter writer, CaretValue table)
        {
            throw new NotImplementedException();
        }
    }
}

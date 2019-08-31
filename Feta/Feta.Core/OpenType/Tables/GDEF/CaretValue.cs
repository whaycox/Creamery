using System;
using System.Collections.Generic;
using System.Text;

namespace Feta.OpenType.Tables.GDEF
{
    using OpenType.Domain;

    public class CaretValue : BaseTable
    {
        public ushort Format { get; set; }

        #region Format One
        public ushort? Coordinate { get; set; }
        #endregion

    }
}

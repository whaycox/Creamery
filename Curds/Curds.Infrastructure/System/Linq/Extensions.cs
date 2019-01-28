using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public static class Extensions
    {
        public static string Stitch<T>(this IEnumerable<T> elements, string delimiter)
        {
            StringBuilder stitchBuilder = new StringBuilder();
            if (elements != null)
            {
                bool firstAdded = false;
                foreach (T element in elements)
                {
                    if (firstAdded)
                        stitchBuilder.Append(delimiter);

                    stitchBuilder.Append(element?.ToString());

                    if (!firstAdded)
                        firstAdded = true;
                }
            }
            return stitchBuilder.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiReader.Application.Helpers
{
    public static class Extensions
    {
        public static string JoinUsing(this IEnumerable<string> src, string separator)
        {
            if (src == null)
                return String.Empty;

            return String.Join(separator, src.ToArray());
        }
    }
}

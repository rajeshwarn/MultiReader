using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiReader.Epub.Exceptions
{
    public class MetadataParseException : Exception
    {
        public MetadataParseException(Exception ex, string message)
            : base(message, ex)
        {
        }
    }
}

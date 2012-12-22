using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiReader.Epub.Exceptions
{
    public class EpubFileException : Exception
    {
        public EpubFileException(Exception ex, string message)
            : base(message, ex)
        {
        }
    }
}

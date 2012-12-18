using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epub.Exceptions
{
    public class MultipleMetadataOccurencesException : Exception
    {
        public MultipleMetadataOccurencesException(string metadataName, string message)
            : base(message)
        {
            MetadataName = metadataName;
        }

        public string MetadataName { get; set; }
    }
}

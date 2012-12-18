using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epub.Exceptions
{
    public class MetadataException : Exception
    {
        public MetadataException(string metadataName, string message)
            : base(message)
        {
            MetadataName = metadataName;
        }

        public string MetadataName { get; set; }
    }
}
